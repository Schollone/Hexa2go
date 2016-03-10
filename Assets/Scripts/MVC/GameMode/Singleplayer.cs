using System;
using UnityEngine;

namespace Hexa2Go {

	public class Singleplayer : AbstractGameMode {

		public Singleplayer ():base(2) {
			Debug.Log ("Singleplayer");

		}

		public override void Init () {
			stateMap.Clear ();
			stateMap.Add (MatchStates.NullStateSingleplayer, new NullState ());
			stateMap.Add (MatchStates.ThrowDiceSingleplayer, new ThrowDice ());
			stateMap.Add (MatchStates.ThrowingSingleplayer, new Throwing ());
			stateMap.Add (MatchStates.SelectCharacterSingleplayer, new SelectCharacter ());
			stateMap.Add (MatchStates.FocusCharacterTargetSingleplayer, new FocusCharacterTarget ());
			stateMap.Add (MatchStates.SelectHexagonSingleplayer, new SelectHexagon ());
			stateMap.Add (MatchStates.FocusHexagonTargetSingleplayer, new FocusHexagonTarget ());
			stateMap.Add (MatchStates.MovingSingleplayer, new Moving ());
			stateMap.Add (MatchStates.GameOverSingleplayer, new GameOver ());

			IMatchState state = null;
			stateMap.TryGetValue (MatchStates.NullStateSingleplayer, out state);
			SetMatchState (state);

			players [0] = new Player ();
			players [1] = new Computer ();

			base.Init ();
		}

		/*private AIHandler _aiHandler;

		public Singleplayer () {
		}

		public void Init () {
			_aiHandler = new AIHandler ();

			String namePlayerOne = LocalizationManager.GetText (TextIdentifier.PLAYER.ToString ());
			String namePlayerTwo = LocalizationManager.GetText (TextIdentifier.COMPUTER.ToString ());
			GameManager.Instance.UIHandler.PlayerController_One = new PlayerController (TeamColor.RED, namePlayerOne);
			GameManager.Instance.UIHandler.PlayerController_Two = new PlayerController (TeamColor.BLUE, namePlayerTwo);
		}

		public void Unregister () {
			base.Unregister ();
			if (_aiHandler != null) {
				_aiHandler.Unregister ();
			}
		}

		public void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			switch (nextMatchState) {
				case MatchState.SelectCharacter:
					{
						if (GameManager.Instance.GridHandler.SelectedCharacter != null) {
							GameManager.Instance.GridHandler.TintCharacter ();
						
							if (GameManager.Instance.PlayerState == PlayerState.Opponent) {
								GameManager.Instance.MatchState = MatchState.FocusCharacterTarget;
							}
						
						} else {
							SwitchToNextPlayer ();
						}
						break;
					
					}
				case MatchState.Moving:
					{
						if (GameManager.Instance.GameFinished) {
							GameManager.Instance.GameFinished = false;
							GameManager.Instance.MatchState = MatchState.Win;
						} else {
							SwitchToNextPlayer ();
						}
						break;
					
					}
			}


			PlayerState playerState = GameManager.Instance.PlayerState;
			if (playerState == PlayerState.Opponent) {
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
			}
			
		}

		public void UpdateThrowDiceGUI () {
			PlayerState playerState = GameManager.Instance.PlayerState;
			if (playerState == PlayerState.Player) {
				GameManager.Instance.UIHandler.DicesController.Enable ();
				GameManager.Instance.UIHandler.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_THROW_DICE.ToString ()));
			} else if (playerState == PlayerState.Opponent) {
				GameManager.Instance.UIHandler.DicesController.Disable ();
			}
		}

		public void UpdateSelectCharacterGUI () {
			PlayerState playerState = GameManager.Instance.PlayerState;
			if (playerState == PlayerState.Player) {
				GameManager.Instance.UIHandler.DicesController.Enable ();	
				GameManager.Instance.UIHandler.NextCharacterController.View.Show ();
				GameManager.Instance.UIHandler.AcceptController.View.Show ();
				GameManager.Instance.UIHandler.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_SELECT_CHARACTER.ToString ()));
			} else if (playerState == PlayerState.Opponent) {
				GameManager.Instance.UIHandler.DicesController.Disable ();
				GameManager.Instance.UIHandler.NextCharacterController.View.Hide ();
				GameManager.Instance.UIHandler.AcceptController.View.Hide ();
			}
		}

		public void UpdateFocusCharacterTargetGUI () {
			PlayerState playerState = GameManager.Instance.PlayerState;
			if (playerState == PlayerState.Opponent) {
				GameManager.Instance.UIHandler.PrevHexagonController.View.Hide ();
				GameManager.Instance.UIHandler.NextHexagonController.View.Hide ();
				GameManager.Instance.UIHandler.AcceptController.View.Hide ();
			}
		}

		public void UpdateSelectHexagonGUI () {
			PlayerState playerState = GameManager.Instance.PlayerState;
			if (playerState == PlayerState.Opponent) {
				GameManager.Instance.UIHandler.PrevHexagonController.View.Hide ();
				GameManager.Instance.UIHandler.NextHexagonController.View.Hide ();
				GameManager.Instance.UIHandler.AcceptController.View.Hide ();
			}
		}

		public void UpdateFocusHexagonTargetGUI () {
			PlayerState playerState = GameManager.Instance.PlayerState;
			if (playerState == PlayerState.Opponent) {
				GameManager.Instance.UIHandler.PrevHexagonController.View.Hide ();
				GameManager.Instance.UIHandler.NextHexagonController.View.Hide ();
				GameManager.Instance.UIHandler.AcceptController.View.Hide ();
			}
		}

		public void UpdateGameOverGUI () {
			PlayerState playerState = GameManager.Instance.PlayerState;
			if (playerState == PlayerState.Player) {
				Color color = HexagonColors.GetColor (GameManager.Instance.UIHandler.PlayerController_One.Model.TeamColor);
				GameManager.Instance.UIHandler.PlayerController_One.View.UpdatePlayer (color, LocalizationManager.GetText (TextIdentifier.WON.ToString ()));
			} else if (playerState == PlayerState.Opponent) {
				Color color = HexagonColors.GetColor (GameManager.Instance.UIHandler.PlayerController_One.Model.TeamColor);
				GameManager.Instance.UIHandler.PlayerController_One.View.UpdatePlayer (color, LocalizationManager.GetText (TextIdentifier.LOSE.ToString ()));
			}
		}

		public void UpdateAIThrowDice () {
			if (GameManager.Instance.PlayerState == PlayerState.Opponent) {
				GameManager.Instance.SetCurrentMatchState (new Throwing ());
			}
		}

		public void UpdateAIFocusCharacterTarget () {

		}*/

	}
}

