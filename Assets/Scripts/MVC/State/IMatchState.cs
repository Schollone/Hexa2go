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
		IMatchState GetNextState ();
		void HandleClick(IHexagonModel hexagon);

		void OnHexagonActivationChange (IHexagonController hexagonController);
	}
}