using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hexa2Go {

	public class GridHandler {

		public static int WIDTH = 10;
		public static int HEIGHT = 7;

		private readonly HexagonHandler _hexagonHandler;
		private readonly CharacterHandler _characterHandler_P1;
		private readonly CharacterHandler _characterHandler_P2;

		private IHexagonController _selectedHexagon = null;
		private ICharacterController _selectedCharacter = null;

		public GridHandler () {

			_hexagonHandler = new HexagonHandler (WIDTH, HEIGHT);
			_characterHandler_P1 = new CharacterHandler (TeamColor.RED);
			_characterHandler_P2 = new CharacterHandler (TeamColor.BLUE);

			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
		}

		public HexagonHandler HexagonHandler {
			get {
				return _hexagonHandler;
			}
		}

		public CharacterHandler CharacterHandler_P1 {
			get {
				return _characterHandler_P1;
			}
		}

		public CharacterHandler CharacterHandler_P2 {
			get {
				return _characterHandler_P2;
			}
		}

		public void SelectNextCharacter () {
			_hexagonHandler.InitNeighbors (_selectedCharacter.Model.GridPos, true);
			_hexagonHandler.ResetFocusableNeighbors ();
			if (GameManager.Instance.PlayerState == PlayerState.Player) {
				_selectedCharacter = _characterHandler_P1.SelectNextCharacter ();
			} else if (GameManager.Instance.PlayerState == PlayerState.Enemy) {
				_selectedCharacter = _characterHandler_P2.SelectNextCharacter ();
			}
			_selectedHexagon = _hexagonHandler.Get (_selectedCharacter.Model.GridPos);
			_hexagonHandler.InitNeighbors (_selectedHexagon.Model.GridPos, true);
			_hexagonHandler.TintFocusableNeighbors ();
		}

		public void SelectNextHexagon () {
			_hexagonHandler.InitNeighbors (_selectedHexagon.Model.GridPos, true, true);
			_hexagonHandler.ResetFocusableNeighbors ();
			_selectedHexagon = _hexagonHandler.SelectNextHexagon ();
			_hexagonHandler.InitNeighbors (_selectedHexagon.Model.GridPos, true, true);
			_hexagonHandler.TintFocusableNeighbors ();
		}

		public void SelectPrevHexagon () {
			_hexagonHandler.InitNeighbors (_selectedHexagon.Model.GridPos, true, true);
			_hexagonHandler.ResetFocusableNeighbors ();
			_selectedHexagon = _hexagonHandler.SelectPrevHexagon ();
			_hexagonHandler.InitNeighbors (_selectedHexagon.Model.GridPos, true, true);
			_hexagonHandler.TintFocusableNeighbors ();
		}

		public void FocusNextHexagon (bool useForPasch = false) {
			_hexagonHandler.FocusNextHexagon (_selectedHexagon.Model.GridPos, useForPasch);
		}

		public void FocusPrevHexagon (bool useForPasch = false) {
			_hexagonHandler.FocusPrevHexagon (_selectedHexagon.Model.GridPos, useForPasch);
		}

		public void Unregister () {
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
		}

		private void SwitchToNextPlayer () {
			PlayerState playerState = (GameManager.Instance.PlayerState == PlayerState.Player) ? PlayerState.Enemy : PlayerState.Player;
			GameManager.Instance.PlayerState = playerState;
			GameManager.Instance.MatchState = MatchState.ThrowDice;
		}

		private void ResetSelectedCharacterAndNeighbors () {
			if (_selectedCharacter != null) {
				_selectedCharacter.Model.Deselect ();
				_hexagonHandler.InitNeighbors (_selectedCharacter.Model.GridPos);
				_hexagonHandler.ResetFocusableNeighbors ();
			}
		}

		private void ResetSelectedHexagonAndNeighbors () {
			if (_selectedHexagon != null) {
				_selectedHexagon.Model.Deselect ();
				_hexagonHandler.InitNeighbors (_selectedHexagon.Model.GridPos);
				_hexagonHandler.ResetFocusableNeighbors ();
			}
		}

		private void MoveCharacter () {
			_selectedCharacter.Model.GridPos = _hexagonHandler.FocusedHexagon.Model.GridPos;
			if (_selectedCharacter.Model.TeamColor == _hexagonHandler.FocusedHexagon.Model.TeamColor) {
				_selectedCharacter.Model.Remove ();
			}
		}

		private void MoveHexagon () {
			Debug.Log ("Move Hexagon " + _selectedHexagon.Model.TeamColor);
			_hexagonHandler.FocusedHexagon.Model.Activate (false, _selectedHexagon.Model.TeamColor);
			/*if (_selectedHexagon.Model.IsTarget) {
				_hexagonHandler.FocusedHexagon.Model.DeclareTarget (_selectedHexagon.Model.TeamColor);
			}*/
			_selectedHexagon.Model.Deactivate ();

			List<ICharacterController> characters1 = (List<ICharacterController>)_characterHandler_P1.GetCharacters (_selectedHexagon.Model.GridPos);
			List<ICharacterController> characters2 = (List<ICharacterController>)_characterHandler_P2.GetCharacters (_selectedHexagon.Model.GridPos);
			characters1.AddRange (characters2);
			foreach (ICharacterController character in characters1) {
				character.Model.GridPos = _hexagonHandler.FocusedHexagon.Model.GridPos;
			}
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {

			switch (nextMatchState) {
				case MatchState.SelectCharacter:
					{
						Debug.Log ("SelectCharacter GridHandler");
					
						_selectedCharacter = null;
						_selectedHexagon = null;
					
						CharacterType characterType_1 = GameManager.Instance.ButtonHandler.DicesController.DiceController_left.Model.CharacterType;
						CharacterType characterType_2 = GameManager.Instance.ButtonHandler.DicesController.DiceController_right.Model.CharacterType;
					
						if (GameManager.Instance.PlayerState == PlayerState.Player) {
						
							_characterHandler_P1.InitSelectedCharacters (characterType_1, characterType_2);
							_selectedCharacter = _characterHandler_P1.SelectNextCharacter ();
						
						} else if (GameManager.Instance.PlayerState == PlayerState.Enemy) {
						
							_characterHandler_P2.InitSelectedCharacters (characterType_1, characterType_2);
							_selectedCharacter = _characterHandler_P2.SelectNextCharacter ();
						
						}

						if (_selectedCharacter != null) {
							_selectedHexagon = _hexagonHandler.Get (_selectedCharacter.Model.GridPos);
							_hexagonHandler.InitNeighbors (_selectedHexagon.Model.GridPos, true);
							_hexagonHandler.TintFocusableNeighbors ();
						} else {
							SwitchToNextPlayer ();
						}
						break;
					}

				case MatchState.FocusCharacterTarget:
					{
						Debug.Log ("FocusCharacterTarget GridHandler");
						if (_selectedHexagon != null) {
							Debug.LogWarning (_selectedCharacter.Model.GridPos + " - " + _selectedHexagon.Model.GridPos);
							_hexagonHandler.FocusNextHexagon (_selectedHexagon.Model.GridPos);
						}
						break;
					}

				case MatchState.SelectHexagon:
					{
						Debug.Log ("SelectHexagon GridHandler");
						_selectedCharacter = null;
						_selectedHexagon = null;
					
						_hexagonHandler.InitSelectableHexagons ();
						_selectedHexagon = _hexagonHandler.SelectNextHexagon ();
						_hexagonHandler.InitNeighbors (_selectedHexagon.Model.GridPos, true, true);
						_hexagonHandler.TintFocusableNeighbors ();
						break; 
					}

				case MatchState.FocusHexagonTarget:
					{
						Debug.Log ("FocusHexagonTarget GridHandler");
						if (_selectedHexagon != null) {
							_hexagonHandler.FocusNextHexagon (_selectedHexagon.Model.GridPos, true);
						}
						break;
					}

				case MatchState.Moving:
					{
						Debug.Log ("Moving GridHandler");
					
						ResetSelectedCharacterAndNeighbors ();
						ResetSelectedHexagonAndNeighbors ();
					
						if (_hexagonHandler.FocusedHexagon != null) {
							if (_selectedCharacter != null) {
								MoveCharacter ();
							} else {
								MoveHexagon ();
							}
						}
					
						_hexagonHandler.ResetFocusedHexagon ();
						_selectedCharacter = null;
						_selectedHexagon = null;

						if (GameManager.Instance.GameFinished) {
							GameManager.Instance.MatchState = MatchState.Win;
						} else {
							SwitchToNextPlayer ();
						}
						break;
					}
			}
		}


	}

}