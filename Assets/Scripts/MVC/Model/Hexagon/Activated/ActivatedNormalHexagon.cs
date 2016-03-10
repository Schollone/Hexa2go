using System;
using UnityEngine;

namespace Hexa2Go {

	public class ActivatedNormalHexagon : ActivatedHexagon {
		public ActivatedNormalHexagon (IHexagonModel hexagon):base(hexagon) {
		}

		#region IHexagonState implementation		
		public override void MarkAsFocusable () {
			_hexagon.State = new FocusableHexagon (new ActivatedNormalHexagon (_hexagon));
		}
		
		public override void MarkAsFocused () {
			_hexagon.State = new FocusedHexagon (new ActivatedNormalHexagon (_hexagon));
		}
		
		public override void MarkAsSelected () {
			Debug.Log ("MarkAsSelected");
			_hexagon.State = new SelectedHexagon (new ActivatedNormalHexagon (_hexagon));
		}
		
		public override void MarkAsBlocked () {
			_hexagon.State = new BlockedHexagon (new ActivatedNormalHexagon (_hexagon));
		}
		
		public override void MarkAsMoveable () {
			_hexagon.State = new MoveableHexagon (new ActivatedNormalHexagon (_hexagon));
		}

		public override void MarkAsHome (TeamColor teamColor) {
			_hexagon.State = new ActivatedHomeHexagon (_hexagon, teamColor);
		}
		#endregion

	}
}

