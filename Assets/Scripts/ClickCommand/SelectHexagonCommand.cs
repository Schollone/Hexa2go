using System;

namespace Hexa2Go {
	public class SelectHexagonCommand : IClickCommand {
		public SelectHexagonCommand () {
		}

		public void Execute (object data) {
			GameManager.Instance.GetGameMode ().SwitchToNextState ();
		}
	}
}

