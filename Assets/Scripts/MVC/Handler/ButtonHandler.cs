using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class ButtonHandler {

		private DicesController _dicesController;
		private PrevHexagonController _prevHexagonController;
		private NextHexagonController _nextHexagonController;
		private NextCharacterController _nextCharcarterController;
		private AcceptController _acceptController;

		public ButtonHandler() {
			Debug.Log("ButtonHandler");
			//GameObject prefab = Resources.Load<GameObject>("Btn_Accept");
			//GameObject instance = UnityEngine.Object.Instantiate(prefab);
			//IButtonView acceptView = instance.GetComponent<IButtonView>();
			initAcceptController();

			initNextCharacterController();

			initHexagonController();

			initDicesController();

			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
		}

		private void initAcceptController() {
			GameObject accept = GameObject.Find ("Btn_Accept");
			AcceptView acceptView = accept.GetComponent<AcceptView>();
			_acceptController = new AcceptController(acceptView);
		}

		private void initNextCharacterController() {
			GameObject nextCharacter = GameObject.Find ("Btn_NextCharacter");
			NextCharacterView nextCharacterView = nextCharacter.GetComponent<NextCharacterView>();
			_nextCharcarterController = new NextCharacterController(nextCharacterView);
		}

		private void initHexagonController() {
			GameObject prevHexagon = GameObject.Find ("Btn_PrevHexagon");
			PrevHexagonView prevHexagonView = prevHexagon.GetComponent<PrevHexagonView>();
			_prevHexagonController = new PrevHexagonController(prevHexagonView);

			GameObject nextHexagon = GameObject.Find ("Btn_NextHexagon");
			NextHexagonView nextHexagonView = nextHexagon.GetComponent<NextHexagonView>();
			_nextHexagonController = new NextHexagonController(nextHexagonView);
		}

		private void initDicesController() {
			GameObject dice_left = GameObject.Find("Btn_Dice_Left");
			IDiceView diceView_left = dice_left.GetComponent<IDiceView>();
			GameObject dice_right = GameObject.Find("Btn_Dice_Right");
			IDiceView diceView_right = dice_right.GetComponent<IDiceView>();

			//IDiceView diceView_left = dice_left.AddComponent<DiceView>() as DiceView;
			//IDiceView diceView_right = dice_right.AddComponent<DiceView>() as DiceView;
			
			IDiceController diceController_left = new DiceController(diceView_left);
			IDiceController diceController_right = new DiceController(diceView_right);
			_dicesController = new DicesController(diceController_left, diceController_right);
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			PlayerState playerState = GameManager.Instance.PlayerState;

			switch(nextMatchState) {
				case MatchState.NullState: {

					break;
				}
				case MatchState.ThrowDice: {

					_dicesController.Show();
					_prevHexagonController.View.Hide();
					_nextHexagonController.View.Hide();
					_nextCharcarterController.View.Hide();
					_acceptController.View.Hide();

					switch(GameManager.Instance.GameMode) {
						case GameMode.Singleplayer:
						case GameMode.OnlineMultiplayer: {
							if (playerState == PlayerState.Player) {
								_dicesController.Enable();
							} else if (playerState == PlayerState.Enemy) {
								_dicesController.Disable();
							}
							
							break;
						}
						case GameMode.Multiplayer: {
							_dicesController.Enable();
							break;
						}
					}

					break;
				}
				case MatchState.Throwing: {

					_dicesController.Show();
					_dicesController.Disable();
					_dicesController.StartThrow();
					_prevHexagonController.View.Hide();
					_nextHexagonController.View.Hide();
					_nextCharcarterController.View.Hide();
					_acceptController.View.Hide();

					break;
				}
				case MatchState.SelectCharacter: {

					_dicesController.Show();
					_dicesController.Disable();
					_prevHexagonController.View.Hide();
					_nextHexagonController.View.Hide();

					switch(GameManager.Instance.GameMode) {
						case GameMode.Singleplayer:
						case GameMode.OnlineMultiplayer: {
							if (playerState == PlayerState.Player) {
								_nextCharcarterController.View.Show();
								_acceptController.View.Show();
							} else if (playerState == PlayerState.Enemy) {
								_nextCharcarterController.View.Hide();
								_acceptController.View.Hide();
							}
							
							break;
						}
						case GameMode.Multiplayer: {
							_nextCharcarterController.View.Show();
							_acceptController.View.Show();
							break;
						}
					}
				
				break;
				}
				case MatchState.FocusCharacterTarget: {

					_dicesController.Show();
					_dicesController.Disable();
					_prevHexagonController.View.Show();
					_nextHexagonController.View.Show();
					_nextCharcarterController.View.Hide();
					_acceptController.View.Show();

					switch(GameManager.Instance.GameMode) {
						case GameMode.Singleplayer:
						case GameMode.OnlineMultiplayer: {
							if (playerState == PlayerState.Player) {
								_prevHexagonController.View.Enable();
								_nextHexagonController.View.Enable();
								_acceptController.View.Enable();
							} else if (playerState == PlayerState.Enemy) {
								_prevHexagonController.View.Disable();
								_nextHexagonController.View.Disable();
								_acceptController.View.Disable();
							}
					
							break;
						}
						case GameMode.Multiplayer: {
							_prevHexagonController.View.Enable();
							_nextHexagonController.View.Enable();
							_acceptController.View.Enable();
							break;
						}
					}
				
					break;
				}
				case MatchState.SelectHexagon: {

					_dicesController.Show();
					_dicesController.Disable();
					_prevHexagonController.View.Show();
					_nextHexagonController.View.Show();
					_nextCharcarterController.View.Hide();
					_acceptController.View.Show();

					switch(GameManager.Instance.GameMode) {
						case GameMode.Singleplayer:
						case GameMode.OnlineMultiplayer: {
							if (playerState == PlayerState.Player) {
								_prevHexagonController.View.Enable();
								_nextHexagonController.View.Enable();
								_acceptController.View.Enable();
							} else if (playerState == PlayerState.Enemy) {
								_prevHexagonController.View.Disable();
								_nextHexagonController.View.Disable();
								_acceptController.View.Disable();
							}
							
							break;
						}
						case GameMode.Multiplayer: {
							_prevHexagonController.View.Enable();
							_nextHexagonController.View.Enable();
							_acceptController.View.Enable();
							break;
						}
					}

				break;
				}
				case MatchState.FocusHexagonTarget: {

					_dicesController.Show();
					_dicesController.Disable();
					_prevHexagonController.View.Show();
					_nextHexagonController.View.Show();
					_nextCharcarterController.View.Hide();
					_acceptController.View.Show();

					switch(GameManager.Instance.GameMode) {
						case GameMode.Singleplayer:
						case GameMode.OnlineMultiplayer: {
							if (playerState == PlayerState.Player) {
								_prevHexagonController.View.Enable();
								_nextHexagonController.View.Enable();
								_acceptController.View.Enable();
							} else if (playerState == PlayerState.Enemy) {
								_prevHexagonController.View.Disable();
								_nextHexagonController.View.Disable();
								_acceptController.View.Disable();
							}
					
							break;
						}
						case GameMode.Multiplayer: {
							_prevHexagonController.View.Enable();
							_nextHexagonController.View.Enable();
							_acceptController.View.Enable();
							break;
						}
					}

					break;
				}
				
			}
		}

		public void Unregister() {
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
		}

		public DicesController DicesController {
			get {
				return _dicesController;
			}
		}
	}

}