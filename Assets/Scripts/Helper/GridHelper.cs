using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Hexa2Go {

	public static class GridHelper {

		public const float ACTIVATED_Y_POS = -0.3f;
		public const float DEACTIVATED_Y_POS = 0f;

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
				case OffsetPosition.TOP_LEFT:
					{
						xOffset = -1.6f;
						zOffset = 1.6f;
						break;
					}
				case OffsetPosition.TOP_RIGHT:
					{
						xOffset = 1.6f;
						zOffset = 1.6f;
						break;
					}
				case OffsetPosition.BOTTOM_LEFT:
					{
						xOffset = -1.6f;
						zOffset = -1.6f;
						break;
					}
				case OffsetPosition.BOTTOM_RIGHT:
					{
						xOffset = 1.6f;
						zOffset = -1.6f;
						break;
					}
			}

			return new Vector3 (xOffset, 0f, zOffset);
		}

		public static Vector3 Bezier (Vector3 start, Vector3 bezier, Vector3 end, float t) {
			return (((1 - t) * (1 - t)) * start) + (2 * t * (1 - t) * bezier) + ((t * t) * end);
		}

		public static void Shuffle<T> (this IList<T> list) {
			RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider ();
			int n = list.Count;
			while (n > 1) {
				byte[] box = new byte[1];
				do {
					provider.GetBytes (box);
				} while (!(box[0] < n * (Byte.MaxValue / n)));
				int k = (box [0] % n);
				n--;
				T value = list [k];
				list [k] = list [n];
				list [n] = value;
			}
		}

		public static OffsetPosition GetOffsetPosition (IList<ICharacterModel> characters) {
			OffsetPosition offsetPosition;
			if (characters.Count > 0) {
				offsetPosition = characters [0].OffsetPosition;
				switch (offsetPosition) {
					case OffsetPosition.TOP_LEFT:
					{
						offsetPosition = OffsetPosition.BOTTOM_RIGHT;
						break;
					}
					case OffsetPosition.TOP_RIGHT:
					{
						offsetPosition = OffsetPosition.BOTTOM_LEFT;
						break;
					}
					case OffsetPosition.BOTTOM_LEFT:
					{
						offsetPosition = OffsetPosition.TOP_RIGHT;
						break;
					}
					case OffsetPosition.BOTTOM_RIGHT:
					{
						offsetPosition = OffsetPosition.TOP_LEFT;
						break;
					}
				}
			} else {
				int offsetPositionIndex = new System.Random ().Next (0, 4);
				offsetPosition = (OffsetPosition)offsetPositionIndex;
			}
			
			return offsetPosition;
		}
	}

}