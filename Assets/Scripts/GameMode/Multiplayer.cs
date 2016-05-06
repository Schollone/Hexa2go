using System;
using UnityEngine;

namespace Hexa2Go {

	public class Multiplayer : AbstractGameMode {

		public Multiplayer ():base(2) {
			Debug.Log ("Multiplayer");
		}

		public override void Init () {
			stateMap.Clear ();
			stateMap.Add (MatchStates.NullState, new NullState ());
			stateMap.Add (MatchStates.ThrowDice, new ThrowDice ());
			stateMap.Add (MatchStates.Throwing, new Throwing ());
			stateMap.Add (MatchStates.SelectCharacter, new SelectCharacter ());
			stateMap.Add (MatchStates.SelectHexagon, new SelectHexagon ());
			stateMap.Add (MatchStates.Moving, new Moving ());
			stateMap.Add (MatchStates.GameOver, new GameOverMP ());

			SetMatchState (MatchStates.NullState);
			
			players [0] = new Player (TeamColor.RED, 1);
			players [1] = new Player (TeamColor.BLUE, 2);
			
			base.Init ();
		}

		/*public void Init () {
			base.Init ();

			String namePlayerOne = LocalizationManager.GetText (TextIdentifier.PLAYER_1.ToString ());
			String namePlayerTwo = LocalizationManager.GetText (TextIdentifier.PLAYER_2.ToString ());
			GameManager.Instance.UIHandler.PlayerController_One = new PlayerController (TeamColor.RED, namePlayerOne);
			GameManager.Instance.UIHandler.PlayerController_Two = new PlayerController (TeamColor.BLUE, namePlayerTwo);
		}
		
		public void Unregister () {
			base.Unregister ();
		}
		
		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			base.HandleOnMatchStateChange (prevMatchState, nextMatchState);

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
			GameManager.Instance.UIHandler.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_THROW_DICE.ToString ()));
			GameManager.Instance.UIHandler.DicesController.Enable ();
		}

		public void UpdateSelectCharacterGUI () {
			GameManager.Instance.UIHandler.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_SELECT_CHARACTER.ToString ()));
			GameManager.Instance.UIHandler.NextCharacterController.View.Show ();
			GameManager.Instance.UIHandler.AcceptController.View.Show ();
		}

		public void UpdateFocusCharacterTargetGUI () {
			GameManager.Instance.UIHandler.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_FOCUS_CHARACTER_TARGET.ToString ()));
			GameManager.Instance.UIHandler.PrevHexagonController.View.Show ();
			GameManager.Instance.UIHandler.NextHexagonController.View.Show ();
			GameManager.Instance.UIHandler.AcceptController.View.Show ();
		}

		public void UpdateSelectHexagonGUI () {
			GameManager.Instance.UIHandler.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_SELECT_HEXAGON.ToString ()));
			GameManager.Instance.UIHandler.PrevHexagonController.View.Show ();
			GameManager.Instance.UIHandler.NextHexagonController.View.Show ();
			GameManager.Instance.UIHandler.AcceptController.View.Show ();
		}

		public void UpdateGameOverGUI () {
			PlayerState playerState = GameManager.Instance.PlayerState;
			if (playerState == PlayerState.Player) {
				Color color = HexagonColors.GetColor (GameManager.Instance.UIHandler.PlayerController_One.Model.TeamColor);
				GameManager.Instance.UIHandler.PlayerController_One.View.UpdatePlayer (color, LocalizationManager.GetText (TextIdentifier.WON.ToString ()));
			} else if (playerState == PlayerState.Opponent) {
				Color color = HexagonColors.GetColor (GameManager.Instance.UIHandler.PlayerController_Two.Model.TeamColor);
				GameManager.Instance.UIHandler.PlayerController_Two.View.UpdatePlayer (color, LocalizationManager.GetText (TextIdentifier.WON.ToString ()));
			}
		}*/
	}
}

