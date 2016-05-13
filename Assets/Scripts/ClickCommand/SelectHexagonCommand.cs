using System;

namespace Hexa2Go {
	public class SelectHexagonCommand : IClickCommand {

		public void Execute (object data) {
			GridPos gridPos = (GridPos) data;
			GameManager.Instance.GridFacade.HexagonFacade.SelectHexagon (gridPos);
		}
	}
}

