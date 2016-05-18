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
			Time.timeScale = 1f;
		}

		private void HandleOnClicked () {
			switch (gameMode) {
				case GameMode.Singleplayer:
					GameManager.Instance.SetGameMode (new Singleplayer ());
					break;
				case GameMode.Multiplayer:
					GameManager.Instance.SetGameMode (new Multiplayer ());
					break;
				case GameMode.OnlineMultiplayer:
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