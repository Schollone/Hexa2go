using System;

namespace Hexa2Go {

	public interface IMatchState {
		void Operate (IPlayer player);
		void OnExitState (IPlayer currentPlayer);
		MatchStates GetNextState ();
		void HandleClick (IHexagonController hexagon);
	}
}