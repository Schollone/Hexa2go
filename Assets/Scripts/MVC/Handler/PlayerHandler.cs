using UnityEngine;
using System.Collections;

namespace Hexa2Go {
	public class PlayerHandler {

		private readonly IPlayerController _playerController_One;
		private readonly IPlayerController _playerController_Two;

		public PlayerHandler () {
			_playerController_One = new PlayerController (TeamColor.RED);
			_playerController_Two = new PlayerController (TeamColor.BLUE);

			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			switch (nextMatchState) {
				case MatchState.ThrowDice:
					{
						Debug.LogWarning (GameManager.Instance.PlayerState);
						if (GameManager.Instance.PlayerState == PlayerState.Player) {
							Color color = HexagonColors.GetColor (_playerController_One.Model.TeamColor, Color.white);
							_playerController_One.View.UpdatePlayer (color, _playerController_One.Model.Name);
						} else if (GameManager.Instance.PlayerState == PlayerState.Enemy) {
							Color color = HexagonColors.GetColor (_playerController_Two.Model.TeamColor, Color.white);
							_playerController_Two.View.UpdatePlayer (color, _playerController_Two.Model.Name);
						}
						break;
					}
			}
		}

		public IPlayerController PlayerController_One {
			get {
				return _playerController_One;
			}
		}

		public IPlayerController PlayerController_Two {
			get {
				return _playerController_Two;
			}
		}

		public void Unregister () {
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
		}
	}

}