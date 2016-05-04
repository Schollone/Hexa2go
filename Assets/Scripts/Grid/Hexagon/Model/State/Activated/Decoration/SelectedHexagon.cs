using System;
using UnityEngine;

namespace Hexa2Go {

	public class SelectedHexagon : AbstractDecoratorState {
		public SelectedHexagon (IHexagonState state):base(state) {
		}

		#region IHexagonState implementation
		public override Color AreaColor {
			get {
				return HexagonColors.ORANGE;
			}
		}
		#endregion
	}
}

