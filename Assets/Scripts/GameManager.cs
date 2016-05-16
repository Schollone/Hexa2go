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
	public enum GameMode {
		Singleplayer,
		Multiplayer,
		OnlineMultiplayer
	}

	public delegate void GameStateChangeHandler (GameState prevGameState,GameState nextGameState);

	public class GameManager {

		private static GameManager _instance = null;

		public event GameStateChangeHandler OnGameStateChange;

		private GridFacade _gridFacade;

		private CameraController _cameraController;

		private GameState _gameState;

		public bool MatchStateChanged = false;
		public bool PlayerStateChanged = false;

		private IGameMode _gameMode;

		protected GameManager () {
			_gameState = GameState.NullState;

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

		private void InitGame () {
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
			if (prevGameState == GameState.MainMenu && nextGameState == GameState.Match) {
				InitGame ();
			} else {
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