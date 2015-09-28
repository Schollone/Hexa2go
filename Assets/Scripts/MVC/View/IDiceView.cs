using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {

	public class DiceThrowedEventArgs : EventArgs {
		public DiceObject DiceObject;
		public DiceThrowedEventArgs (DiceObject diceObject) {
			this.DiceObject = diceObject;
		}
	}

	public interface IDiceView : IButtonView {

		event EventHandler<DiceThrowedEventArgs> OnThrowed;
		
		void UpdateView (CharacterType characterType, TeamColor teamColor);

		DiceObject UpdateViewByIndex (int characterTypeIndex, int teamColorIndex);

		MonoBehaviour This { get; }

		void StartThrow ();

	}

}