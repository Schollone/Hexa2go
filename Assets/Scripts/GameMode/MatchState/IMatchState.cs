using System;

namespace Hexa2Go {
	public interface IMatchState {
		/*void UpdateData ();
		//void OnClick ();
		void OnClickAccept ();
		void OnClickDice (IDiceController diceController);
		void OnClickHexagon ();
		void OnClickNextCharacter ();
		void OnClickNextHexagon ();
		void OnClickPrevHexagon ();

		void OnHexagonActivationChange (IHexagonController hexagonController);
		*/
		void Operate (IPlayer player);
		void OnExitState (IPlayer currentPlayer);
		MatchStates GetNextState ();
		void HandleClick (IHexagonController hexagon);

		//void OnHexagonActivationChange (IHexagonController hexagonController);
	}
}