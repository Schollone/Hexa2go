using System;
using UnityEngine;

namespace Hexa2Go {

	public class GameOver : AbstractMatchState {
		public GameOver () {
		}

		public override void Operate (IPlayer player) {
			UIHandler.Instance.DicesController.Hide ();
			UIHandler.Instance.AcceptController.View.Show ();
			
			player.GameOver ();
			Debug.Log ("GameOver: " + player);

			Color color = HexagonColors.GetColor (player.Model.TeamColor);
			if (player.Equals(GameManager.Instance.GetGameMode().GetPlayers()[0])) {
				player.View.UpdatePlayer (color, LocalizationManager.GetText (TextIdentifier.WON.ToString ()));
			} else {
				player.View.UpdatePlayer (color, LocalizationManager.GetText (TextIdentifier.LOSE.ToString ()));
			}

		}

		public override MatchStates GetNextState () {
			return MatchStates.NullState;
		}

	}
}

