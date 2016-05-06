using UnityEngine;
using System;

namespace Hexa2Go {

	public class Player : AbstractPlayer {
		public Player (TeamColor teamColor, int playernumber = -1) {
			String playername = (LocalizationManager.GetText (TextIdentifier.PLAYER.ToString ()));
			if (playernumber != -1) {
				playername += " " + playernumber.ToString();
			}
			_model = new PlayerModel (teamColor, playername);

			_model.OnMatchFinished += HandleOnMatchFinished;
		}

		public override void ThrowDice () {
			Color color = HexagonColors.GetColor (GameManager.Instance.GetGameMode ().CurrentPlayer.Model.TeamColor);
			String name = GameManager.Instance.GetGameMode ().CurrentPlayer.Model.Name;
			GameManager.Instance.GetGameMode ().CurrentPlayer.View.UpdatePlayer (color, name);
			UIHandler.Instance.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_THROW_DICE.ToString ()));
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

