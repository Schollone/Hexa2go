using System;
using UnityEngine;

namespace Hexa2Go {

	public class ThrowDice : AbstractMatchState {
		public ThrowDice () {
		}

		public override void Operate (IPlayer player) {
			UIHandler.Instance.DicesController.Show ();
			UIHandler.Instance.AcceptController.View.Hide ();

			player.ThrowDice ();
		}

		public override MatchStates GetNextState () {
			return MatchStates.Throwing;
		}
	}

}