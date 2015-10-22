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
		Win
	}
	public enum PlayerState {
		NullState,
		Player,
		Opponent
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
		private UIHandler _uiHandler;

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

			LocalizationManager.LoadLanguage ("english");

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
		
		public UIHandler UIHandler {
			get {
				return _uiHandler;
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
				if (_playerState == value) {
					return;
				}

				PlayerState prevState = _playerState;
				_playerState = value;

				if (OnPlayerStateChange != null) {
					OnPlayerStateChange (prevState, value);
				}

			}
		}

		public void InitGame () {
			Debug.LogWarning ("InitGame");
			_uiHandler = new UIHandler ();
			_gridHandler = new GridHandler ();
			_gameModeHandler.Init ();
		}

		void HandleOnGameStateChange (GameState prevGameState, GameState nextGameState) {
			if (prevGameState == GameState.MainMenu && nextGameState == GameState.Game) {
				InitGame ();
			} else {
				Debug.LogWarning ("ResetGame");
				if (_uiHandler != null) {
					_uiHandler.Unregister ();
				}
				if (_gridHandler != null) {
					_gridHandler.Unregister ();
				}
				if (_gameModeHandler != null) {
					_gameModeHandler.Unregister ();
				}
				_gridHandler = null;
				_uiHandler = null;
			}
		}
		
		public void OnApplicationQuit () {
			GameManager._instance = null;
		}

	}

}