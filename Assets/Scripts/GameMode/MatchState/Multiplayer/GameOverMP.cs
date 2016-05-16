using System;
using UnityEngine;

namespace Hexa2Go {

	public class GameOverMP : AbstractMatchState {

		public override void Operate (IPlayer player) {
			UIHandler.Instance.DicesController.Hide ();
			UIHandler.Instance.AcceptController.View.Show ();
			
			player.GameOver ();

			Color color = HexagonColors.GetColor (player.Model.TeamColor);
			player.View.UpdatePlayer (color, LocalizationManager.GetText (TextIdentifier.WON.ToString ()));

		}

		public override MatchStates GetNextState () {
			return MatchStates.NullState;
		}

	}
}

