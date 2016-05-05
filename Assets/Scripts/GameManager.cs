using System.Collections;
using UnityEngine;

namespace Hexa2Go {

	public enum GameState {
		NullState,
		MainMenu,
		Match,
		Pause,
		Credits,
		Options,
		Quit
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
	public delegate void PlayerStateChangeHandler (PlayerState prevPlayerState,PlayerState nextPlayerState);

	public class GameManager {

		private static GameManager _instance = null;

		public event GameStateChangeHandler OnGameStateChange;

		private GridHandler _gridHandler;
		private GridFacade _gridFacade;

		private CameraController _cameraController;

		private GameState _gameState;
		private PlayerState _playerState;
		private PlayerState _prevPlayerState;

		public bool MatchStateChanged = false;
		public bool PlayerStateChanged = false;

		private IGameMode _gameMode;

		protected GameManager () {
			Debug.LogWarning ("GameManager");
			_gameState = GameState.NullState;
			_playerState = PlayerState.NullState;
			_prevPlayerState = PlayerState.NullState;

			LocalizationManager.Instance.LoadLanguage ("english");

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

		public GridFacade GridFacade {
			get {
				return _gridFacade;
			}
		}

		public CameraController CameraHandler {
			get {
				return _cameraController;
			}
		}
		
		/*public GridHandler GridHandler {
			get {
				return _gridHandler;
			}
		}*/

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

		public void SetGameMode (IGameMode gameMode) {
			this._gameMode = gameMode;
		}
		
		public IGameMode GetGameMode () {
			return this._gameMode;
		}

		public void InitNextMatchState () {
			GetGameMode ().Operate ();
		}

		public PlayerState PlayerState {
			get {
				return _playerState;
			}
			set {
				if (_playerState == value) {
					return;
				}

				_prevPlayerState = _playerState;
				_playerState = value;
				PlayerStateChanged = true;
			}
		}

		private void InitGame () {
			Debug.LogWarning ("InitGame");
			UIHandler.Instance.Init ();

			GetGameMode ().Init ();
			_gridFacade = new GridFacade ();

			GameObject camera = GameObject.Find ("Camera");
			CameraView cameraView = camera.GetComponent<CameraView> ();
			_cameraController = new CameraController (cameraView);
		}

		private void OnExitGame () {
			_gridFacade = null;
		}

		void HandleOnGameStateChange (GameState prevGameState, GameState nextGameState) {
			Debug.Log ("OnGameStateChange: " + nextGameState);
			if (prevGameState == GameState.MainMenu && nextGameState == GameState.Match) {
				InitGame ();
			} else {
				Debug.LogWarning ("ResetGame");

				if (_gridHandler != null) {
					_gridHandler.Unregister ();
				}
				_gridHandler = null;
				if (prevGameState != GameState.NullState && nextGameState == GameState.MainMenu) {
					OnExitGame ();
				}

			}
		}
		
		public void OnApplicationQuit () {
			GameManager._instance = null;
		}

	}

}