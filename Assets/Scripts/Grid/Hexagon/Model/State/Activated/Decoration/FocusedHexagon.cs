using System;
using UnityEngine;

namespace Hexa2Go {

	public class FocusedHexagon : AbstractDecoratorState {
		public FocusedHexagon (IHexagonState state):base(state) {
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
		#endregion
	}
}

