using System;
using UnityEngine;

namespace Hexa2Go {

	public class NullState : AbstractMatchState {
		public NullState () {
		}

		/*public void UpdateData () {
			//UpdateGUI ();
			//UpdateAI ();
			//UpdateGrid ();
		}*/

		public override void Operate (IPlayer player) {
			//player.ThrowDiceGUI ();
		}

		public override MatchStates GetNextState () {
			return MatchStates.ThrowDice;
		}

		/*public void OnHexagonActivationChange (IHexagonController hexagonController) {
			if (hexagonController.Model.IsActivated) {
				Color color = HexagonColors.GetColor (hexagonController.Model.TeamColor);
				hexagonController.View.Activate (color);
				/*if (GameManager.Instance.MatchState == MatchState.NullState) {

				} else {
					hexagonController.View.Activate (color, true);
				}*/
				
		/*} else {
				hexagonController.View.Deactivate ();
				/*if (GameManager.Instance.MatchState == MatchState.NullState) {
				} else {
					hexagonController.View.Deactivate (true);
				}*/
		/*}
		}*/
	}

}