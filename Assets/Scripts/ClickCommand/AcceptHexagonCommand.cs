using System;

namespace Hexa2Go {
	public class AcceptHexagonCommand : IClickCommand {
		public AcceptHexagonCommand () {
		}

		public void Execute (object data) {
			GameManager.Instance.GetGameMode().SwitchToNextState();
		}
	}
}

