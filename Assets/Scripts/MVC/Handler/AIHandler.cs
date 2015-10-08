using UnityEngine;
using System;

namespace Hexa2Go {
	public class AIHandler {

		public AIHandler () {
			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {			
			if (GameManager.Instance.PlayerState == PlayerState.Player) {
				return;
			}

			PlayerState playerState = GameManager.Instance.PlayerState;

			switch (nextMatchState) {
				case MatchState.ThrowDice: 
					{
						Debug.Log ("ThrowDice AIHandler");
						GameManager.Instance.MatchState = MatchState.Throwing;
						Debug.Log ("ThrowDice AIHandler ENDE");
						break;
					}
				case MatchState.SelectCharacter:
					{
						Debug.Log ("SelectCharacter AIHandler");
						//GameManager.Instance.MatchState = MatchState.FocusCharacterTarget;
						Debug.Log ("SelectCharacter AIHandler ENDE");
						break;
					}
				case MatchState.FocusCharacterTarget:
					{
						Debug.Log ("FocusHexagonTarget AIHandler");
						IHexagonController selectedHexagon = GameManager.Instance.GridHandler.SelectedHexagon;
						ICharacterController selectedCharacter = GameManager.Instance.GridHandler.SelectedCharacter;
						//Debug.LogWarning(selectedHexagon + " - " + selectedCharacter);

						if (selectedHexagon != null && selectedCharacter != null) {
							GridPos targetPos = GameManager.Instance.GridHandler.HexagonHandler.GetTarget (selectedCharacter.Model.TeamColor).Model.GridPos;
							GridPos nextPos = GameManager.Instance.GridHandler.HexagonHandler.GetNextHexagonToFocus (selectedHexagon.Model.GridPos, targetPos);
							IHexagonController focusedHexagon = GameManager.Instance.GridHandler.HexagonHandler.FocusedHexagon;
							while (!GameManager.Instance.GridHandler.HexagonHandler.FocusedHexagon.Model.GridPos.Equals(nextPos)) {
								//Debug.Log("FocusNextHexagon " + nextPos);
								GameManager.Instance.GridHandler.FocusNextHexagon ();
							}

							GameManager.Instance.MatchState = MatchState.Moving;
						}

						Debug.Log ("FocusHexagonTarget AIHandler ENDE");
						break;
					}
				case MatchState.SelectHexagon:
					{
						Debug.Log ("SelectHexagon AIHandler");
						GameManager.Instance.MatchState = MatchState.FocusHexagonTarget;
						Debug.Log ("SelectHexagon AIHandler ENDE");
						break;
					}
				case MatchState.FocusHexagonTarget:
					{
						Debug.Log ("FocusHexagonTarget AIHandler");
						GameManager.Instance.MatchState = MatchState.Moving;
						Debug.Log ("FocusHexagonTarget AIHandler ENDE");
						break;
					}
			}
		}

		public void Unregister () {
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
		}
	}
}

