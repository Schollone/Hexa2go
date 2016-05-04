using System;
using UnityEngine;

namespace Hexa2Go {

	public class MoveableHexagon : AbstractDecoratorState {
		public MoveableHexagon (IHexagonState state):base(state) {
		}

		#region IHexagonState implementation
		public override Color AreaColor {
			get {
				return HexagonColors.GREEN;
			}
		}
		#endregion
	}
}

