using System;

namespace Hexa2Go {
	public class FinishHexagonMoveCommand : IClickCommand {

		public void Execute (object data) {
			GameManager.Instance.GetGameMode().SwitchToNextMatchState();
		}
	}
}

