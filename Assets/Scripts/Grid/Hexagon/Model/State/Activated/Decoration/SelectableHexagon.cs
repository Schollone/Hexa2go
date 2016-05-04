using System;
using UnityEngine;

namespace Hexa2Go {

	public class SelectableHexagon : AbstractDecoratorState {
		public SelectableHexagon (IHexagonState state):base(state) {
		}

		#region IHexagonState implementation
		public override Color BorderColor {
			get {
				return HexagonColors.ORANGE;
			}
		}
		#endregion
	}
}

