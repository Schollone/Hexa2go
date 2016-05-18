using System;
using UnityEngine;

namespace Hexa2Go {

	public class SelectCharacter : AbstractMatchState {
		private IPlayer _player;

		public override void Operate (IPlayer player) {
			UIHandler.Instance.DicesController.Show ();

			bool hasFoundACharacter = GameManager.Instance.GridFacade.InitCharacterSelection ();

			if (hasFoundACharacter) {
				GridPos gridPos = GameManager.Instance.GridFacade.CharacterFacade.SelectedCharacter.GridPos;
				GameManager.Instance.GridFacade.HexagonFacade.SelectCharacter (gridPos);
			}

			_player = player;
			player.SelectCharacter (hasFoundACharacter);

			if (!hasFoundACharacter) {
				GameManager.Instance.GetGameMode().SwitchPlayer();
				GameManager.Instance.GetGameMode().SetMatchState(MatchStates.ThrowDice);
			}
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

