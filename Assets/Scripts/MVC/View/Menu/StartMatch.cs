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
			Debug.Log ("Start Game: " + gameMode);
			GameManager.Instance.GameModeHandler.GameMode = gameMode;

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