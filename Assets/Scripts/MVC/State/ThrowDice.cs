using System;
using UnityEngine;

namespace Hexa2Go {

	public class ThrowDice : AbstractMatchState {
		public ThrowDice () {
		}

		public override void Operate (IPlayer player) {
			Debug.Log ("Operate!!!!");
			GameManager.Instance.UIHandler.DicesController.Show ();
			GameManager.Instance.UIHandler.PrevHexagonController.View.Hide ();
			GameManager.Instance.UIHandler.NextHexagonController.View.Hide ();
			GameManager.Instance.UIHandler.NextCharacterController.View.Hide ();
			GameManager.Instance.UIHandler.AcceptController.View.Hide ();

			player.ThrowDice ();
		}

		public override IMatchState GetNextState () {
			IMatchState state = null;
			GameManager.Instance.GameModeHandler.GetGameMode ().GetStateMap ().TryGetValue (MatchStates.ThrowingSingleplayer, out state);
			return state;
		}

		/*private void UpdateGUI () {
			GameManager.Instance.UIHandler.DicesController.Show ();
			GameManager.Instance.UIHandler.PrevHexagonController.View.Hide ();
			GameManager.Instance.UIHandler.NextHexagonController.View.Hide ();
			GameManager.Instance.UIHandler.NextCharacterController.View.Hide ();
			GameManager.Instance.UIHandler.AcceptController.View.Hide ();

			GameManager.Instance.GameModeHandler.GetGameMode ().UpdateThrowDiceGUI ();
			
			// Update display of player status
			PlayerState playerState = GameManager.Instance.PlayerState;
			if (playerState == PlayerState.Player) {
				Color color = HexagonColors.GetColor (GameManager.Instance.UIHandler.PlayerController_One.Model.TeamColor);
				GameManager.Instance.UIHandler.PlayerController_One.View.UpdatePlayer (color, GameManager.Instance.UIHandler.PlayerController_One.Model.Name);
			} else if (playerState == PlayerState.Opponent) {
				Color color = HexagonColors.GetColor (GameManager.Instance.UIHandler.PlayerController_Two.Model.TeamColor);
				GameManager.Instance.UIHandler.PlayerController_Two.View.UpdatePlayer (color, GameManager.Instance.UIHandler.PlayerController_Two.Model.Name);
			}

		}

		private void UpdatePlayers () {
			//GameManager.Instance.MatchState = MatchState.Throwing;

			GameManager.Instance.GameModeHandler.GetGameMode ().UpdateAI ();
		}

		private void UpdateGrid () {

		}

		public void OnClickDice (IDiceController diceController) {
			GameManager.Instance.SetCurrentMatchState (new Throwing ());
		}*/
	}

}