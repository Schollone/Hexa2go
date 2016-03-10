using System;
using UnityEngine;

namespace Hexa2Go {

	public class DeactivatedNormalHexagon : DeactivatedHexagon {
		public DeactivatedNormalHexagon (IHexagonModel hexagon):base(hexagon) {
		}

		#region IHexagonState implementation		
		public void MarkAsFocusable () {
			_hexagon.State = new DeactivatedPlaceableHexagon (_hexagon);
		}
		#endregion
	}
}

