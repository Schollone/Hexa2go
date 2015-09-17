using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {

	public class DiceModel : IDiceModel {

		private TeamColor _teamColor;
		private CharacterType _characterType;
		
		public DiceModel() {}
		
		public TeamColor TeamColor {
			get {
				return _teamColor;
			}
		}
		
		public CharacterType CharacterType {
			get {
				return _characterType;
			}
		}
		
		public void SetDiceValue(DiceObject diceObject) {

			_characterType = diceObject.CharacterType;
			_teamColor = diceObject.TeamColor;
			DiceValueChangedEventArgs eventArgs = new DiceValueChangedEventArgs(diceObject);
			OnDiceValueChanged(this, eventArgs);
		}
		
		public event EventHandler<DiceValueChangedEventArgs> OnDiceValueChanged = (sender, e) => {};

	}

}