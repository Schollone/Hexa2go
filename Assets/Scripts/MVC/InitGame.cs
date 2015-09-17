using UnityEngine;
using System.Collections;
using Hexa2Go;

public class InitGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameManager.Instance.GameState == GameState.NullState) {
			GameManager.Instance.GameState = GameState.MainMenu;
			GameManager.Instance.MatchState = MatchState.NullState;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnLevelWasLoaded(int level) {
		Debug.LogWarning("Loaded Scene: " + level);

		if (level == 0) {
			GameManager.Instance.GameState = GameState.MainMenu;
			GameManager.Instance.MatchState = MatchState.NullState;
		}
		
		if (level == 1) {
			GameManager.Instance.GameState = GameState.Game;
			//GameManager.Instance.SetMatchState(MatchState.ThrowDice);
		}
	}

}
