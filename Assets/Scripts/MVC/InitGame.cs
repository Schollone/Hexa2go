using UnityEngine;
using System.Collections;
using Hexa2Go;

public class InitGame : MonoBehaviour {
	
	private GameManager g;

	void Awake () {
		g = GameManager.Instance;
	}

	// Use this for initialization
	void Start () {
		if (GameManager.Instance.GameState == GameState.NullState) {
			GameManager.Instance.GameState = GameState.MainMenu;
			GameManager.Instance.MatchState = MatchState.NullState;
		}
	}

	void Update () {
		if (g.MatchStateChanged) {
			g.MatchStateChanged = false;
			g.InitNextMatchState ();
		}
		if (g.PlayerStateChanged) {
			g.PlayerStateChanged = false;
			g.InitNextPlayerState ();
		}
	}

	void OnLevelWasLoaded (int level) {
		Debug.LogWarning ("Loaded Scene: " + level);

		if (level == 0) {
			GameManager.Instance.MatchState = MatchState.NullState;
			GameManager.Instance.GameState = GameState.MainMenu;

		}
		
		if (level == 1) {
			GameManager.Instance.GameState = GameState.Match;
		}
	}

}
