using System;
using UnityEngine;

namespace Hexa2Go {

	public class FocusHexagonTarget : AbstractMatchState {
		public FocusHexagonTarget () {
		}

		public override void Operate (IPlayer player) {
			GameManager.Instance.UIHandler.DicesController.Show ();
			GameManager.Instance.UIHandler.DicesController.Disable ();
			GameManager.Instance.UIHandler.NextCharacterController.View.Hide ();

			GameManager.Instance.UIHandler.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_FOCUS_HEXAGON_TARGET.ToString ()));
			GameManager.Instance.UIHandler.PrevHexagonController.View.Show ();
			GameManager.Instance.UIHandler.NextHexagonController.View.Show ();
			GameManager.Instance.UIHandler.AcceptController.View.Show ();
			
			player.FocusHexagonTarget ();
		}

		public override IMatchState GetNextState () {
			IMatchState state = null;
			GameManager.Instance.GameModeHandler.GetGameMode ().GetStateMap ().TryGetValue (MatchStates.MovingSingleplayer, out state);
			return state;
		}

		/*
		public void OnClickAccept () {
			GameManager.Instance.SetCurrentMatchState (new Moving ());
		}

		public void OnClickNextHexagonTarget () {
			GameManager.Instance.GridHandler.FocusNextHexagon (true);
		}

		public void OnClickPrevHexagonTarget () {
			GameManager.Instance.GridHandler.FocusPrevHexagon (true);
		}*/
	}
}

