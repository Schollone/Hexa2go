using UnityEngine;
using System;

namespace Hexa2Go {

	public class Player : AbstractPlayer {
		public Player (TeamColor teamColor) {
			_model = new PlayerModel (teamColor, (LocalizationManager.GetText (TextIdentifier.PLAYER.ToString ())));

			_model.OnMatchFinished += HandleOnMatchFinished;
		}

		public override void ThrowDice () {
			Color color = HexagonColors.GetColor (GameManager.Instance.GetGameMode ().CurrentPlayer.Model.TeamColor);
			String name = GameManager.Instance.GetGameMode ().CurrentPlayer.Model.Name;
			GameManager.Instance.GetGameMode ().CurrentPlayer.View.UpdatePlayer (color, name);
		}

		public override void Throwing () {

		}

		public override void SelectCharacter () {
			UIHandler.Instance.DicesController.Enable ();
			UIHandler.Instance.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_SELECT_CHARACTER.ToString ()));
		}

		public override void HandleAcceptButton () {
			UIHandler.Instance.AcceptController.View.Show ();
		}

		public override void SelectHexagon () {
			UIHandler.Instance.DicesController.Disable ();
			UIHandler.Instance.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_SELECT_HEXAGON.ToString ()));

			GameManager.Instance.GridFacade.HexagonFacade.InitSelectableHexagons ();
		}

		public override void FocusHexagonTarget () {

		}

		public override void Moving () {

		}
	}
}

