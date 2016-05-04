using UnityEngine;
using System;

namespace Hexa2Go {
	
	public class Opponent : AbstractPlayer {
		public Opponent () {
			_model = new PlayerModel (TeamColor.BLUE, (LocalizationManager.GetText (TextIdentifier.OPPONENT.ToString ())));

			_model.OnMatchFinished += HandleOnMatchFinished;
		}

		public override void ThrowDice () {
			Color color = HexagonColors.GetColor (GameManager.Instance.GetGameMode ().GetPlayers () [1].Model.TeamColor);
			String name = GameManager.Instance.GetGameMode ().GetPlayers () [1].Model.Name;
			GameManager.Instance.GetGameMode ().GetPlayers () [1].View.UpdatePlayer (color, name);
		}
		
		public override void Throwing () {
			
		}
		
		public override void SelectCharacter () {
			UIHandler.Instance.DicesController.Disable ();
			UIHandler.Instance.AcceptController.View.Hide ();
		}

		public override void HandleAcceptButton () {
			UIHandler.Instance.AcceptController.View.Hide ();
		}
		
		public override void SelectHexagon () {
			
		}
		
		public override void FocusHexagonTarget () {
			
		}
		
		public override void Moving () {
			
		}
	}
}

