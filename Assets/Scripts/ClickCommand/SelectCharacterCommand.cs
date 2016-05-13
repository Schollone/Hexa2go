using UnityEngine;
using System;

namespace Hexa2Go {
	public class SelectCharacterCommand : IClickCommand {

		public void Execute (object data) {
			DiceObject diceObject = (DiceObject) data;

			foreach (ICharacterController controller in GameManager.Instance.GridFacade.CharacterFacade.GetCharactersByDices()) {
				if (controller.Model.Type == diceObject.CharacterType) {
					//ClickHandler.Instance.OnClick (ClickTypes.SelectCharacter, diceObject);
					GameManager.Instance.GridFacade.SelectCharacter(diceObject.CharacterType, GameManager.Instance.GetGameMode().CurrentPlayer.Model.TeamColor);
					break;
				}
			}

			//GameManager.Instance.GridFacade.SelectCharacter(diceObject.CharacterType, GameManager.Instance.GetGameMode().CurrentPlayer.Model.TeamColor);
			//GameManager.Instance.GetGameMode ().SwitchToNextState ();
		}
	}
}

