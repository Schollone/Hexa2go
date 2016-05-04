using System;
using UnityEngine;

namespace Hexa2Go {

	public class DeactivatedFocusedHexagon : AbstractDeactivatedHexagon {
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

		public override void MarkAsNormal () {
			_hexagon.State = new DeactivatedNormalHexagon (_hexagon);
		}

		public override void MarkAsFocusable () {
			_hexagon.State = new DeactivatedFocusableHexagon (_hexagon);
		}
		#endregion
	}
}

