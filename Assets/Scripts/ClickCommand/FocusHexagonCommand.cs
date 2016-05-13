using System;

namespace Hexa2Go {
	public class FocusHexagonCommand : IClickCommand {

		public void Execute (object data) {
			ContainerObject container = (ContainerObject) data;

			GridPos gridPos = container.GridPos;
			IPlayer player = (IPlayer) container.Object1;
			GameManager.Instance.GridFacade.HexagonFacade.FocusHexagon (gridPos, player);
		}
	}
}

