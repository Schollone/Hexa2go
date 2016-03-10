using System;
using UnityEngine;

namespace Hexa2Go {

	public static class HexagonFacade {

		public static void SelectHexagon (IHexagonModel hexagon) {
			Debug.Log("Facade SelectHexagon " + hexagon.State.ToString());
			hexagon.State.MarkAsSelected ();

		}

		public static void AcceptSelectedHexagon (IHexagonModel hexagon) {

		}

		public static void RejectSelectedHexagon (IHexagonModel hexagon) {

		}

		public static void FocusHexagon (IHexagonModel hexagon) {
			hexagon.State.MarkAsFocused ();
		}

		public static void MoveHexagon (IHexagonModel hexagon) {

		}
	}
}

