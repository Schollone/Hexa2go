using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public class HexagonValueChangedEventArgs : EventArgs {

		public HexagonValueChangedEventArgs () {
		}
	}

	public interface IHexagonModel {

		event EventHandler<HexagonValueChangedEventArgs> OnSelectionChanged;
		event EventHandler<HexagonValueChangedEventArgs> OnFocusChanged;
		event EventHandler<HexagonValueChangedEventArgs> OnActivationChanged;

		GridPos GridPos { get; }

		bool IsActivated { get; }

		bool IsSelected { get; }

		bool IsTarget { get; }

		bool Visited { get; set; }

		TeamColor TeamColor { get; }

		IList<GridPos> Neighbors { get; }
		
		bool IsFocusableForCharacter { get; }

		bool IsFocusableForHexagon { get; }

		void Activate (bool ignoreView = false, TeamColor teamColor = TeamColor.NONE);

		void Deactivate (bool ignoreView = false);

		void Select ();

		void Deselect ();

	}

}