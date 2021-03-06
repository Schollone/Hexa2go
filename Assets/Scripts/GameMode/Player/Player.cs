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
			_model.OnCharacterRemoved += HandleOnCharacterRemoved;

			String playerStatsGameObject = "PlayerStats_Player" + Math.Abs(playernumber).ToString();
			GameObject playerStats = GameObject.Find (playerStatsGameObject);
			_statsView = playerStats.GetComponent<StatsView> ();
			_statsView.UpdateStats (_model.Name, _model.SavedCharacters);
		}

		public override void ThrowDice () {
			Color color = HexagonColors.GetColor (GameManager.Instance.GetGameMode ().CurrentPlayer.Model.TeamColor);
			String name = GameManager.Instance.GetGameMode ().CurrentPlayer.Model.Name;
			GameManager.Instance.GetGameMode ().CurrentPlayer.View.UpdatePlayer (color, name);
			UIHandler.Instance.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_THROW_DICE.ToString ()));
		}

		public override void Throwing () {
		}

		public override void SelectCharacter (bool hasFoundACharacter) {
			UIHandler.Instance.DicesController.Enable ();
			UIHandler.Instance.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_SELECT_CHARACTER.ToString ()));
		}

		public override void SelectHexagon () {
			UIHandler.Instance.DicesController.Disable ();
			UIHandler.Instance.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_SELECT_HEXAGON.ToString ()));

			GameManager.Instance.GridFacade.HexagonFacade.InitSelectableHexagons ();
		}
		
		public override void HandleAcceptButton () {
			UIHandler.Instance.AcceptController.View.Show ();
		}

		public override void Moving () {
		}

		public override void GameOver () {
			base.GameOver ();
			UIHandler.Instance.HintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_GAME_OVER.ToString ()));
		}
	}
}

