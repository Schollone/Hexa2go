using System;
using UnityEngine;

namespace Hexa2Go {

	public abstract class AbstractDeactivatedHexagon : IHexagonState {

		protected IHexagonModel _hexagon;

		public AbstractDeactivatedHexagon (IHexagonModel hexagon) {
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
		}
		public IHexagonModel HexagonModel {
			get {
				return _hexagon;
			}
		}

		public virtual void Activate (TeamColor teamColor = TeamColor.NONE, bool dontPropagate = false) {
			_hexagon.DontPropagate = dontPropagate;
			if (teamColor != TeamColor.NONE) {
				_hexagon.State = new ActivatedHomeHexagon (_hexagon, teamColor);
			} else {
				_hexagon.State = new ActivatedNeutralHexagon (_hexagon);
			}
		}

		public virtual void Deactivate (bool dontPropagate = false) {
		}

		public virtual void MarkAsNormal () {
		}

		public virtual void MarkAsFocusable () {
		}

		public virtual void MarkAsFocused () {
		}

		public virtual void MarkAsSelectable () {
		}

		public virtual void MarkAsSelected () {
		}

		public virtual void MarkAsBlocked () {
		}

		public virtual void MarkAsMoveable () {
		}

		public virtual void MarkAsNeutral () {
		}

		public virtual void MarkAsHome (TeamColor teamColor) {
		}

		#endregion
	}
}

