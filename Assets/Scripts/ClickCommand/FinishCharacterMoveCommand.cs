using System;

namespace Hexa2Go {
	public class FinishCharacterMoveCommand : IClickCommand {

		public void Execute (object data) {
			GameManager.Instance.GetGameMode().SwitchToNextMatchState();
		}
	}
}

