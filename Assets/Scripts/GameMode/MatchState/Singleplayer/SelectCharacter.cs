using System;
using UnityEngine;

namespace Hexa2Go {

	public class SelectCharacter : AbstractMatchState {
		private IPlayer _player;

		public override void Operate (IPlayer player) {
			UIHandler.Instance.DicesController.Show ();

			GameManager.Instance.GridFacade.InitCharacterSelection ();

			_player = player;
			player.SelectCharacter ();
		}

		public override void HandleClick (IHexagonController hexagon) {
			if (hexagon.Model.State is FocusableHexagon) {
				ContainerObject data = new ContainerObject (hexagon.Model.GridPos, _player);
				ClickHandler.Instance.OnClick (ClickTypes.FocusHexagon, data);
			}
		}

		public override MatchStates GetNextState () {
			return MatchStates.Moving;
		}
	}
}

