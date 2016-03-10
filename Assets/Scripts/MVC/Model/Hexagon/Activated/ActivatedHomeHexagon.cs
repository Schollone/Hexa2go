using System;
using UnityEngine;

namespace Hexa2Go {

	public class ActivatedHomeHexagon : ActivatedHexagon {
		public ActivatedHomeHexagon (IHexagonModel hexagon, TeamColor teamColor):base(hexagon) {
			_teamColor = teamColor;
		}

		#region IHexagonState implementation
		public override bool IsHome {
			get {
				return true;
			}
		}
		public override TeamColor TeamColor {
			get {
				Debug.Log ("TeamColor ActivatedHomeHexagon: " + _teamColor);
				return _teamColor;
			}
			set {
				_teamColor = value;
			}
		}
		
		public override void MarkAsNormal () {
			_hexagon.State = new ActivatedNormalHexagon (_hexagon);
		}
		
		public override void MarkAsFocusable () {
			_hexagon.State = new FocusableHexagon (new ActivatedHomeHexagon (_hexagon, _teamColor));
		}
		
		public override void MarkAsFocused () {
			_hexagon.State = new FocusedHexagon (new ActivatedHomeHexagon (_hexagon, _teamColor));
		}
		
		public override void MarkAsSelected () {
			Debug.Log("MarkAsSelected ActivatedHomeHexagon");
			_hexagon.State = new SelectedHexagon (new ActivatedHomeHexagon (_hexagon, _teamColor));
		}
		
		public override void MarkAsBlocked () {
			_hexagon.State = new BlockedHexagon (new ActivatedHomeHexagon (_hexagon, _teamColor));
		}
		
		public override void MarkAsMoveable () {
			_hexagon.State = new MoveableHexagon (new ActivatedHomeHexagon (_hexagon, _teamColor));
		}
		#endregion
	}
}

