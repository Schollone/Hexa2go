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

		event EventHandler<EventArgs> OnUpdatedData;

		event EventHandler<HexagonValueChangedEventArgs> OnSelectionChanged;
		event EventHandler<HexagonValueChangedEventArgs> OnFocusChanged;
		event EventHandler<HexagonValueChangedEventArgs> OnActivationChanged;

		GridPos GridPos { get; }

		IHexagonState State { get; set; }

		IList<GridPos> Neighbors { get; }








		bool IsActivated { get; }

		bool IsSelected { get; }

		bool IsTarget { get; }

		bool Visited { get; set; }

		TeamColor TeamColor { get; }
		
		bool IsFocusableForCharacter { get; }

		void Activate (bool ignoreView = false, TeamColor teamColor = TeamColor.NONE);

		void Deactivate (bool ignoreView = false);

		void Select ();

		void Deselect ();

	}

}