using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public class HexagonValueChangedEventArgs : EventArgs {
		public GridPos GridPos;
		public TeamColor TeamColor;
		public bool IsSelected;

		public HexagonValueChangedEventArgs() {}
	}

	public interface IHexagonModel {

		event EventHandler<HexagonValueChangedEventArgs> OnSelectionChanged;
		event EventHandler<HexagonValueChangedEventArgs> OnFocusChanged;
		event EventHandler<HexagonValueChangedEventArgs> OnActivationChanged;
		event EventHandler<HexagonValueChangedEventArgs> OnDeclaredTargetChanged;

		GridPos GridPos { get; }

		bool IsField { get; set; }

		IList<GridPos> Neighbors { get; }
		
		bool IsFocusable { get; }

		void Activate();

		void DeclareTarget(TeamColor teamColor);

		void Select();

		void Deselect();
		

		/*GameObject gameObject;

		GridPos gridPos;

		bool isField;

		bool canReceiveHexagon;

		IList<IHexagonModel> neighbors;

		int neighborIndex;

		Color defaultAreaColor;

		Color defaultBorderColor;

		bool isSelected;

		bool isFocusable;

		bool isFocused;

		ICharacterModel character1;

		ICharacterModel character2;

		bool hasMoveableCharacter;

		bool hasCharacterWithTeamColor(TeamColor teamColor);

		ICharacterModel getCharacterWithTeamColor(TeamColor teamColor);

		bool canReceiveCharacter;

		bool isBlocked;

		bool isTarget;

		TeamColor teamColor;

		bool hasEmptyCharacter1Slot();

		bool hasEmptyCharacter2Slot();

		void changeAreaColor(Color color);

		void changeBorderColor(Color color);*/

		string ToString ();

	}

}