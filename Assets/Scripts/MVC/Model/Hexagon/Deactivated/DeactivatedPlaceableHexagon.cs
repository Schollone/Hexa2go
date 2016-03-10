using System;
using UnityEngine;

namespace Hexa2Go {

	public class DeactivatedPlaceableHexagon : DeactivatedHexagon {
		public DeactivatedPlaceableHexagon (IHexagonModel hexagon):base(hexagon) {
		}

		#region IHexagonState implementation
		public override Color BorderColor {
			get {
				return HexagonColors.GREEN;
			}
		}
		
		public void MarkAsFocused () {
			_hexagon.State = new DeactivatedFocusedHexagon (_hexagon);
		}
		#endregion
	}
}

