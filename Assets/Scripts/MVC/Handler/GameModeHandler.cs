using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class GameModeHandler {

		private PlayerHandler _playerHandler;
		private AIHandler _aiHandler;

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

			if (_gameMode == GameMode.Singleplayer) {
				_aiHandler = new AIHandler();
			}

			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;

			GameManager.Instance.PlayerState = PlayerState.Player;
			GameManager.Instance.MatchState = MatchState.ThrowDice;
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			switch(nextMatchState) {
				case MatchState.SelectCharacter:
					{

						Debug.Log("SelectCharacter GameModeHandler");
						if (GameManager.Instance.GridHandler.SelectedCharacter != null) {
							GameManager.Instance.GridHandler.TintCharacter();
							
							if (GameMode == GameMode.Singleplayer && GameManager.Instance.PlayerState == PlayerState.Enemy) {
								GameManager.Instance.MatchState = MatchState.FocusCharacterTarget;
							}
							
						} else {
							GameManager.Instance.GridHandler.SwitchToNextPlayer ();
						}
						Debug.Log("SelectCharacter GameModeHandler ENDE");
						break;

					}
				case MatchState.Moving:
					{

						Debug.Log("Moving GameModeHandler");
						if (GameManager.Instance.GameFinished) {
							GameManager.Instance.MatchState = MatchState.Win;
						} else {
							GameManager.Instance.GridHandler.SwitchToNextPlayer ();
						}
						Debug.Log("Moving GameModeHandler ENDE");
						break;

					}
			}
			
		}
		
		public void Unregister () {
			if (_playerHandler != null) {
				_playerHandler.Unregister ();
			}
			if (_aiHandler != null) {
				_aiHandler.Unregister();
			}
		}
	}
	
}