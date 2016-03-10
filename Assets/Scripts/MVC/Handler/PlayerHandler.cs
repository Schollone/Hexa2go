using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {
	public class PlayerHandler {

		public PlayerHandler () {
			//GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			/*PlayerState playerState = GameManager.Instance.PlayerState;
			GameMode gameMode = GameManager.Instance.GameModeHandler.GameMode;
			if (gameMode == GameMode.Singleplayer && playerState == PlayerState.Opponent) {
				return;
			}


			switch (nextMatchState) {
				case MatchState.FocusCharacterTarget:
					{
						GameManager.Instance.GridHandler.FocusNextHexagon ();
						break;
					}
				case MatchState.SelectHexagon:
					{
						HexagonHandler hexagonHandler = GameManager.Instance.GridHandler.HexagonHandler;
						hexagonHandler.InitSelectableHexagons ();
						IHexagonController selectedHexagon = hexagonHandler.SelectNextHexagon ();
						GameManager.Instance.GridHandler.SelectedHexagon = selectedHexagon;
						hexagonHandler.InitNeighbors (selectedHexagon.Model.GridPos, true, true);
						hexagonHandler.TintFocusableNeighbors ();
						break;
					}
				case MatchState.FocusHexagonTarget:
					{
						GameManager.Instance.GridHandler.FocusNextHexagon (true);
						break;
					}
			}*/
		}

		public void Unregister () {
			//GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
		}
	}

}