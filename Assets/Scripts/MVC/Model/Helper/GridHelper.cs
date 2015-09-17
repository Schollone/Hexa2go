using UnityEngine;
using System;

namespace Hexa2Go {

	public static class GridHelper {

		public static Vector3 HexagonPosition(GridPos gridPos) {
			float yOffset = 4.75f;
			float zValue = -9.5f * gridPos.y;
			if ( (gridPos.x & 1) == 1 ) {
				zValue -= yOffset;
			}
			return new Vector3(8.25f * gridPos.x, 0, zValue);
		}
	}

}