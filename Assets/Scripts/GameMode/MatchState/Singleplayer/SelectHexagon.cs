using System;
using UnityEngine;

namespace Hexa2Go {

	public class SelectHexagon : AbstractMatchState {
		private IPlayer _player;

		public override void Operate (IPlayer player) {
			UIHandler.Instance.DicesController.Show ();

			GameManager.Instance.GridFacade.HexagonFacade.InitSelectableHexagons ();

			_player = player;
			player.SelectHexagon ();
		}

		public override void HandleClick (IHexagonController hexagon) {
			if (hexagon.Model.State is SelectableHexagon) {
				ClickHandler.Instance.OnClick (ClickTypes.SelectHexagon, hexagon.Model.GridPos);
			} else if (hexagon.Model.State is DeactivatedFocusableHexagon) {
				ContainerObject data = new ContainerObject (hexagon.Model.GridPos, _player);
				ClickHandler.Instance.OnClick (ClickTypes.FocusHexagon, data);
			}

		}

		public override MatchStates GetNextState () {
			return MatchStates.Moving;
		}

	}
}

