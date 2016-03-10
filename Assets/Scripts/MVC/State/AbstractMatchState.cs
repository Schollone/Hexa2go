using System;
using UnityEngine;

namespace Hexa2Go {
	public class AbstractMatchState : IMatchState {
		public AbstractMatchState () {
		}

		public virtual void Operate (IPlayer player) {
			throw new NotImplementedException ();
		}

		public virtual void HandleClick(IHexagonModel hexagon) {
			throw new NotImplementedException ();
		}

		public virtual IMatchState GetNextState () {
			throw new NotImplementedException ();
		}

		/*public void UpdateData () {
			UpdateGUI ();
			//UpdateAI ();
			UpdateGrid ();
		}*/

		public virtual void OnHexagonActivationChange (IHexagonController hexagonController) {
			if (hexagonController.Model.IsActivated) {
				Color color = HexagonColors.GetColor (hexagonController.Model.TeamColor);
				hexagonController.View.Activate (color, true);
			} else {
				hexagonController.View.Deactivate (true);
			}
		}

		/*protected void UpdateGUI () {
		}

		protected void UpdateAIThrowDice () {
			GameManager.Instance.GameModeHandler.GetGameMode ().UpdateAIThrowDice ();
		}

		protected void UpdateAIFocusCharacterTarget () {
			GameManager.Instance.GameModeHandler.GetGameMode ().UpdateAIFocusCharacterTarget ();
		}

		protected void UpdateGrid () {
		}*/
	}
}

