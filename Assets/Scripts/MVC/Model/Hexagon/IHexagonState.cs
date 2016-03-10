using System;
using UnityEngine;

namespace Hexa2Go {

	public interface IHexagonState {

		Color AreaColor { get; }
		Color BorderColor { get; }
		bool IsActivated { get; }
		bool IsHome { get; }
		TeamColor TeamColor { get; set; }
		IHexagonModel HexagonModel { get; }

		void Activate ();
		void Deactivate ();
		void MarkAsNormal ();
		void MarkAsFocusable ();
		void MarkAsFocused ();
		void MarkAsSelected ();
		void MarkAsBlocked ();
		void MarkAsMoveable ();
		void MarkAsHome (TeamColor teamColor);

	}
}

