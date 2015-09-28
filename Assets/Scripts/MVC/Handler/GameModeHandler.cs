using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class GameModeHandler {

		private PlayerHandler _playerHandler;

		private GameMode _gameMode = GameMode.Singleplayer;

		public PlayerHandler PlayerHandler {
			get {
				return _playerHandler;
			}
		}

		public GameMode GameMode {
			get {
				return _gameMode;
			}
			set {
				_gameMode = value;
			}
		}

		public void Init () {
			_playerHandler = new PlayerHandler ();
			GameManager.Instance.OnGameStateChange += HandleOnGameStateChange;
			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
			GameManager.Instance.OnPlayerStateChange += HandleOnPlayerStateChange;
			GameManager.Instance.PlayerState = PlayerState.Player;
			GameManager.Instance.MatchState = MatchState.ThrowDice;
		}

		void HandleOnGameStateChange (GameState prevGameState, GameState nextGameState) {
			
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			if (GameManager.Instance.GameModeHandler.GameMode != GameMode.Singleplayer) {
				return;
			}

			if (GameManager.Instance.PlayerState == PlayerState.Player) {
				return;
			}
			
			switch (nextMatchState) {
				case MatchState.SelectCharacter:
					{
						Debug.Log ("SelectCharacter GameModeHandler");
						//Debug.LogWarning (selectedCharacter);
						//GameManager.Instance.GridHandler.SelectNextCharacter ();
						//selectedCharacter = GameManager.Instance.GridHandler.SelectedCharacter;
						//Debug.LogWarning (selectedCharacter);
						GameManager.Instance.MatchState = MatchState.FocusCharacterTarget;
						break;
					}
				case MatchState.FocusCharacterTarget:
					{
						Debug.Log ("FocusHexagonTarget GameModeHandler");
						IHexagonController selectedHexagon = GameManager.Instance.GridHandler.SelectedHexagon;
						ICharacterController selectedCharacter = GameManager.Instance.GridHandler.SelectedCharacter;
						GridPos targetPos = GameManager.Instance.GridHandler.HexagonHandler.GetTarget (selectedCharacter.Model.TeamColor).Model.GridPos;
						GridPos nextPos = GameManager.Instance.GridHandler.HexagonHandler.GetNextHexagonToFocus (
						selectedHexagon.Model.GridPos, targetPos);
						IHexagonController focusedHexagon = GameManager.Instance.GridHandler.HexagonHandler.FocusedHexagon;
						while (!GameManager.Instance.GridHandler.HexagonHandler.FocusedHexagon.Model.GridPos.Equals(nextPos)) {
							GameManager.Instance.GridHandler.FocusNextHexagon ();
						}

						GameManager.Instance.MatchState = MatchState.Moving;
						break;
					}
				case MatchState.SelectHexagon:
					{
						Debug.Log ("SelectHexagon GameModeHandler");
						break;
					}
				case MatchState.FocusHexagonTarget:
					{
						Debug.Log ("FocusHexagonTarget GameModeHandler");
						break;
					}
			}
		}

		void HandleOnPlayerStateChange (PlayerState prevPlayerState, PlayerState nextPlayerState) {
			if (GameManager.Instance.GameModeHandler.GameMode != GameMode.Singleplayer) {
				return;
			}

			if (nextPlayerState == PlayerState.Enemy) {
				Debug.Log (GameManager.Instance.MatchState);
				GameManager.Instance.MatchState = MatchState.Throwing;
			}
		}

		public void Unregister () {
			if (_playerHandler != null) {
				_playerHandler.Unregister ();
			}
			GameManager.Instance.OnGameStateChange -= HandleOnGameStateChange;
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
			GameManager.Instance.OnPlayerStateChange -= HandleOnPlayerStateChange;
		}
	}
	
}