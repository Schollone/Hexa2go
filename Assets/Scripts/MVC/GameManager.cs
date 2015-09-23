using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public enum GameState {
		NullState,
		MainMenu,
		Game,
		Pause,
		Credits,
		Options,
		Quit
	}
	public enum MatchState {
		NullState,
		ThrowDice,
		Throwing,
		SelectCharacter,
		FocusCharacterTarget,
		SelectHexagon,
		FocusHexagonTarget,
		Moving,
		Win,
		Lose
	}
	public enum PlayerState {
		NullState,
		Player,
		Enemy
	}
	public enum GameMode {
		Singleplayer,
		Multiplayer,
		OnlineMultiplayer
	}

	public delegate void GameStateChangeHandler (GameState prevGameState,GameState nextGameState);
	public delegate void MatchStateChangeHandler (MatchState prevMatchState,MatchState nextMatchState);
	public delegate void PlayerStateChangeHandler (PlayerState prevPlayerState,PlayerState nextPlayerState);

	public class GameManager {

		private static GameManager _instance = null;

		public event GameStateChangeHandler OnGameStateChange;
		public event MatchStateChangeHandler OnMatchStateChange;
		public event PlayerStateChangeHandler OnPlayerStateChange;

		private GridHandler _gridHandler;
		private GameModeHandler _gameModeHandler;
		private ButtonHandler _buttonHandler;

		private GameState _gameState;
		private MatchState _matchState;
		private PlayerState _playerState;

		public bool GameFinished = false;

		protected GameManager () {
			Debug.LogWarning ("GameManager");
			_gameState = GameState.NullState;
			_matchState = MatchState.NullState;
			_playerState = PlayerState.NullState;

			_gameModeHandler = new GameModeHandler ();

			OnGameStateChange += HandleOnGameStateChange;
		}

		public static GameManager Instance {
			get {
				if (GameManager._instance == null) {
					GameManager._instance = new GameManager ();
				}
				return GameManager._instance;
			}
		}

		public GameModeHandler GameModeHandler {
			get {
				return _gameModeHandler;
			}
		}
		
		public ButtonHandler ButtonHandler {
			get {
				return _buttonHandler;
			}
		}
		
		public GridHandler GridHandler {
			get {
				return _gridHandler;
			}
		}

		public GameState GameState {
			get {
				return _gameState;
			}
			set {
				Debug.LogWarning (_gameState + " ==> " + value);
				if (_gameState == value) {
					return;
				}

				GameState prevState = _gameState;
				_gameState = value;
				
				if (OnGameStateChange != null) {
					OnGameStateChange (prevState, value);
				}

			}
		}

		public MatchState MatchState {
			get {
				return _matchState;
			}
			set {
				Debug.LogWarning (_matchState + " ==> " + value);
				if (_matchState == value) {
					return;
				}

				MatchState prevState = _matchState;
				_matchState = value;
				
				if (OnMatchStateChange != null) {
					OnMatchStateChange (prevState, value);
				}

			}
		}

		public PlayerState PlayerState {
			get {
				return _playerState;
			}
			set {
				Debug.LogWarning (_playerState + " ==> " + value);

				PlayerState prevState = _playerState;
				_playerState = value;

				if (OnPlayerStateChange != null) {
					OnPlayerStateChange (prevState, value);
				}

			}
		}

		public void InitGame () {
			Debug.LogWarning ("InitGame");
			_buttonHandler = new ButtonHandler ();
			_gridHandler = new GridHandler ();
			_gameModeHandler.Init ();
			//_gameModeHandler = new GameModeHandler ();
		}

		void HandleOnGameStateChange (GameState prevGameState, GameState nextGameState) {
			if (prevGameState == GameState.MainMenu && nextGameState == GameState.Game) {
				InitGame ();
			} else {
				Debug.LogWarning ("ResetGame");
				if (_buttonHandler != null) {
					_buttonHandler.Unregister ();
				}
				if (_gridHandler != null) {
					_gridHandler.Unregister ();
				}
				if (_gameModeHandler != null) {
					_gameModeHandler.Unregister ();
				}
				_gridHandler = null;
				_buttonHandler = null;
			}
		}
		
		public void OnApplicationQuit () {
			GameManager._instance = null;
		}

	}

}