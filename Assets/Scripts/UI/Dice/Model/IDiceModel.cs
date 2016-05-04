using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {

	public class DiceValueChangedEventArgs : EventArgs {
		public DiceObject DiceObject;
		public DiceValueChangedEventArgs (DiceObject diceObject) {
			this.DiceObject = diceObject;
		}
	}

	public interface IDiceModel {

		event EventHandler<DiceValueChangedEventArgs> OnDiceValueChanged;

		TeamColor TeamColor { get; }
		CharacterType CharacterType { get; }
		void SetDiceValue (DiceObject diceObject);
	}

}