using System;
using UnityEngine;

namespace Hexa2Go {

	public class SelectHexagon : AbstractMatchState {
		private IPlayer _player;

		public SelectHexagon () {
		}

		public override void Operate (IPlayer player) {
			UIHandler.Instance.DicesController.Show ();

			GameManager.Instance.GridFacade.HexagonFacade.InitSelectableHexagons ();

			_player = player;
			player.SelectHexagon ();
		}

		public override void HandleClick (IHexagonController hexagon) {
			if (hexagon.Model.State is SelectableHexagon) {
				GameManager.Instance.GridFacade.HexagonFacade.SelectHexagon (hexagon);
			} else if (hexagon.Model.State is DeactivatedFocusableHexagon) {
				GameManager.Instance.GridFacade.HexagonFacade.FocusHexagon (hexagon, _player);
			}

		}

		public override MatchStates GetNextState () {
			return MatchStates.Moving;
		}

		/*
		public void OnClickAccept () {
			GameManager.Instance.SetCurrentMatchState (new FocusHexagonTarget ());
		}

		public void OnClickNextHexagonTarget () {
			GameManager.Instance.GridHandler.SelectNextHexagon ();
		}

		public void OnClickPrevHexagonTarget () {
			GameManager.Instance.GridHandler.SelectPrevHexagon ();
		}*/

	}
}

