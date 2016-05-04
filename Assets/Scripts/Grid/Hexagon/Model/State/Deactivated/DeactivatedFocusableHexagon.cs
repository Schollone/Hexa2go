using System;
using UnityEngine;

namespace Hexa2Go {

	public class DeactivatedFocusableHexagon : AbstractDeactivatedHexagon {
		public DeactivatedFocusableHexagon (IHexagonModel hexagon):base(hexagon) {
		}

		#region IHexagonState implementation
		public override Color BorderColor {
			get {
				return HexagonColors.GREEN;
			}
		}

		public override void MarkAsNormal () {
			_hexagon.State = new DeactivatedNormalHexagon (_hexagon);
		}
		
		public override void MarkAsFocused () {
			_hexagon.State = new DeactivatedFocusedHexagon (_hexagon);
		}
		#endregion
	}
}

