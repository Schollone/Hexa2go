using System;
using UnityEngine;

namespace Hexa2Go {

	public class Moving : AbstractMatchState {

		public override void Operate (IPlayer player) {
			UIHandler.Instance.DicesController.Hide ();
			UIHandler.Instance.AcceptController.View.Hide ();
			UIHandler.Instance.DicesController.ResetDicesBackground();

			player.Moving ();

			GameManager.Instance.GridFacade.ResetField ();

			if (UIHandler.Instance.DicesController.Double) {
				GameManager.Instance.GridFacade.MoveHexagon ();
			} else {
				GameManager.Instance.GridFacade.MoveCharacter ();
			}

			GameManager.Instance.GridFacade.ResetSelectionInfos();
		}

		public override MatchStates GetNextState () {
			GameManager.Instance.GetGameMode ().SwitchPlayer();
			return MatchStates.ThrowDice;
		}

	}
}

