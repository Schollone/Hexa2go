using System;
using UnityEngine;

namespace Hexa2Go {

	public class Moving : AbstractMatchState {
		public Moving () {
		}

		public override void Operate (IPlayer player) {			
			player.Moving ();
		}

		public override IMatchState GetNextState () {
			IMatchState state = null;
			GameManager.Instance.GameModeHandler.GetGameMode ().GetStateMap ().TryGetValue (MatchStates.ThrowDiceSingleplayer, out state);
			return state;
		}

	}
}

