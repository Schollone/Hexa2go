using System;
using UnityEngine;

namespace Hexa2Go {

	public class Throwing : AbstractMatchState {
		public Throwing () {
		}

		public override void Operate (IPlayer player) {
			UIHandler.Instance.DicesController.Show ();
			UIHandler.Instance.DicesController.Disable ();
			UIHandler.Instance.DicesController.StartThrow ();
			UIHandler.Instance.AcceptController.View.Hide ();
			UIHandler.Instance.HintController.View.UpdateHint ("");
			
			player.Throwing ();
		}

	}
}

