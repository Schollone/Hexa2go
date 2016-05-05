using System;
using UnityEngine;

namespace Hexa2Go {
	public class AbstractMatchState : IMatchState {
		public AbstractMatchState () {
		}

		public virtual void Operate (IPlayer player) {
			//throw new NotImplementedException ();
		}

		public virtual void OnExitState (IPlayer player) {

		}

		public virtual void HandleClick (IHexagonController hexagon) {
			//throw new NotImplementedException ();
		}

		public virtual MatchStates GetNextState () {
			//throw new NotImplementedException ();
			return MatchStates.NullState;
		}

		/*public void UpdateData () {
			UpdateGUI ();
			//UpdateAI ();
			UpdateGrid ();
		}*/

		/*public virtual void OnHexagonActivationChange (IHexagonController hexagonController) {
			Debug.LogWarning("Don't use! " + this.GetType());
			if (hexagonController.Model.IsActivated) {
				Color color = HexagonColors.GetColor (hexagonController.Model.TeamColor);
				hexagonController.View.Activate (color, true);
			} else {
				hexagonController.View.Deactivate (null, true);
			}
		}*/

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

