using UnityEngine;
using System;

namespace Hexa2Go {
	
	public class Computer : AbstractPlayer {
		public Computer () {
			_model = new PlayerModel (TeamColor.BLUE, (LocalizationManager.GetText (TextIdentifier.COMPUTER.ToString ())));

			_model.OnMatchFinished += HandleOnMatchFinished;
		}
		
		public override void ThrowDice () {
			Color color = HexagonColors.GetColor (GameManager.Instance.GetGameMode ().GetPlayers () [1].Model.TeamColor);
			String name = GameManager.Instance.GetGameMode ().GetPlayers () [1].Model.Name;
			GameManager.Instance.GetGameMode ().GetPlayers () [1].View.UpdatePlayer (color, name);

			//this.OnClick (ClickTypes.ThrowDice);
			ClickHandler.Instance.OnClick (ClickTypes.ThrowDice);
			Debug.Log ("Throw Dice Computer");
		}
		
		public override void Throwing () {
			Debug.Log ("Throwing Computer");
		}
		
		public override void SelectCharacter () {
			UIHandler.Instance.DicesController.Disable ();
			Debug.Log ("SelectCharacter Computer");
		}

		public override void HandleAcceptButton () {
			UIHandler.Instance.AcceptController.View.Hide ();
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
	}
}

