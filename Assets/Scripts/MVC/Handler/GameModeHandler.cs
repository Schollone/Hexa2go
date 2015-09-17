using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class GameModeHandler {

		private readonly PlayerHandler _playerHandler;

		private GameMode _gameMode = GameMode.Singleplayer;

		public GameModeHandler(GameMode gameMode) {

			_gameMode = gameMode;

			GameManager.Instance.OnGameStateChange += HandleOnGameStateChange;

			switch(_gameMode) {
				case GameMode.Singleplayer: {
					Debug.Log("Singleplayer");
					break;
				}
				case GameMode.Multiplayer: {
					Debug.Log("Multiplayer");
					break;
				}
				case GameMode.OnlineMultiplayer: {
					Debug.Log("OnlineMultiplayer");
					break;
				}
			}

			// choose player random
			GameManager.Instance.PlayerState = PlayerState.Player;


			GameManager.Instance.MatchState = MatchState.ThrowDice;
		}

		void HandleOnGameStateChange (GameState prevGameState, GameState nextGameState) {
			if (prevGameState == GameState.NullState) {
				// Init
			}

			if (nextGameState == GameState.Game) {
				//Debug.Log("Change To Game");
				//GameManager.Instance.SetMatchState(MatchState.ThrowDice);
			}
		}
	}
	
}