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
		private CameraController _cameraController;

		private IHexagonController _selectedHexagon = null;
		private ICharacterController _selectedCharacter = null;

		public delegate void CharacterChangeHandler (ICharacterController nextCharacter);

		private event CharacterChangeHandler OnCharacterChanged;

		public IHexagonController SelectedHexagon {
			get {
				return _selectedHexagon;
			}
			set {
				_selectedHexagon = value;
			}
		}

		public ICharacterController SelectedCharacter {
			get {
				return _selectedCharacter;
			}
			set {
				_selectedCharacter = value;
			}
		}

		public GridHandler () {
			GameObject camera = GameObject.Find ("Camera");
			CameraView cameraView = camera.GetComponent<CameraView> ();
			_cameraController = new CameraController (cameraView);
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

		public void TintCharacter () {
			_selectedHexagon = _hexagonHandler.Get (_selectedCharacter.Model.GridPos);
			_hexagonHandler.InitNeighbors (_selectedHexagon.Model.GridPos, true);
			_hexagonHandler.TintFocusableNeighbors ();
		}

		public void SelectNextCharacter (CharacterType type) {
			if (GameManager.Instance.PlayerState == PlayerState.Player) {
				ICharacterController controller = CharacterHandler_P1.GetCharacter (type);
				if (OnCharacterChanged != null) {
					OnCharacterChanged (controller);
				}
			}


		}
		
		public void SelectNextCharacter () {
			if (_selectedCharacter == null) {
				return;
			}

			for (int i = 0; i < 2; i++) {
				_hexagonHandler.InitNeighbors (_selectedCharacter.Model.GridPos, true);
				_hexagonHandler.ResetFocusableNeighbors ();
				if (GameManager.Instance.PlayerState == PlayerState.Player) {
					_selectedCharacter = _characterHandler_P1.SelectNextCharacter ();
				} else if (GameManager.Instance.PlayerState == PlayerState.Opponent) {
					_selectedCharacter = _characterHandler_P2.SelectNextCharacter ();
				}
				
				TintCharacter ();

				if (_hexagonHandler.HasFocusableNeighbors ()) {
					break;
				}
			}

			if (!_hexagonHandler.HasFocusableNeighbors ()) {
				_hexagonHandler.ResetFocusableNeighbors ();
				ResetSelectedHexagonAndNeighbors ();
				//GameManager.Instance.GameModeHandler.GetGameMode ().SwitchToNextPlayer ();
			}
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
			if (_selectedHexagon != null) {
				_hexagonHandler.FocusNextHexagon (_selectedHexagon.Model.GridPos, useForPasch);
			}
		}

		public void FocusPrevHexagon (bool useForPasch = false) {
			if (_selectedHexagon != null) {
				_hexagonHandler.FocusPrevHexagon (_selectedHexagon.Model.GridPos, useForPasch);
			}
		}

		public void Unregister () {
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
			_cameraController.Unregister ();
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
				_hexagonHandler.FocusedHexagon.View.PlayExplosion ();
			}
		}

		private void MoveHexagon () {
			_hexagonHandler.FocusedHexagon.Model.Activate (false, _selectedHexagon.Model.TeamColor);
			_selectedHexagon.Model.Deactivate ();

			List<ICharacterController> characters1 = (List<ICharacterController>)_characterHandler_P1.GetCharacters (_selectedHexagon.Model.GridPos);
			List<ICharacterController> characters2 = (List<ICharacterController>)_characterHandler_P2.GetCharacters (_selectedHexagon.Model.GridPos);
			characters1.AddRange (characters2);
			foreach (ICharacterController character in characters1) {
				character.Model.GridPos = _hexagonHandler.FocusedHexagon.Model.GridPos;
			}
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			PlayerState playerState = GameManager.Instance.PlayerState;

			switch (nextMatchState) {
				case MatchState.SelectCharacter:
					{
						_hexagonHandler.ResetFocusedHexagon ();
						_selectedCharacter = null;
						_selectedHexagon = null;
					
						CharacterType characterType_1 = GameManager.Instance.UIHandler.DicesController.DiceController_left.Model.CharacterType;
						CharacterType characterType_2 = GameManager.Instance.UIHandler.DicesController.DiceController_right.Model.CharacterType;

						if (GameManager.Instance.PlayerState == PlayerState.Player) {
							_characterHandler_P1.InitSelectedCharacters (characterType_1, characterType_2);
							_selectedCharacter = _characterHandler_P1.SelectNextCharacter ();
						} else if (GameManager.Instance.PlayerState == PlayerState.Opponent) {
							_characterHandler_P2.InitSelectedCharacters (characterType_1, characterType_2);
							_selectedCharacter = _characterHandler_P2.SelectNextCharacter ();
						}
						SelectNextCharacter ();
						break;
					}

				case MatchState.SelectHexagon:
					{
						_hexagonHandler.ResetFocusedHexagon ();
						_selectedCharacter = null;
						_selectedHexagon = null;
						break; 
					}
		
				case MatchState.Moving:
					{
						ResetSelectedCharacterAndNeighbors ();
						ResetSelectedHexagonAndNeighbors ();
					
						if (_hexagonHandler.FocusedHexagon != null) {
							if (_selectedCharacter != null) {
								MoveCharacter ();
							} else {
								MoveHexagon ();
							}
						}
					
						//_hexagonHandler.ResetFocusedHexagon ();
						_selectedCharacter = null;
						_selectedHexagon = null;
						break;
					}
				case MatchState.Win:
					{
						if (_hexagonHandler.FocusedHexagon != null) {
							_hexagonHandler.FocusedHexagon.View.PlayExplosion (true);
						}
						_hexagonHandler.ResetFocusedHexagon ();
						break;
					}
			}
		}





	}

}