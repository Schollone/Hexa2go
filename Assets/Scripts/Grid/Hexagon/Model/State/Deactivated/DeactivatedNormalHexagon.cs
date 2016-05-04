using System;
using UnityEngine;

namespace Hexa2Go {

	public class DeactivatedNormalHexagon : AbstractDeactivatedHexagon {
		public DeactivatedNormalHexagon (IHexagonModel hexagon):base(hexagon) {
		}

		#region IHexagonState implementation		
		public override void MarkAsFocusable () {
			_hexagon.State = new DeactivatedFocusableHexagon (_hexagon);
		}

		public override void MarkAsFocused () {
			_hexagon.State = new DeactivatedFocusedHexagon (_hexagon);
		}
		#endregion
	}
}

