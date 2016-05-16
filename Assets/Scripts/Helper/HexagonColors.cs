using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class HexagonColors {

		public static Color LIGHT_GRAY = new Color (0.6f, 0.6f, 0.6f);
		public static Color WHITE = Color.white;
		public static Color RED = new Color (0.6f, 0f, 0f);
		public static Color DICE_RED = new Color (0.39f, 0f, 0f);
		public static Color BLUE = new Color (0f, 0f, 0.6f);
		public static Color DICE_BLUE = new Color (0f, 0f, 0.4f);
		public static Color GREEN = Color.green;
		public static Color ORANGE = new Color (1f, 0.68f, 0f);
		public static Color BLACK = Color.black;

		public static Color GetColor (TeamColor teamColor, Color? defaultColor = null) {
			if (teamColor == TeamColor.NONE && defaultColor == null) {
				return HexagonColors.LIGHT_GRAY;
			}
			Color color = (teamColor == TeamColor.BLUE) ? HexagonColors.BLUE : HexagonColors.RED;
			return color;
		}
	}

}