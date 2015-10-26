using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {
	public class PlayerHandler {

		public PlayerHandler () {
			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			PlayerState playerState = GameManager.Instance.PlayerState;
			GameMode gameMode = GameManager.Instance.GameModeHandler.GameMode;
			if (gameMode == GameMode.Singleplayer && playerState == PlayerState.Opponent) {
				return;
			}


			switch (nextMatchState) {
				case MatchState.SelectCharacter:
					{
						Debug.Log ("SelectCharacter PlayerHandler");
						//GameManager.Instance.MatchState = MatchState.FocusCharacterTarget;
						Debug.Log ("SelectCharacter PlayerHandler ENDE");
						break;
					}
				case MatchState.FocusCharacterTarget:
					{
						Debug.Log ("FocusHexagonTarget PlayerHandler");

						GameManager.Instance.GridHandler.FocusNextHexagon ();
					
						Debug.Log ("FocusHexagonTarget PlayerHandler ENDE");
						break;
					}
				case MatchState.SelectHexagon:
					{
						Debug.Log ("SelectHexagon PlayerHandler");

						HexagonHandler hexagonHandler = GameManager.Instance.GridHandler.HexagonHandler;
						hexagonHandler.InitSelectableHexagons ();
						IHexagonController selectedHexagon = hexagonHandler.SelectNextHexagon ();
						GameManager.Instance.GridHandler.SelectedHexagon = selectedHexagon;
						hexagonHandler.InitNeighbors (selectedHexagon.Model.GridPos, true, true);
						hexagonHandler.TintFocusableNeighbors ();
						//GameManager.Instance.MatchState = MatchState.FocusHexagonTarget;
						Debug.Log ("SelectHexagon PlayerHandler ENDE");
						break;
					}
				case MatchState.FocusHexagonTarget:
					{
						Debug.Log ("FocusHexagonTarget PlayerHandler");

						GameManager.Instance.GridHandler.FocusNextHexagon (true);

						Debug.Log ("FocusHexagonTarget PlayerHandler ENDE");
						break;
					}
			}
		}

		public void Unregister () {
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
		}
	}

}