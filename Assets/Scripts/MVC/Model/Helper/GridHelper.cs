using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public static class GridHelper {

		//HexagonTree<GridPos> _tree;

		public enum OffsetPosition {
			TopLeft,
			TopRight,
			BottomLeft,
			BottomRight
		}

		public static Vector3 HexagonPosition (GridPos gridPos) {
			float yOffset = 4.75f;
			float zValue = -9.5f * gridPos.y;
			if ((gridPos.x & 1) == 1) {
				zValue -= yOffset;
			}
			return new Vector3 (8.25f * gridPos.x, 0, zValue);
		}

		public static Vector3 CharacterOffset (OffsetPosition position) {
			float xOffset = 0f;
			float zOffset = 0f;

			switch (position) {
				case OffsetPosition.TopLeft:
					{
						xOffset = -1.6f;
						zOffset = 1.6f;
						break;
					}
				case OffsetPosition.TopRight:
					{
						xOffset = 1.6f;
						zOffset = 1.6f;
						break;
					}
				case OffsetPosition.BottomLeft:
					{
						xOffset = -1.6f;
						zOffset = -1.6f;
						break;
					}
				case OffsetPosition.BottomRight:
					{
						xOffset = 1.6f;
						zOffset = -1.6f;
						break;
					}
			}

			return new Vector3 (xOffset, 0f, zOffset);
		}
	}

}