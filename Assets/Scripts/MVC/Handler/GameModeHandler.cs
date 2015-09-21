using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class GameModeHandler {

		private readonly PlayerHandler _playerHandler;

		private GameMode _gameMode = GameMode.Singleplayer;

		public GameModeHandler (GameMode gameMode) {

			_gameMode = gameMode;

			_playerHandler = new PlayerHandler ();

			GameManager.Instance.OnGameStateChange += HandleOnGameStateChange;

			switch (_gameMode) {
				case GameMode.Singleplayer:
					{
						Debug.Log ("Singleplayer");
						_playerHandler.PlayerController_One.Model.Name = "Spieler";
						_playerHandler.PlayerController_Two.Model.Name = "Computer";
						break;
					}
				case GameMode.Multiplayer:
					{
						Debug.Log ("Multiplayer");
						_playerHandler.PlayerController_One.Model.Name = "Spieler 1";
						_playerHandler.PlayerController_Two.Model.Name = "Spieler 2";
						break;
					}
				case GameMode.OnlineMultiplayer:
					{
						Debug.Log ("OnlineMultiplayer");
						_playerHandler.PlayerController_One.Model.Name = "Spieler";
						_playerHandler.PlayerController_Two.Model.Name = "Gegner";
						break;
					}
			}

			// choose player random
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