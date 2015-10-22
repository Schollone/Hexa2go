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
						namePlayerOne = LocalizationManager.GetText (TextIdentifier.PLAYER.ToString ());
						namePlayerTwo = LocalizationManager.GetText (TextIdentifier.COMPUTER.ToString ());
						break;
					}
				case GameMode.Multiplayer:
					{
						namePlayerOne = LocalizationManager.GetText (TextIdentifier.PLAYER_1.ToString ());
						namePlayerTwo = LocalizationManager.GetText (TextIdentifier.PLAYER_2.ToString ());
						break;
					}
				case GameMode.OnlineMultiplayer:
					{
						namePlayerOne = LocalizationManager.GetText (TextIdentifier.PLAYER.ToString ());
						namePlayerTwo = LocalizationManager.GetText (TextIdentifier.OPPONENT.ToString ());
						break;
					}
			}

			_playerController_One = new PlayerController (TeamColor.RED, namePlayerOne);
			_playerController_Two = new PlayerController (TeamColor.BLUE, namePlayerTwo);

			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			PlayerState playerState = GameManager.Instance.PlayerState;

			switch (nextMatchState) {
				case MatchState.ThrowDice:
					{
						if (playerState == PlayerState.Player) {
							Color color = HexagonColors.GetColor (_playerController_One.Model.TeamColor);
							_playerController_One.View.UpdatePlayer (color, _playerController_One.Model.Name);
						} else if (playerState == PlayerState.Opponent) {
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
										_playerController_One.View.UpdatePlayer (color, LocalizationManager.GetText (TextIdentifier.WON.ToString ()));
									} else if (playerState == PlayerState.Opponent) {
										Color color = HexagonColors.GetColor (_playerController_One.Model.TeamColor);
										_playerController_One.View.UpdatePlayer (color, LocalizationManager.GetText (TextIdentifier.LOSE.ToString ()));
									}
									break;
								}
							case GameMode.Multiplayer:
								{
									if (playerState == PlayerState.Player) {
										Color color = HexagonColors.GetColor (_playerController_One.Model.TeamColor);
										_playerController_One.View.UpdatePlayer (color, LocalizationManager.GetText (TextIdentifier.WON.ToString ()));
									} else if (playerState == PlayerState.Opponent) {
										Color color = HexagonColors.GetColor (_playerController_Two.Model.TeamColor);
										_playerController_Two.View.UpdatePlayer (color, LocalizationManager.GetText (TextIdentifier.WON.ToString ()));
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