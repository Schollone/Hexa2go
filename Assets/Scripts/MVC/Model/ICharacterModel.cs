using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {

	public class CharacterValueChangedEventArgs : EventArgs {
		public GridPos GridPos;
		public TeamColor TeamColor;
		public bool IsSelected;
		
		public CharacterValueChangedEventArgs() {}
	}

	public interface ICharacterModel {

		event EventHandler<CharacterValueChangedEventArgs> OnSelectionChanged;

		GameObject gameObject { get; }

		bool isSelected { get; set; }

		TeamColor teamColor { get; }

		CharacterPosition characterPosition { get; set; }


		string ToString();

		void Select();

		void Deselect();

		GridPos GridPos { get; set; }
	}

}