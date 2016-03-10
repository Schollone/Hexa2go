using System;
using UnityEngine;

namespace Hexa2Go {

	public class FocusCharacterTarget : AbstractMatchState {
		public FocusCharacterTarget () {
		}

		public override void Operate (IPlayer player) {
			GameManager.Instance.UIHandler.DicesController.Show ();
			GameManager.Instance.UIHandler.DicesController.Disable ();
			GameManager.Instance.UIHandler.NextCharacterController.View.Hide ();
			
			player.FocusCharacterTarget ();
		}

		public override IMatchState GetNextState () {
			IMatchState state = null;
			GameManager.Instance.GameModeHandler.GetGameMode ().GetStateMap ().TryGetValue (MatchStates.MovingSingleplayer, out state);
			return state;
		}
		
		/*public void OnClick () { // what Click?
			GameManager.Instance.MatchState = MatchState.Throwing;
		}*/
		/*
		private void UpdateGUI () {
			GameManager.Instance.UIHandler.DicesController.Show ();
			GameManager.Instance.UIHandler.DicesController.Disable ();
			GameManager.Instance.UIHandler.NextCharacterController.View.Hide ();

			GameManager.Instance.GameModeHandler.GetGameMode ().UpdateFocusCharacterTargetGUI ();
		}

		public void OnClickAccept () {
			GameManager.Instance.SetCurrentMatchState (new Moving ());
		}

		public void OnClickNextHexagonTarget () {
			GameManager.Instance.GridHandler.FocusNextHexagon ();
		}

		public void OnClickPrevHexagonTarget () {
			GameManager.Instance.GridHandler.FocusPrevHexagon ();
		}

		private void UpdatePlayers () {
			GameManager.Instance.GameModeHandler.GetGameMode ().UpdateAI ();
		}*/

	}
}

