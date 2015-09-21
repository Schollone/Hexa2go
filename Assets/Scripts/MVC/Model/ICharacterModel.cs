using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {

	public class CharacterValueChangedEventArgs : EventArgs {
		public GridPos GridPos;
		
		public CharacterValueChangedEventArgs() {}
	}

	public interface ICharacterModel {

		event EventHandler<CharacterValueChangedEventArgs> OnSelectionChanged;
		event EventHandler<CharacterValueChangedEventArgs> OnGridPosChanged;
		event EventHandler<CharacterValueChangedEventArgs> OnTargetReached;

		bool IsSelected { get; set; }

		bool IsInGame { get; }

		TeamColor TeamColor { get; }

		CharacterPosition CharacterPosition { get; set; }

		GridPos GridPos { get; set; }

		void Select();

		void Deselect();

		void Remove();
	}

}