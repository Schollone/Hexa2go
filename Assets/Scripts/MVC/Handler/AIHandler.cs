using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {
	public class AIHandler {

		public AIHandler () {
			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {	
			PlayerState playerState = GameManager.Instance.PlayerState;
			if (playerState == PlayerState.Player) {
				return;
			}

			switch (nextMatchState) {
				case MatchState.ThrowDice: 
					{
						Debug.Log ("ThrowDice AIHandler");
						GameManager.Instance.MatchState = MatchState.Throwing;
						Debug.Log ("ThrowDice AIHandler ENDE");
						break;
					}
				case MatchState.FocusCharacterTarget:
					{
						Debug.Log ("FocusHexagonTarget AIHandler");
						GameManager.Instance.GridHandler.FocusNextHexagon ();

						IHexagonController selectedHexagon = GameManager.Instance.GridHandler.SelectedHexagon;
						ICharacterController selectedCharacter = GameManager.Instance.GridHandler.SelectedCharacter;
						//Debug.LogWarning(selectedHexagon + " - " + selectedCharacter);

						if (selectedHexagon != null && selectedCharacter != null) {
							GridPos targetPos = GameManager.Instance.GridHandler.HexagonHandler.GetTarget (selectedCharacter.Model.TeamColor).Model.GridPos;
							Nullable<GridPos> nextPos = GameManager.Instance.GridHandler.HexagonHandler.GetNextHexagonToFocus (selectedHexagon.Model.GridPos, targetPos);
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
						GameManager.Instance.MatchState = MatchState.FocusHexagonTarget;
						break;
					}
				case MatchState.FocusHexagonTarget:
					{
						Debug.Log ("FocusHexagonTarget AIHandler");

						GameManager.Instance.GridHandler.FocusNextHexagon (true);
					
						ICollection<ICharacterController> collection = GameManager.Instance.GridHandler.CharacterHandler_P2.Characters.Values;
						ICharacterController[] characters = new ICharacterController[collection.Count];
						collection.CopyTo (characters, 0);
					
						Nullable<GridPos> nextPos = null;
						foreach (ICharacterController character in characters) {
							nextPos = GameManager.Instance.GridHandler.HexagonHandler.Strategy_1 (character);
							if (nextPos != null) {
								GameManager.Instance.GridHandler.SelectedHexagon = GameManager.Instance.GridHandler.HexagonHandler.Get (character.Model.GridPos);
								break;
							}
						}
					
						if (nextPos != null) {
							GameManager.Instance.GridHandler.HexagonHandler.SetHexagonToPosition ((GridPos)nextPos);
						} // else { // Strategy 2
						/* foreach (ICharacterController character in characters) {
								nextPos = GameManager.Instance.GridHandler.HexagonHandler.Strategy_2 (character);
								if (nextPos != null) {
									GameManager.Instance.GridHandler.SelectedHexagon = GameManager.Instance.GridHandler.HexagonHandler.Get (character.Model.GridPos);
									break;
								}
							}*/
						//}

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

