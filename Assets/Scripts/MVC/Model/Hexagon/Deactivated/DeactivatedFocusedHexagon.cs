using System;
using UnityEngine;

namespace Hexa2Go {

	public class DeactivatedFocusedHexagon : DeactivatedHexagon {
		public DeactivatedFocusedHexagon (IHexagonModel hexagon):base(hexagon) {
		}

		#region IHexagonState implementation
		public override Color AreaColor {
			get {
				return HexagonColors.GREEN;
			}
		}
		public override Color BorderColor {
			get {
				return HexagonColors.GREEN;
			}
		}

		public void MarkAsNormal () {
			_hexagon.State = new DeactivatedNormalHexagon (_hexagon);
		}

		public void MarkAsFocusable () {
			_hexagon.State = new DeactivatedPlaceableHexagon (_hexagon);
		}
		#endregion
	}
}

