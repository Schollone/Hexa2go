using System;
using UnityEngine;

namespace Hexa2Go {

	public class NullState : AbstractMatchState {

		public override void Operate (IPlayer player) {
			//player.ThrowDiceGUI ();
		}

		public override MatchStates GetNextState () {
			return MatchStates.ThrowDice;
		}
	}

}