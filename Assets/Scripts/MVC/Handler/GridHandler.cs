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
			_hexagonHandler.ResetFocusableNeighbors (_selectedCharacter.Model.GridPos);
			if (GameManager.Instance.PlayerState == PlayerState.Player) {
				_selectedCharacter = _characterHandler_P1.SelectNextCharacter ();
			} else if (GameManager.Instance.PlayerState == PlayerState.Enemy) {
				_selectedCharacter = _characterHandler_P2.SelectNextCharacter ();
			}
			_selectedHexagon = _hexagonHandler.Get (_selectedCharacter.Model.GridPos);
			_hexagonHandler.TintFocusableNeighbors (_selectedHexagon.Model.GridPos);
		}

		public void SelectNextHexagon () {
			//_hexagonHandler.ResetFocusableNeighbors(_selectedHexagon.Model.GridPos);
			_selectedHexagon = _hexagonHandler.SelectNextHexagon ();
		}

		public void SelectPrevHexagon () {
			//_hexagonHandler.ResetFocusableNeighbors(_selectedHexagon.Model.GridPos);
			_selectedHexagon = _hexagonHandler.SelectPrevHexagon ();
		}

		public void FocusNextHexagon (bool useForPasch = false) {
			Debug.Log (_selectedHexagon.Model.GridPos);
			_hexagonHandler.FocusNextHexagon (_selectedHexagon.Model.GridPos, useForPasch);
		}

		public void FocusPrevHexagon () {
			Debug.Log (_selectedHexagon.Model.GridPos);
			_hexagonHandler.FocusPrevHexagon (_selectedHexagon.Model.GridPos);
		}

		public void Unregister () {
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {

			if (nextMatchState == MatchState.Moving) {

				//Debug.LogWarning(_selectedCharacter.Model.GridPos + " - " + _selectedHexagon.Model.GridPos);

				if (_selectedCharacter != null) {
					_selectedCharacter.Model.Deselect ();
					_hexagonHandler.ResetFocusableNeighbors (_selectedCharacter.Model.GridPos);
				}
				if (_selectedHexagon != null) {
					_selectedHexagon.Model.Deselect ();
					//_selectedHexagon.View.ResetTint();
				}

				if (_hexagonHandler.FocusedHexagon != null) {
					if (_selectedCharacter != null) { // Move Character

						_selectedCharacter.Model.GridPos = _hexagonHandler.FocusedHexagon.Model.GridPos;

						Debug.Log (_selectedCharacter.Model.TeamColor + " == " + _hexagonHandler.FocusedHexagon.Model.TeamColor);
						if (_selectedCharacter.Model.TeamColor == _hexagonHandler.FocusedHexagon.Model.TeamColor) {
							_selectedCharacter.Model.Remove ();
						}
					} else { // Move Hexagon
						_selectedHexagon.Model.Deactivate ();
						_hexagonHandler.FocusedHexagon.Model.Activate ();

						List<ICharacterController> characters1 = (List<ICharacterController>)_characterHandler_P1.GetCharacters (_selectedHexagon.Model.GridPos);
						List<ICharacterController> characters2 = (List<ICharacterController>)_characterHandler_P2.GetCharacters (_selectedHexagon.Model.GridPos);
						characters1.AddRange (characters2);
						foreach (ICharacterController character in characters1) {
							character.Model.GridPos = _hexagonHandler.FocusedHexagon.Model.GridPos;
						}
					}
				}

				_hexagonHandler.ResetFocusedHexagon ();
				_selectedCharacter = null;
				_selectedHexagon = null;

				PlayerState playerState = (GameManager.Instance.PlayerState == PlayerState.Player) ? PlayerState.Enemy : PlayerState.Player;
				GameManager.Instance.PlayerState = playerState;
				GameManager.Instance.MatchState = MatchState.ThrowDice;


			} else if (nextMatchState == MatchState.SelectCharacter) {

				//Debug.LogWarning(_selectedCharacter.Model.GridPos + " - " + _selectedHexagon.Model.GridPos);

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

				_selectedHexagon = _hexagonHandler.Get (_selectedCharacter.Model.GridPos);
				_hexagonHandler.TintFocusableNeighbors (_selectedHexagon.Model.GridPos);
				Debug.LogWarning (_selectedCharacter.Model.GridPos + " - " + _selectedHexagon.Model.GridPos);

				/*
				 * TODO Select Hexagon and Character of player
				 */
			} else if (nextMatchState == MatchState.FocusCharacterTarget) {
				if (_selectedHexagon != null) {
					Debug.LogWarning (_selectedCharacter.Model.GridPos + " - " + _selectedHexagon.Model.GridPos);
					_hexagonHandler.FocusNextHexagon (_selectedHexagon.Model.GridPos, false);
				}
			} else if (nextMatchState == MatchState.SelectHexagon) {
				_selectedCharacter = null;
				_selectedHexagon = null;
				
				_hexagonHandler.InitSelectableHexagons ();
				_selectedHexagon = _hexagonHandler.SelectNextHexagon ();
			} else if (nextMatchState == MatchState.FocusHexagonTarget) {
				if (_selectedHexagon != null) {
					_hexagonHandler.FocusNextHexagon (_selectedHexagon.Model.GridPos, true);
				}
			}
		}


	}

}