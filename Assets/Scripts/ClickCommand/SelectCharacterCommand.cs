using UnityEngine;
using System;

namespace Hexa2Go {
	public class SelectCharacterCommand : IClickCommand {
		public SelectCharacterCommand () {
		}

		public void Execute (object data) {
			DiceObject diceObject = (DiceObject) data;

			GameManager.Instance.GridFacade.SelectCharacter(diceObject.CharacterType, GameManager.Instance.GetGameMode().CurrentPlayer.Model.TeamColor);
			//GameManager.Instance.GetGameMode ().SwitchToNextState ();
		}
	}
}

