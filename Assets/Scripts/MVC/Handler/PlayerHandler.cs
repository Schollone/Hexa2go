using UnityEngine;
using System.Collections;

namespace Hexa2Go {
	public class PlayerHandler {

		private readonly IPlayerController _playerController_One;
		private readonly IPlayerController _playerController_Two;

		public PlayerHandler () {
			string namePlayerOne = "";
			string namePlayerTwo = "";

			GameMode gameMode = GameManager.Instance.GameModeHandler.GameMode;

			switch (gameMode) {
				case GameMode.Singleplayer:
					{
						namePlayerOne = "Spieler";
						namePlayerTwo = "Computer";
						break;
					}
				case GameMode.Multiplayer:
					{
						namePlayerOne = "Spieler 1";
						namePlayerTwo = "Spieler 2";
						break;
					}
				case GameMode.OnlineMultiplayer:
					{
						namePlayerOne = "Spieler";
						namePlayerTwo = "Gegner";
						break;
					}
			}

			_playerController_One = new PlayerController (TeamColor.RED, namePlayerOne);
			_playerController_Two = new PlayerController (TeamColor.BLUE, namePlayerTwo);

			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			PlayerState playerState = GameManager.Instance.PlayerState;

			//Debug.LogWarning(playerState + "!!! On Match State Change " + nextMatchState + " --- PlayerHandler");

			switch (nextMatchState) {
				case MatchState.ThrowDice:
					{
						if (playerState == PlayerState.Player) {
							Color color = HexagonColors.GetColor (_playerController_One.Model.TeamColor);
							_playerController_One.View.UpdatePlayer (color, _playerController_One.Model.Name);
						} else if (playerState == PlayerState.Enemy) {
							Color color = HexagonColors.GetColor (_playerController_Two.Model.TeamColor);
							_playerController_Two.View.UpdatePlayer (color, _playerController_Two.Model.Name);
						}
						break;
					}
				case MatchState.Win:
					{
						switch (GameManager.Instance.GameModeHandler.GameMode) {
							case GameMode.Singleplayer:
							case GameMode.OnlineMultiplayer:
								{
									if (playerState == PlayerState.Player) {
										Color color = HexagonColors.GetColor (_playerController_One.Model.TeamColor);
										_playerController_One.View.UpdatePlayer (color, "Gewonnen");
									} else if (playerState == PlayerState.Enemy) {
										Color color = HexagonColors.GetColor (_playerController_One.Model.TeamColor);
										_playerController_One.View.UpdatePlayer (color, "Verloren");
									}
									break;
								}
							case GameMode.Multiplayer:
								{
									if (playerState == PlayerState.Player) {
										Color color = HexagonColors.GetColor (_playerController_One.Model.TeamColor);
										_playerController_One.View.UpdatePlayer (color, "Gewonnen");
									} else if (playerState == PlayerState.Enemy) {
										Color color = HexagonColors.GetColor (_playerController_Two.Model.TeamColor);
										_playerController_Two.View.UpdatePlayer (color, "Gewonnen");
									}
									break;
								}
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