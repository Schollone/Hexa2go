using UnityEngine;
using System.Collections;
using Hexa2Go;

namespace Hexa2Go {

	public class InitGame : MonoBehaviour {
	
		private GameManager g;

		void Awake () {
			g = GameManager.Instance;
		}

		// Use this for initialization
		void Start () {
			if (GameManager.Instance.GameState == GameState.NullState) {
				GameManager.Instance.GameState = GameState.MainMenu;
			}
		}

		void Update () {
			if (g.MatchStateChanged) {
				g.MatchStateChanged = false;
				g.InitNextMatchState ();
			}
		}

		void OnLevelWasLoaded (int level) {
			if (level == 0) {
				GameManager.Instance.GameState = GameState.MainMenu;
			}
		
			if (level == 1) {
				GameManager.Instance.GameState = GameState.Match;
			}
		}

	}

}