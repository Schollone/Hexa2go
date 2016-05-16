using System;
using UnityEngine;

namespace Hexa2Go {

	public abstract class AbstractDecoratorState : AbstractActivatedHexagon {

		protected IHexagonState _state; 
		
		public AbstractDecoratorState (IHexagonState state):base(state.HexagonModel) {
			_state = state;
		}

		public override Color AreaColor {
			get {
				return _state.AreaColor;
			}
		}
		public override Color BorderColor {
			get {
				return _state.BorderColor;
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
		}

		public override void MarkAsNormal () {
			_state.MarkAsNormal();
		}
		
		public override void MarkAsFocusable () {
			_state.MarkAsFocusable();
		}
		
		public override void MarkAsFocused () {
			_state.MarkAsFocused();
		}
		
		public override void MarkAsSelectable () {
			_state.MarkAsSelectable();
		}
		
		public override void MarkAsSelected () {
			_state.MarkAsSelected();
		}
		
		public override void MarkAsBlocked () {
			_state.MarkAsBlocked();
		}
		
		public override void MarkAsMoveable () {
			_state.MarkAsMoveable();
		}

		public virtual void MarkAsNeutral () {
			_state.MarkAsNeutral();
		}
		
		public virtual void MarkAsHome (TeamColor teamColor) {
			_state.MarkAsHome(teamColor);
		}

	}
}

