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
				_aiHandler = new AIHandler ();
			} else {
				_aiHandler = null;
			}

			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;

			System.Random r = new System.Random ();
			int randomPlayer = r.Next (1, 3);
			GameManager.Instance.PlayerState = (PlayerState)randomPlayer;
			//GameManager.Instance.PlayerState = PlayerState.Opponent;
			//GameManager.Instance.MatchState = MatchState.ThrowDice;
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			switch (nextMatchState) {
				case MatchState.SelectCharacter:
					{
						if (GameManager.Instance.GridHandler.SelectedCharacter != null) {
							GameManager.Instance.GridHandler.TintCharacter ();
							
							if (GameMode == GameMode.Singleplayer && GameManager.Instance.PlayerState == PlayerState.Opponent) {
								GameManager.Instance.MatchState = MatchState.FocusCharacterTarget;
							}
							
						} else {
							SwitchToNextPlayer ();
						}
						break;

					}
				case MatchState.Moving:
					{
						if (GameManager.Instance.GameFinished) {
							GameManager.Instance.GameFinished = false;
							GameManager.Instance.MatchState = MatchState.Win;
						} else {
							SwitchToNextPlayer ();
						}
						break;

					}
			}
			
		}
		
		public void Unregister () {
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
			if (_playerHandler != null) {
				_playerHandler.Unregister ();
			}
			if (_aiHandler != null) {
				_aiHandler.Unregister ();
			}
		}

		public void SwitchToNextPlayer () {
			PlayerState playerState = (GameManager.Instance.PlayerState == PlayerState.Player) ? PlayerState.Opponent : PlayerState.Player;
			GameManager.Instance.PlayerState = playerState;
			GameManager.Instance.MatchState = MatchState.ThrowDice;
		}
	}
	
}