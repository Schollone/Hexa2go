using System;
using UnityEngine;

namespace Hexa2Go {

	public class SelectHexagon : AbstractMatchState {
		public SelectHexagon () {
		}

		public override void Operate (IPlayer player) {
			GameManager.Instance.UIHandler.DicesController.Show ();
			GameManager.Instance.UIHandler.DicesController.Disable ();
			GameManager.Instance.UIHandler.NextCharacterController.View.Hide ();
			
			player.SelectHexagon ();
		}

		public override IMatchState GetNextState () {
			IMatchState state = null;
			GameManager.Instance.GameModeHandler.GetGameMode ().GetStateMap ().TryGetValue (MatchStates.FocusHexagonTargetSingleplayer, out state);
			return state;
		}

		/*
		public void OnClickAccept () {
			GameManager.Instance.SetCurrentMatchState (new FocusHexagonTarget ());
		}

		public void OnClickNextHexagonTarget () {
			GameManager.Instance.GridHandler.SelectNextHexagon ();
		}

		public void OnClickPrevHexagonTarget () {
			GameManager.Instance.GridHandler.SelectPrevHexagon ();
		}*/

	}
}

