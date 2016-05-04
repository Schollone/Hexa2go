using System;

namespace Hexa2Go {
	public class ThrowDiceCommand : IClickCommand {
		public ThrowDiceCommand () {
		}

		public void Execute (object data) {
			GameManager.Instance.GetGameMode ().SwitchToNextState ();
		}
	}
}

