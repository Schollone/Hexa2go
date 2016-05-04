using System;
using UnityEngine;

namespace Hexa2Go {

	public class FocusableHexagon : AbstractDecoratorState {
		public FocusableHexagon (IHexagonState state):base(state) {
		}

		#region IHexagonState implementation}
		public override Color BorderColor {
			get {
				return HexagonColors.GREEN;
			}
		}
		#endregion
	}
}

