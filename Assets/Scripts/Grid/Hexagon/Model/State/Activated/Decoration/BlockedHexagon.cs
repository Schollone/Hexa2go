using System;
using UnityEngine;

namespace Hexa2Go {

	public class BlockedHexagon : AbstractDecoratorState {
		public BlockedHexagon (IHexagonState state):base(state) {
		}

		#region IHexagonState implementation
		public override Color BorderColor {
			get {
				return HexagonColors.BLACK;
			}
		}
		#endregion
	}
}

