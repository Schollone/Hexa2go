using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hexa2Go {
	public enum AIType {
		Constructive,
		Destructive,
		Mixed
	}

	public delegate bool StrategyDelegate ();

	public class AIHandler {

		private AIType _aiType;
		private static System.Random r = new System.Random ();

		public AIHandler () {

			_aiType = AIType.Constructive;
			int random = r.Next (0, 3);
			_aiType = (AIType)random;

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
						GameManager.Instance.MatchState = MatchState.Throwing;
						break;
					}
				case MatchState.FocusCharacterTarget:
					{
						GameManager.Instance.GridHandler.FocusNextHexagon ();

						HexagonHandler hexagonHandler = GameManager.Instance.GridHandler.HexagonHandler;

						IHexagonController selectedHexagon = GameManager.Instance.GridHandler.SelectedHexagon;
						ICharacterController selectedCharacter = GameManager.Instance.GridHandler.SelectedCharacter;

						//Debug.LogWarning(selectedHexagon + " - " + selectedCharacter);

						if (selectedHexagon != null && selectedCharacter != null) {
							GridPos targetPos = hexagonHandler.GetTarget (selectedCharacter.Model.TeamColor).Model.GridPos;
							Nullable<GridPos> nextPos = hexagonHandler.GetNextHexagonToFocus (selectedHexagon.Model.GridPos, targetPos);
							//Debug.LogWarning (nextPos);

							IHexagonController hexagon = hexagonHandler.Get ((GridPos)nextPos);

							if (!hexagon.Model.IsFocusableForCharacter) { // if neighborHexagon is blocked
								Debug.Log ("Counterclockwise");
								nextPos = hexagonHandler.GetNextHexagonToFocus (selectedHexagon.Model.GridPos, targetPos, true);
								hexagon = hexagonHandler.Get ((GridPos)nextPos);
							
							}
						
							if (!hexagon.Model.IsFocusableForCharacter) { // if neighborHexagon is blocked
								Debug.Log ("Letzter Ausweg");
								GameManager.Instance.GridHandler.FocusNextHexagon ();

							} else {

								while (!hexagonHandler.FocusedHexagon.Model.GridPos.Equals(nextPos)) {
									GameManager.Instance.GridHandler.FocusNextHexagon ();
								}
							}
							GameManager.Instance.MatchState = MatchState.Moving;
						}

						break;
					}
				case MatchState.SelectHexagon:
					{
						GameManager.Instance.MatchState = MatchState.FocusHexagonTarget;
						break;
					}
				case MatchState.FocusHexagonTarget:
					{
						//GameManager.Instance.GridHandler.FocusNextHexagon (true);

						HexagonHandler hexagonHandler = GameManager.Instance.GridHandler.HexagonHandler;
					
						ICollection<ICharacterController> opponentCollection = GameManager.Instance.GridHandler.CharacterHandler_P2.Characters.Values;
						ICharacterController[] opponentCharacters = new ICharacterController[opponentCollection.Count];
						opponentCollection.CopyTo (opponentCharacters, 0);

						ICollection<ICharacterController> playerCollection = GameManager.Instance.GridHandler.CharacterHandler_P1.Characters.Values;
						ICharacterController[] playerCharacters = new ICharacterController[playerCollection.Count];
						playerCollection.CopyTo (playerCharacters, 0);

						opponentCharacters = hexagonHandler.SortCharacterByDistance (opponentCharacters);
						playerCharacters = hexagonHandler.SortCharacterByDistance (playerCharacters);

						List<StrategyDelegate> l = new List<StrategyDelegate> ();
						l.Add (() => Strategy (opponentCharacters, true, true));
						l.Add (() => Strategy (opponentCharacters, false, true));
						l.Add (() => Strategy (playerCharacters, true));
						l.Add (() => Strategy (playerCharacters, false));

						if (_aiType == AIType.Destructive) {
							l = new List<StrategyDelegate> ();
							l.Add (() => Strategy (playerCharacters, true));
							l.Add (() => Strategy (playerCharacters, false));
							l.Add (() => Strategy (opponentCharacters, true, true));
							l.Add (() => Strategy (opponentCharacters, false, true));
						} else if (_aiType == AIType.Mixed) {
							GridHelper.Shuffle (l);
						}
						l.Add (() => Strategy (null));

						foreach (StrategyDelegate action in l) {
							bool hasResult = action ();
							if (hasResult) {
								break;
							}
						}

						GameManager.Instance.MatchState = MatchState.Moving;
						break;
					}
			}
		}

		public void Unregister () {
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
		}

		private bool Strategy (ICharacterController[] characters, bool fromCharacterToTarget = true, bool checkForShortDistance = false) {
			Nullable<GridPos> nextPos = null;
			HexagonHandler hexagonHandler = GameManager.Instance.GridHandler.HexagonHandler;

			if (characters != null) {

				for (int i = 0; i < characters.Length; i++) {
					ICharacterController character = characters [i];

					nextPos = fromCharacterToTarget ? hexagonHandler.StrategyFromCharacterToTarget (character, checkForShortDistance) : hexagonHandler.StrategyFromTargetToCharacter (character, checkForShortDistance);

					if (nextPos != null) {
						GameManager.Instance.GridHandler.HexagonHandler.SetHexagonToPosition ((GridPos)nextPos);
						return true;
					}
				}
			} else {
				GridHandler gridHandler = GameManager.Instance.GridHandler;
				IHexagonController selectedHexagon = null;
				hexagonHandler.InitSelectableHexagons ();
				ArrayList selectableHexagons = hexagonHandler.GetSelectableHexagons ();

				foreach (IHexagonController hexagon in selectableHexagons) {
					if (hexagon.Model.IsTarget) {
						continue;
					}
					GridPos gridPos = hexagon.Model.GridPos;
					List<ICharacterController> list = (List<ICharacterController>)gridHandler.CharacterHandler_P1.GetCharacters (gridPos);
					List<ICharacterController> list2 = (List<ICharacterController>)gridHandler.CharacterHandler_P2.GetCharacters (gridPos);
					list.AddRange (list2);
					if (list.Count <= 0) {
						selectedHexagon = hexagon;
						break;
					}
				}
				foreach (GridPos neighborPos in selectedHexagon.Model.Neighbors) {
					IHexagonController neighbor = hexagonHandler.Get (neighborPos);
					if (hexagonHandler.IsFocusableForHexagon (selectedHexagon, neighbor)) {
						hexagonHandler.SetHexagonToPosition (neighborPos);
						break;
					}
				}
				GameManager.Instance.GridHandler.SelectedHexagon = selectedHexagon;
			}

			return false;
		}
	}
}

