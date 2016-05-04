using System;
using UnityEngine;

namespace Hexa2Go {

	public class ActivatedNeutralHexagon : AbstractActivatedHexagon {
		public ActivatedNeutralHexagon (IHexagonModel hexagon):base(hexagon) {
		}

		#region IHexagonState implementation
		public override void MarkAsNormal () {
			_hexagon.State = new NormalHexagon (new ActivatedNeutralHexagon (_hexagon));
		}

		public override void MarkAsFocusable () {
			_hexagon.State = new FocusableHexagon (new ActivatedNeutralHexagon (_hexagon));
		}
		
		public override void MarkAsFocused () {
			_hexagon.State = new FocusedHexagon (new ActivatedNeutralHexagon (_hexagon));
		}

		public override void MarkAsSelectable () {
			_hexagon.State = new SelectableHexagon (new ActivatedNeutralHexagon (_hexagon));
		}
		
		public override void MarkAsSelected () {
			_hexagon.State = new SelectedHexagon (new ActivatedNeutralHexagon (_hexagon));
		}
		
		public override void MarkAsBlocked () {
			_hexagon.State = new BlockedHexagon (new ActivatedNeutralHexagon (_hexagon));
		}
		
		public override void MarkAsMoveable () {
			_hexagon.State = new MoveableHexagon (new ActivatedNeutralHexagon (_hexagon));
		}
		#endregion

	}
}

