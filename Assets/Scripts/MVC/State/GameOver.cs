using System;
using UnityEngine;

namespace Hexa2Go {

	public class GameOver : AbstractMatchState {
		public GameOver () {
		}

		public override void Operate (IPlayer player) {
			GameManager.Instance.UIHandler.DicesController.Hide ();
			GameManager.Instance.UIHandler.PrevHexagonController.View.Hide ();
			GameManager.Instance.UIHandler.NextHexagonController.View.Hide ();
			GameManager.Instance.UIHandler.NextCharacterController.View.Hide ();
			GameManager.Instance.UIHandler.AcceptController.View.Show ();
			
			player.GameOver ();
		}

		public override IMatchState GetNextState () {
			IMatchState state = null;
			//GameManager.Instance.GameModeHandler.GetGameMode ().GetStateMap ().TryGetValue (MatchStates, out state);
			return state;
		}

		/*
		public void OnClickAccept () {
			Application.LoadLevel (0);
		}*/

	}
}

