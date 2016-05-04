using System;
using UnityEngine;

namespace Hexa2Go {

	public class SelectCharacter : AbstractMatchState {
		private IPlayer _player;

		public SelectCharacter () {
		}

		public override void Operate (IPlayer player) {
			UIHandler.Instance.DicesController.Show ();

			GameManager.Instance.GridFacade.InitCharacterSelection ();
			//GameManager.Instance.GridFacade.InitNeighbors();

			_player = player;
			player.SelectCharacter ();
		}

		public override void HandleClick (IHexagonModel hexagon) {
			if (hexagon.State is FocusableHexagon) {
				GameManager.Instance.GridFacade.HexagonFacade.FocusHexagon (hexagon, _player);
			}
		}

		public override MatchStates GetNextState () {
			return MatchStates.Moving;
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

