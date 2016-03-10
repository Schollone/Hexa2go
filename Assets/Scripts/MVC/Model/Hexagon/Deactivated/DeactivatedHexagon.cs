using System;
using UnityEngine;

namespace Hexa2Go {

	public abstract class DeactivatedHexagon : IHexagonState {

		protected IHexagonModel _hexagon;

		public DeactivatedHexagon (IHexagonModel hexagon) {
			_hexagon = hexagon;
		}


		#region IHexagonState implementation
		public virtual Color AreaColor {
			get {
				return (TeamColor == TeamColor.NONE) ? HexagonColors.WHITE : HexagonColors.GetColor (TeamColor);
			}
		}
		public virtual Color BorderColor {
			get {
				return (TeamColor == TeamColor.NONE) ? HexagonColors.WHITE : HexagonColors.GetColor (TeamColor);
			}
		}
		public virtual bool IsActivated {
			get {
				return false;
			}
		}
		public virtual bool IsHome {
			get {
				return false;
			}
		}
		public virtual TeamColor TeamColor {
			get {
				return TeamColor.NONE;
			}
			set {

			}
		}
		public IHexagonModel HexagonModel {
			get {
				return _hexagon;
			}
		}

		public void Activate () {
			_hexagon.State = new ActivatedNormalHexagon (_hexagon);
		}

		public void Deactivate () {
		}

		public void MarkAsNormal () {
		}

		public void MarkAsFocusable () {
		}

		public void MarkAsFocused () {
		}

		public void MarkAsSelected () {
		}

		public void MarkAsBlocked () {
		}

		public void MarkAsMoveable () {
		}

		public void MarkAsHome (TeamColor teamColor) {
		}

		#endregion
	}
}

