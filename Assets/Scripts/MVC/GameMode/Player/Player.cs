using UnityEngine;
using System;

namespace Hexa2Go {

	public class Player : AbstractPlayer {
		public Player () {
			_model = new PlayerModel (TeamColor.RED, (LocalizationManager.GetText (TextIdentifier.PLAYER.ToString ())));
		}

		public override void ThrowDice () {
			Color color = HexagonColors.GetColor (GameManager.Instance.GameModeHandler.GetGameMode ().GetPlayers () [0].Model.TeamColor);
			String name = GameManager.Instance.GameModeHandler.GetGameMode ().GetPlayers () [0].Model.Name;
			GameManager.Instance.GameModeHandler.GetGameMode ().GetPlayers () [0].View.UpdatePlayer (color, name);
		}

		public override void Throwing () {

		}

		public override void SelectCharacter () {
			GameManager.Instance.UIHandler.DicesController.Enable ();	
			GameManager.Instance.UIHandler.NextCharacterController.View.Show ();
			GameManager.Instance.UIHandler.AcceptController.View.Show ();
			GameManager.Instance.UIHandler.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_SELECT_CHARACTER.ToString ()));
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

