using System;
using UnityEngine;

namespace Hexa2Go {

	public abstract class ActivatedHexagon : IHexagonState {

		protected IHexagonModel _hexagon;
		protected TeamColor _teamColor;

		public ActivatedHexagon (IHexagonModel hexagon) {
			_hexagon = hexagon;
		}

		#region IHexagonState implementation
		public virtual Color AreaColor {
			get {
				return (TeamColor == TeamColor.NONE) ? HexagonColors.LIGHT_GRAY : HexagonColors.GetColor (TeamColor);
			}
		}
		public virtual Color BorderColor {
			get {
				Debug.Log ("BorderColor ActivatedHexagon");
				return (TeamColor == TeamColor.NONE) ? HexagonColors.LIGHT_GRAY : HexagonColors.GetColor (TeamColor);
			}
		}
		public virtual bool IsActivated {
			get {
				return true;
			}
		}
		public virtual bool IsHome {
			get {
				return false;
			}
		}
		public virtual TeamColor TeamColor {
			get {
				Debug.Log ("TeamColor virtual: " + TeamColor.NONE);
				return TeamColor.NONE;
			}
			set {
				_teamColor = value;
			}
		}
		public IHexagonModel HexagonModel {
			get {
				return _hexagon;
			}
		}

		public virtual void Activate () {
		}
		
		public virtual void Deactivate () {
			_hexagon.State = new DeactivatedNormalHexagon (_hexagon);
		}
		
		public virtual void MarkAsNormal () {
		}
		
		public virtual void MarkAsFocusable () {
		}
		
		public virtual void MarkAsFocused () {
		}
		
		public virtual void MarkAsSelected () {
			Debug.Log("MarkAsSelected virtual");
		}
		
		public virtual void MarkAsBlocked () {
		}
		
		public virtual void MarkAsMoveable () {
		}
		
		public virtual void MarkAsHome (TeamColor teamColor) {
		}
		#endregion
	}
}

