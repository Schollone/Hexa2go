using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class GameModeHandler {

		private PlayerHandler _playerHandler;

		private GameMode _gameMode = GameMode.Singleplayer;

		public GameModeHandler () {

			//_gameMode = gameMode;

			//Init ();
		}

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
			GameManager.Instance.PlayerState = PlayerState.Player;
			GameManager.Instance.MatchState = MatchState.ThrowDice;
		}

		void HandleOnGameStateChange (GameState prevGameState, GameState nextGameState) {

		}

		public void Unregister () {
			if (_playerHandler != null) {
				_playerHandler.Unregister ();
			}
		}
	}
	
}