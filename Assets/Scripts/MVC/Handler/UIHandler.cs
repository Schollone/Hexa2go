using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class UIHandler {

		private DicesController _dicesController;
		private PrevHexagonController _prevHexagonController;
		private NextHexagonController _nextHexagonController;
		private NextCharacterController _nextCharcarterController;
		private AcceptController _acceptController;
		private HintController _hintController;

		private IPlayerController _playerController_One;
		private IPlayerController _playerController_Two;

		public UIHandler () {
			Debug.Log ("ButtonHandler");

			InitAcceptController ();

			InitNextCharacterController ();

			InitHexagonController ();

			InitDicesController ();

			InitHintController ();

			InitPlayerStatus ();

			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
		}

		private void InitAcceptController () {
			GameObject accept = GameObject.Find ("Btn_Accept");
			AcceptView acceptView = accept.GetComponent<AcceptView> ();
			_acceptController = new AcceptController (acceptView);
		}

		private void InitNextCharacterController () {
			GameObject nextCharacter = GameObject.Find ("Btn_NextCharacter");
			NextCharacterView nextCharacterView = nextCharacter.GetComponent<NextCharacterView> ();
			_nextCharcarterController = new NextCharacterController (nextCharacterView);
		}

		private void InitHexagonController () {
			GameObject prevHexagon = GameObject.Find ("Btn_PrevHexagon");
			PrevHexagonView prevHexagonView = prevHexagon.GetComponent<PrevHexagonView> ();
			_prevHexagonController = new PrevHexagonController (prevHexagonView);

			GameObject nextHexagon = GameObject.Find ("Btn_NextHexagon");
			NextHexagonView nextHexagonView = nextHexagon.GetComponent<NextHexagonView> ();
			_nextHexagonController = new NextHexagonController (nextHexagonView);
		}

		private void InitDicesController () {
			GameObject dice_left = GameObject.Find ("Btn_Dice_Left");
			IDiceView diceView_left = dice_left.GetComponent<IDiceView> ();
			GameObject dice_right = GameObject.Find ("Btn_Dice_Right");
			IDiceView diceView_right = dice_right.GetComponent<IDiceView> ();
			
			IDiceController diceController_left = new DiceController (diceView_left);
			IDiceController diceController_right = new DiceController (diceView_right);
			_dicesController = new DicesController (diceController_left, diceController_right);
		}

		private void InitHintController () {
			GameObject hint = GameObject.Find ("Hint");
			HintView hintView = hint.GetComponent<HintView> ();
			_hintController = new HintController (hintView);
		}

		private void InitPlayerStatus () {
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
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			PlayerState playerState = GameManager.Instance.PlayerState;
			GameMode gameMode = GameManager.Instance.GameModeHandler.GameMode;
			_hintController.View.UpdateHint ("");

			switch (nextMatchState) {
				case MatchState.ThrowDice:
					{
						Debug.Log ("ThrowDice ButtonHandler");						
						_dicesController.Show ();
						_prevHexagonController.View.Hide ();
						_nextHexagonController.View.Hide ();
						_nextCharcarterController.View.Hide ();
						_acceptController.View.Hide ();

						switch (gameMode) {
							case GameMode.Singleplayer:
							case GameMode.OnlineMultiplayer:
								{
									if (playerState == PlayerState.Player) {
										_dicesController.Enable ();
										_hintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_THROW_DICE.ToString ()));
									} else if (playerState == PlayerState.Opponent) {
										_dicesController.Disable ();
									}
							
									break;
								}
							case GameMode.Multiplayer:
								{
									_hintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_THROW_DICE.ToString ()));
									_dicesController.Enable ();
									break;
								}
						}

						// Update display of player status
						if (playerState == PlayerState.Player) {
							Color color = HexagonColors.GetColor (_playerController_One.Model.TeamColor);
							_playerController_One.View.UpdatePlayer (color, _playerController_One.Model.Name);
						} else if (playerState == PlayerState.Opponent) {
							Color color = HexagonColors.GetColor (_playerController_Two.Model.TeamColor);
							_playerController_Two.View.UpdatePlayer (color, _playerController_Two.Model.Name);
						}

						Debug.Log ("ThrowDice ButtonHandler ENDE");	
						break;
					}
				case MatchState.Throwing:
					{
						Debug.Log ("Throwing ButtonHandler");
						_dicesController.Show ();
						_dicesController.Disable ();
						_dicesController.StartThrow ();
						_prevHexagonController.View.Hide ();
						_nextHexagonController.View.Hide ();
						_nextCharcarterController.View.Hide ();
						_acceptController.View.Hide ();

						Debug.Log ("Throwing ButtonHandler ENDE");
						break;
					}
				case MatchState.SelectCharacter:
					{
						Debug.Log ("SelectCharacter ButtonHandler");
						_dicesController.Show ();
						_dicesController.Disable ();
						_prevHexagonController.View.Hide ();
						_nextHexagonController.View.Hide ();

						switch (gameMode) {
							case GameMode.Singleplayer:
							case GameMode.OnlineMultiplayer:
								{
									if (playerState == PlayerState.Player) {
										_nextCharcarterController.View.Show ();
										_acceptController.View.Show ();
										_hintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_SELECT_CHARACTER.ToString ()));
									} else if (playerState == PlayerState.Opponent) {
										_nextCharcarterController.View.Hide ();
										_acceptController.View.Hide ();
									}
							
									break;
								}
							case GameMode.Multiplayer:
								{
									_hintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_SELECT_CHARACTER.ToString ()));
									_nextCharcarterController.View.Show ();
									_acceptController.View.Show ();
									break;
								}
						}
				
						Debug.Log ("SelectCharacter ButtonHandler ENDE");
						break;
					}
				case MatchState.FocusCharacterTarget:
					{
						Debug.Log ("FocusCharacterTarget ButtonHandler");
						_dicesController.Show ();
						_dicesController.Disable ();
						_nextCharcarterController.View.Hide ();

						if (gameMode != GameMode.Multiplayer && playerState == PlayerState.Opponent) {
							_prevHexagonController.View.Hide ();
							_nextHexagonController.View.Hide ();
							_acceptController.View.Hide ();
						} else {
							_hintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_FOCUS_CHARACTER_TARGET.ToString ()));
							_prevHexagonController.View.Show ();
							_nextHexagonController.View.Show ();
							_acceptController.View.Show ();
						}
				
						Debug.Log ("FocusCharacterTarget ButtonHandler ENDE");
						break;
					}
				case MatchState.SelectHexagon:
					{
						Debug.Log ("SelectHexagon ButtonHandler");
						_dicesController.Show ();
						_dicesController.Disable ();
						_nextCharcarterController.View.Hide ();

						if (gameMode != GameMode.Multiplayer && playerState == PlayerState.Opponent) {
							_prevHexagonController.View.Hide ();
							_nextHexagonController.View.Hide ();
							_acceptController.View.Hide ();
						} else {
							_hintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_SELECT_HEXAGON.ToString ()));
							_prevHexagonController.View.Show ();
							_nextHexagonController.View.Show ();
							_acceptController.View.Show ();
						}
					
						Debug.Log ("SelectHexagon ButtonHandler ENDE");
						break;
					}
				case MatchState.FocusHexagonTarget:
					{
						Debug.Log ("FocusHexagonTarget ButtonHandler");
						_dicesController.Show ();
						_dicesController.Disable ();
						_nextCharcarterController.View.Hide ();

						if (gameMode != GameMode.Multiplayer && playerState == PlayerState.Opponent) {
							_prevHexagonController.View.Hide ();
							_nextHexagonController.View.Hide ();
							_acceptController.View.Hide ();
						} else {
							_hintController.View.UpdateHint (LocalizationManager.GetText (TextIdentifier.HINT_FOCUS_HEXAGON_TARGET.ToString ()));
							_prevHexagonController.View.Show ();
							_nextHexagonController.View.Show ();
							_acceptController.View.Show ();
						}
					
						Debug.Log ("FocusHexagonTarget ButtonHandler ENDE");
						break;
					}
				case MatchState.Win:
					{
						Debug.Log ("FocusHexagonTarget ButtonHandler");
						_dicesController.Hide ();
						_prevHexagonController.View.Hide ();
						_nextHexagonController.View.Hide ();
						_nextCharcarterController.View.Hide ();
						_acceptController.View.Show ();

						switch (gameMode) {
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

						Debug.Log ("FocusHexagonTarget ButtonHandler ENDE");
						break;
					}
				
			}
		}

		public void Unregister () {
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
		}

		public DicesController DicesController {
			get {
				return _dicesController;
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
	}

}