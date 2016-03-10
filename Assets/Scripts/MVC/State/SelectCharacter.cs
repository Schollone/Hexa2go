using System;
using UnityEngine;

namespace Hexa2Go {

	public class SelectCharacter : AbstractMatchState {
		public SelectCharacter () {
		}

		public override void Operate (IPlayer player) {
			GameManager.Instance.UIHandler.DicesController.Show ();
			GameManager.Instance.UIHandler.PrevHexagonController.View.Hide ();
			GameManager.Instance.UIHandler.NextHexagonController.View.Hide ();

			player.SelectCharacter ();
		}

		public override IMatchState GetNextState () {
			IMatchState state = null;
			GameManager.Instance.GameModeHandler.GetGameMode ().GetStateMap ().TryGetValue (MatchStates.FocusCharacterTargetSingleplayer, out state);
			return state;
		}

		public override void HandleClick(IHexagonModel hexagon) {
			HexagonFacade.SelectHexagon(hexagon);
		}
		
		/*public void OnClick () { // what Click?
			GameManager.Instance.MatchState = MatchState.Throwing;
		}*/
		/*
		private void UpdateGUI () {
			GameManager.Instance.UIHandler.DicesController.Show ();
			//_dicesController.Disable ();
			GameManager.Instance.UIHandler.PrevHexagonController.View.Hide ();
			GameManager.Instance.UIHandler.NextHexagonController.View.Hide ();

			GameManager.Instance.GameModeHandler.GetGameMode ().UpdateSelectCharacterGUI ();
		}

		public void OnClickAccept () {
			GameManager.Instance.SetCurrentMatchState (new FocusCharacterTarget ());
		}

		public void OnClickDice (IDiceController diceController) {
			GameManager.Instance.GridHandler.SelectNextCharacter ();
			//Debug.Log (sender.ToString ());
			ICharacterController controller = GameManager.Instance.GridHandler.CharacterHandler_P1.GetCharacter (diceController.Model.CharacterType);
			controller.Model.Deselect ();
			GameManager.Instance.GridHandler.HexagonHandler.Deselect (controller.Model.GridPos);
		}

		public void OnClickNextCharacter () {
			GameManager.Instance.GridHandler.SelectNextCharacter ();
		}*/

	}
}

