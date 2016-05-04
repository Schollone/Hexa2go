using System;
using UnityEngine;

namespace Hexa2Go {

	public interface IHexagonState {

		Color AreaColor { get; }
		Color BorderColor { get; }
		bool IsActivated { get; }
		bool IsHome { get; }
		TeamColor TeamColor { get; }
		IHexagonModel HexagonModel { get; }

		void Activate (TeamColor teamColor = TeamColor.NONE, bool dontPropagate = false);
		void Deactivate (bool dontPropagate = false);
		void MarkAsNormal ();
		void MarkAsFocusable ();
		void MarkAsFocused ();
		void MarkAsSelectable ();
		void MarkAsSelected ();
		void MarkAsBlocked ();
		void MarkAsMoveable ();
		void MarkAsNeutral ();
		void MarkAsHome (TeamColor teamColor);

	}
}

