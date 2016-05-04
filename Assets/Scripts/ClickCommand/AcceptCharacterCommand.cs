using System;

namespace Hexa2Go {
	public class AcceptCharacterCommand : IClickCommand {
		public AcceptCharacterCommand () {
		}

		public void Execute (object data) {
			GameManager.Instance.GetGameMode().SwitchToNextState();
		}
	}
}
