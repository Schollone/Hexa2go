using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class ButtonHandler {

		private DicesController _dicesController;
		private PrevHexagonController _prevHexagonController;
		private NextHexagonController _nextHexagonController;
		private NextCharacterController _nextCharcarterController;
		private AcceptController _acceptController;

		public ButtonHandler () {
			Debug.Log ("ButtonHandler");

			initAcceptController ();

			initNextCharacterController ();

			initHexagonController ();

			initDicesController ();

			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
		}

		private void initAcceptController () {
			GameObject accept = GameObject.Find ("Btn_Accept");
			AcceptView acceptView = accept.GetComponent<AcceptView> ();
			_acceptController = new AcceptController (acceptView);
		}

		private void initNextCharacterController () {
			GameObject nextCharacter = GameObject.Find ("Btn_NextCharacter");
			NextCharacterView nextCharacterView = nextCharacter.GetComponent<NextCharacterView> ();
			_nextCharcarterController = new NextCharacterController (nextCharacterView);
		}

		private void initHexagonController () {
			GameObject prevHexagon = GameObject.Find ("Btn_PrevHexagon");
			PrevHexagonView prevHexagonView = prevHexagon.GetComponent<PrevHexagonView> ();
			_prevHexagonController = new PrevHexagonController (prevHexagonView);

			GameObject nextHexagon = GameObject.Find ("Btn_NextHexagon");
			NextHexagonView nextHexagonView = nextHexagon.GetComponent<NextHexagonView> ();
			_nextHexagonController = new NextHexagonController (nextHexagonView);
		}

		private void initDicesController () {
			GameObject dice_left = GameObject.Find ("Btn_Dice_Left");
			IDiceView diceView_left = dice_left.GetComponent<IDiceView> ();
			GameObject dice_right = GameObject.Find ("Btn_Dice_Right");
			IDiceView diceView_right = dice_right.GetComponent<IDiceView> ();
			
			IDiceController diceController_left = new DiceController (diceView_left);
			IDiceController diceController_right = new DiceController (diceView_right);
			_dicesController = new DicesController (diceController_left, diceController_right);
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			PlayerState playerState = GameManager.Instance.PlayerState;
			GameMode gameMode = GameManager.Instance.GameModeHandler.GameMode;

			switch (nextMatchState) {
				case MatchState.NullState:
					{
						Debug.Log ("NullState ButtonHandler");
						break;
					}
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
									} else if (playerState == PlayerState.Enemy) {
										_dicesController.Disable ();
									}
							
									break;
								}
							case GameMode.Multiplayer:
								{
									_dicesController.Enable ();
									break;
								}
						}

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
									} else if (playerState == PlayerState.Enemy) {
										_nextCharcarterController.View.Hide ();
										_acceptController.View.Hide ();
									}
							
									break;
								}
							case GameMode.Multiplayer:
								{
									_nextCharcarterController.View.Show ();
									_acceptController.View.Show ();
									break;
								}
						}
				
						break;
					}
				case MatchState.FocusCharacterTarget:
					{
						Debug.Log ("FocusCharacterTarget ButtonHandler");
						_dicesController.Show ();
						_dicesController.Disable ();
						_prevHexagonController.View.Show ();
						_nextHexagonController.View.Show ();
						_nextCharcarterController.View.Hide ();
						_acceptController.View.Show ();

						switch (gameMode) {
							case GameMode.Singleplayer:
							case GameMode.OnlineMultiplayer:
								{
									if (playerState == PlayerState.Player) {
										_prevHexagonController.View.Enable ();
										_nextHexagonController.View.Enable ();
										_acceptController.View.Enable ();
									} else if (playerState == PlayerState.Enemy) {
										_prevHexagonController.View.Disable ();
										_nextHexagonController.View.Disable ();
										_acceptController.View.Disable ();
									}
					
									break;
								}
							case GameMode.Multiplayer:
								{
									_prevHexagonController.View.Enable ();
									_nextHexagonController.View.Enable ();
									_acceptController.View.Enable ();
									break;
								}
						}
				
						break;
					}
				case MatchState.SelectHexagon:
					{
						Debug.Log ("SelectHexagon ButtonHandler");
						_dicesController.Show ();
						_dicesController.Disable ();
						_prevHexagonController.View.Show ();
						_nextHexagonController.View.Show ();
						_nextCharcarterController.View.Hide ();
						_acceptController.View.Show ();

						switch (gameMode) {
							case GameMode.Singleplayer:
							case GameMode.OnlineMultiplayer:
								{
									if (playerState == PlayerState.Player) {
										_prevHexagonController.View.Enable ();
										_nextHexagonController.View.Enable ();
										_acceptController.View.Enable ();
									} else if (playerState == PlayerState.Enemy) {
										_prevHexagonController.View.Disable ();
										_nextHexagonController.View.Disable ();
										_acceptController.View.Disable ();
									}
							
									break;
								}
							case GameMode.Multiplayer:
								{
									_prevHexagonController.View.Enable ();
									_nextHexagonController.View.Enable ();
									_acceptController.View.Enable ();
									break;
								}
						}

						break;
					}
				case MatchState.FocusHexagonTarget:
					{
						Debug.Log ("FocusHexagonTarget ButtonHandler");
						_dicesController.Show ();
						_dicesController.Disable ();
						_prevHexagonController.View.Show ();
						_nextHexagonController.View.Show ();
						_nextCharcarterController.View.Hide ();
						_acceptController.View.Show ();

						switch (gameMode) {
							case GameMode.Singleplayer:
							case GameMode.OnlineMultiplayer:
								{
									if (playerState == PlayerState.Player) {
										_prevHexagonController.View.Enable ();
										_nextHexagonController.View.Enable ();
										_acceptController.View.Enable ();
									} else if (playerState == PlayerState.Enemy) {
										_prevHexagonController.View.Disable ();
										_nextHexagonController.View.Disable ();
										_acceptController.View.Disable ();
									}
					
									break;
								}
							case GameMode.Multiplayer:
								{
									_prevHexagonController.View.Enable ();
									_nextHexagonController.View.Enable ();
									_acceptController.View.Enable ();
									break;
								}
						}

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
	}

}