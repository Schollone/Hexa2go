using System;
using UnityEngine;

namespace Hexa2Go {

	public class Throwing : AbstractMatchState {
		public Throwing () {
		}
		
		/*public void OnClick () { // what Click?
			GameManager.Instance.MatchState = MatchState.Throwing;
		}*/

		public override void Operate (IPlayer player) {
			GameManager.Instance.UIHandler.DicesController.Show ();
			GameManager.Instance.UIHandler.DicesController.Disable ();
			GameManager.Instance.UIHandler.DicesController.StartThrow ();
			GameManager.Instance.UIHandler.PrevHexagonController.View.Hide ();
			GameManager.Instance.UIHandler.NextHexagonController.View.Hide ();
			GameManager.Instance.UIHandler.NextCharacterController.View.Hide ();
			GameManager.Instance.UIHandler.AcceptController.View.Hide ();
			GameManager.Instance.UIHandler.HintController.View.UpdateHint ("");
			
			player.ThrowDice ();
		}

		public override IMatchState GetNextState () {
			IMatchState state = null;
			//GameManager.Instance.GameModeHandler.GetGameMode ().GetStateMap ().TryGetValue (MatchStates.ThrowDiceSingleplayer, out state);
			return state;
		}

	}
}

