using System;
using UnityEngine;

namespace Hexa2Go {

	public abstract class AbstractDecoratorHexagon : ActivatedHexagon {

		protected IHexagonState _state; 
		
		public AbstractDecoratorHexagon (IHexagonState state):base(state.HexagonModel) {
			_state = state;
		}

		public override Color AreaColor {
			get {
				return _state.AreaColor;
			}
		}
		public override Color BorderColor {
			get {
				return (TeamColor == TeamColor.NONE) ? HexagonColors.LIGHT_GRAY : HexagonColors.GetColor (TeamColor);
			}
		}
		public override bool IsActivated {
			get {
				return _state.IsActivated;
			}
		}
		public override bool IsHome {
			get {
				return _state.IsHome;
			}
		}
		public override TeamColor TeamColor {
			get {
				return _state.TeamColor;
			}
			set {
				_state.TeamColor = value;
			}
		}

	}
}

