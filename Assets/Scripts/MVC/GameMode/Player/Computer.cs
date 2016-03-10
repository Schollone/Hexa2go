using UnityEngine;
using System;

namespace Hexa2Go {
	
	public class Computer : AbstractPlayer {
		public Computer () {
			_model = new PlayerModel (TeamColor.BLUE, (LocalizationManager.GetText (TextIdentifier.COMPUTER.ToString ())));
		}
		
		public override void ThrowDice () {
			Color color = HexagonColors.GetColor (GameManager.Instance.GameModeHandler.GetGameMode ().GetPlayers () [1].Model.TeamColor);
			String name = GameManager.Instance.GameModeHandler.GetGameMode ().GetPlayers () [1].Model.Name;
			GameManager.Instance.GameModeHandler.GetGameMode ().GetPlayers () [1].View.UpdatePlayer (color, name);

			//this.OnClick (ClickTypes.ThrowDice);
			ClickHandler.Instance.OnClick (ClickTypes.ThrowDice);
			Debug.Log ("Throw Dice Computer");
		}
		
		public override void Throwing () {
			Debug.Log ("Throwing Computer");
		}
		
		public override void SelectCharacter () {
			GameManager.Instance.UIHandler.DicesController.Disable ();
			GameManager.Instance.UIHandler.NextCharacterController.View.Hide ();
			GameManager.Instance.UIHandler.AcceptController.View.Hide ();
			Debug.Log ("SelectCharacter Computer");
		}
		
		public override void FocusCharacterTarget () {
			Debug.Log ("FocusCharacterTarget Computer");
		}
		
		public override void SelectHexagon () {
			//this.OnClick (ClickTypes.SelectHexagon);
			//this.OnClick (ClickTypes.AcceptHexagon);
			Debug.Log ("SelectHexagon Computer");
		}
		
		public override void FocusHexagonTarget () {
			Debug.Log ("FocusHexagonTarget Computer");
		}
		
		public override void Moving () {
			Debug.Log ("Moving Computer");
		}
		
		public override void GameOver () {
			Debug.Log ("GameOver Computer");
		}
	}
}

