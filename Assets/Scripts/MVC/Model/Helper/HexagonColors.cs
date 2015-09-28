using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class HexagonColors {

		public static Color LIGHT_GRAY = new Color (0.8f, 0.8f, 0.8f);
		public static Color WHITE = Color.white;
		public static Color RED = new Color (0.6f, 0f, 0f);
		//public static Color RED = Color.red;
		public static Color BLUE = Color.blue;
		public static Color GREEN = Color.green;
		public static Color ORANGE = new Color (1f, 0.68f, 0f);

		public static Color GetColor (TeamColor teamColor, Color? defaultColor = null) {
			if (teamColor == TeamColor.NONE && defaultColor == null) {
				return HexagonColors.LIGHT_GRAY;
			}
			Color color = (teamColor == TeamColor.BLUE) ? HexagonColors.BLUE : HexagonColors.RED;
			return color;
		}
	}

}