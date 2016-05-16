using System;
using UnityEngine;

namespace Hexa2Go {
	public class AbstractMatchState : IMatchState {

		public virtual void Operate (IPlayer player) {
		}

		public virtual void OnExitState (IPlayer player) {
		}

		public virtual void HandleClick (IHexagonController hexagon) {
		}

		public virtual MatchStates GetNextState () {
			return MatchStates.NullState;
		}
	}
}

