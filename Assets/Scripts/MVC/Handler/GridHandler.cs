using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public class GridHandler {

		public static int WIDTH = 10;
		public static int HEIGHT = 7;

		private readonly HexagonHandler _hexagonHandler;
		private readonly CharacterHandler _characterHandler_P1;
		private readonly CharacterHandler _characterHandler_P2;

		private IHexagonController _selectedHexagon = null;
		private ICharacterController _selectedCharacter = null;

		public GridHandler() {

			_hexagonHandler = new HexagonHandler(WIDTH, HEIGHT);
			_characterHandler_P1 = new CharacterHandler(TeamColor.RED);
			_characterHandler_P2 = new CharacterHandler(TeamColor.BLUE);

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

		public void SelectNextCharacter() {
			_hexagonHandler.ResetFocusableNeighbors(_selectedCharacter.Model.GridPos);
			if (GameManager.Instance.PlayerState == PlayerState.Player) {
				_selectedCharacter = _characterHandler_P1.SelectNextCharacter();
			} else if (GameManager.Instance.PlayerState == PlayerState.Enemy) {
				_selectedCharacter = _characterHandler_P2.SelectNextCharacter();
			}
			_selectedHexagon = _hexagonHandler.Get(_selectedCharacter.Model.GridPos);
			_hexagonHandler.TintFocusableNeighbors(_selectedHexagon.Model.GridPos);
		}

		public void FocusNextHexagon() {
			Debug.Log(_selectedCharacter.Model.GridPos);
			_hexagonHandler.FocusNextHexagon(_selectedCharacter.Model.GridPos);
		}

		public void FocusPrevHexagon() {
			Debug.Log(_selectedCharacter.Model.GridPos);
			_hexagonHandler.FocusPrevHexagon(_selectedCharacter.Model.GridPos);
		}

		public void Unregister() {
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {

			if (nextMatchState == MatchState.Moving) {

				Debug.LogWarning(_selectedCharacter.Model.GridPos + " - " + _selectedHexagon.Model.GridPos);

				if (_selectedCharacter != null) {
					_selectedCharacter.Model.Deselect();
					_hexagonHandler.ResetFocusableNeighbors(_selectedCharacter.Model.GridPos);
				}
				if (_selectedHexagon != null) {
					_selectedHexagon.Model.Deselect();
					//_selectedHexagon.View.ResetTint();
				}

				_hexagonHandler.ResetFocusedHexagon();
				_selectedCharacter = null;
				_selectedHexagon = null;

				PlayerState playerState = (GameManager.Instance.PlayerState == PlayerState.Player) ? PlayerState.Enemy : PlayerState.Player;
				GameManager.Instance.MatchState = MatchState.ThrowDice;
				GameManager.Instance.PlayerState = playerState;

			} else if (nextMatchState == MatchState.SelectCharacter) {

				//Debug.LogWarning(_selectedCharacter.Model.GridPos + " - " + _selectedHexagon.Model.GridPos);

				_selectedCharacter = null;
				_selectedHexagon = null;

				CharacterType characterType_1 = GameManager.Instance.ButtonHandler.DicesController.DiceController_left.Model.CharacterType;
				CharacterType characterType_2 = GameManager.Instance.ButtonHandler.DicesController.DiceController_right.Model.CharacterType;

				if (GameManager.Instance.PlayerState == PlayerState.Player) {

					_characterHandler_P1.InitSelectedCharacters(characterType_1, characterType_2);
					_selectedCharacter = _characterHandler_P1.SelectNextCharacter();

				} else if (GameManager.Instance.PlayerState == PlayerState.Enemy) {

					_characterHandler_P2.InitSelectedCharacters(characterType_1, characterType_2);
					_selectedCharacter = _characterHandler_P2.SelectNextCharacter();

				}

				_selectedHexagon = _hexagonHandler.Get(_selectedCharacter.Model.GridPos);
				_hexagonHandler.TintFocusableNeighbors(_selectedHexagon.Model.GridPos);
				Debug.LogWarning(_selectedCharacter.Model.GridPos + " - " + _selectedHexagon.Model.GridPos);

				/*
				 * TODO Select Hexagon and Character of player
				 */
			} else if (nextMatchState == MatchState.FocusCharacterTarget) {
				if (_selectedCharacter != null) {
					Debug.LogWarning(_selectedCharacter.Model.GridPos + " - " + _selectedHexagon.Model.GridPos);
					_hexagonHandler.FocusNextHexagon(_selectedCharacter.Model.GridPos);
				}
			}
		}


	}

}