using UnityEngine;
using System;

namespace Hexa2Go {
	
	public class Opponent : AbstractPlayer {

		public Opponent () {
			_model = new PlayerModel (TeamColor.BLUE, (LocalizationManager.GetText (TextIdentifier.OPPONENT.ToString ())));

			_model.OnMatchFinished += HandleOnMatchFinished;
			_model.OnCharacterRemoved += HandleOnCharacterRemoved;

			GameObject playerStats = GameObject.Find ("PlayerStats_Player2");
			_statsView = playerStats.GetComponent<StatsView> ();
			_statsView.UpdateStats (_model.Name, _model.SavedCharacters);
		}

		public override void ThrowDice () {
			Color color = HexagonColors.GetColor (GameManager.Instance.GetGameMode ().GetPlayers () [1].Model.TeamColor);
			String name = GameManager.Instance.GetGameMode ().GetPlayers () [1].Model.Name;
			GameManager.Instance.GetGameMode ().GetPlayers () [1].View.UpdatePlayer (color, name);
		}
		
		public override void Throwing () {
			
		}

		public override void SelectCharacter (bool hasFoundACharacter) {
			UIHandler.Instance.DicesController.Disable ();
			UIHandler.Instance.AcceptController.View.Hide ();
		}

		public override void SelectHexagon () {
			
		}

		public override void HandleAcceptButton () {
			UIHandler.Instance.AcceptController.View.Hide ();
		}
		
		public override void Moving () {
			
		}
	}
}

