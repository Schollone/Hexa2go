using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Hexa2Go {

	public class StartGame : MonoBehaviour {

		[SerializeField] private GameMode gameMode;

		// Use this for initialization
		void Start () {
			Button start = GetComponent<Button>();	
			start.onClick.AddListener(HandleOnClicked);
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		private void HandleOnClicked() {
			Debug.Log("Start Game: " + gameMode);
			GameManager.Instance.GameMode = gameMode;
			Application.LoadLevel(1);
		}
	}

}