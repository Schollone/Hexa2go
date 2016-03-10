using UnityEngine;
using System;

namespace Hexa2Go {
	
	public class Opponent : AbstractPlayer {
		public Opponent () {
			_model = new PlayerModel (TeamColor.BLUE, (LocalizationManager.GetText (TextIdentifier.OPPONENT.ToString ())));
		}

		public override void ThrowDice () {
			Color color = HexagonColors.GetColor (GameManager.Instance.GameModeHandler.GetGameMode ().GetPlayers () [1].Model.TeamColor);
			String name = GameManager.Instance.GameModeHandler.GetGameMode ().GetPlayers () [1].Model.Name;
			GameManager.Instance.GameModeHandler.GetGameMode ().GetPlayers () [1].View.UpdatePlayer (color, name);
		}
		
		public override void Throwing () {
			
		}
		
		public override void SelectCharacter () {
			GameManager.Instance.UIHandler.DicesController.Disable ();
			GameManager.Instance.UIHandler.NextCharacterController.View.Hide ();
			GameManager.Instance.UIHandler.AcceptController.View.Hide ();
		}
		
		public override void FocusCharacterTarget () {
			
		}
		
		public override void SelectHexagon () {
			
		}
		
		public override void FocusHexagonTarget () {
			
		}
		
		public override void Moving () {
			
		}
		
		public override void GameOver () {
			
		}
	}
}

