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
			UIHandler.Instance.DicesController.Show ();
			UIHandler.Instance.DicesController.Disable ();
			UIHandler.Instance.DicesController.StartThrow ();
			UIHandler.Instance.AcceptController.View.Hide ();
			UIHandler.Instance.HintController.View.UpdateHint ("");
			
			player.Throwing ();
		}

		public override MatchStates GetNextState () {
			//IMatchState state = null;
			//GameManager.Instance.GameModeHandler.GetGameMode ().GetStateMap ().TryGetValue (MatchStates.ThrowDiceSingleplayer, out state);
			return MatchStates.NullState;
		}

	}
}

