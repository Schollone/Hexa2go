using System;
using UnityEngine;

namespace Hexa2Go {

	public class SelectedHexagon : AbstractDecoratorHexagon {
		public SelectedHexagon (IHexagonState state):base(state) {

		}

		#region IHexagonState implementation
		public override Color AreaColor {
			get {
				return HexagonColors.ORANGE;
			}
		}

		public override void MarkAsSelected () {
			Debug.Log("MarkAsSelected SelectedHexagon: " + TeamColor);
			_state.MarkAsSelected();
		}
		#endregion
	}
}

