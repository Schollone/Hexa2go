using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Hexa2Go {

	public class StartMatch : MonoBehaviour {

		[SerializeField]
		private GameMode
			gameMode;

		// Use this for initialization
		void Start () {
			Button start = GetComponent<Button> ();
			start.onClick.AddListener (HandleOnClicked);
		}

		private void HandleOnClicked () {
			//GameManager.Instance.GameModeHandler.GameMode = gameMode;
			switch (gameMode) {
				case GameMode.Singleplayer:
					GameManager.Instance.GameModeHandler.SetGameMode (new Singleplayer ());
					break;
				case GameMode.Multiplayer:
					GameManager.Instance.GameModeHandler.SetGameMode (new Multiplayer ());
					break;
				case GameMode.OnlineMultiplayer:
					//GameManager.Instance.GameModeHandler.SetGameMode (new OnlineMultiplayer ());
					break;
				default:
					throw new System.ArgumentOutOfRangeException ();
			}

			StartCoroutine (LoadingScreen ());
			StartCoroutine (LoadGame ());

		}

		IEnumerator LoadingScreen () {
			AsyncOperation async = Application.LoadLevelAdditiveAsync (2);
			yield return async;
		}

		IEnumerator LoadGame () {
			AsyncOperation async = Application.LoadLevelAsync (1);
			yield return async;
		}
	}

}