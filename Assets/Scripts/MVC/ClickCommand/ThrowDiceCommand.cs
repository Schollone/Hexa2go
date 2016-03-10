using System;

namespace Hexa2Go {
	public class ThrowDiceCommand : IClickCommand {
		public ThrowDiceCommand () {
		}

		public void Execute () {
			GameManager.Instance.GameModeHandler.GetGameMode ().SwitchToNextState ();
		}
	}
}

