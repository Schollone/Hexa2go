using System;

namespace Hexa2Go {
	public class ThrowDiceCommand : IClickCommand {

		public void Execute (object data) {
			GameManager.Instance.GetGameMode ().SetMatchState (MatchStates.Throwing);
		}
	}
}

