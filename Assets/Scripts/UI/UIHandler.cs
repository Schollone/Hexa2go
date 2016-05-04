using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class UIHandler {

		private DicesController _dicesController;
		private AcceptController _acceptController;
		private HintController _hintController;

		private static UIHandler _instance = null;

		private UIHandler () {
			Debug.Log ("UIHandler");
		}

		public static UIHandler Instance {
			get {
				if (_instance == null) {
					_instance = new UIHandler ();
				}
				return _instance;
			}
		}

		public void Init () {
			Debug.Log ("Init UIHandler");
			InitAcceptController ();
			
			InitDicesController ();
			
			InitHintController ();
		}

		private void InitAcceptController () {
			GameObject accept = GameObject.Find ("Btn_Accept");
			AcceptView acceptView = accept.GetComponent<AcceptView> ();
			_acceptController = new AcceptController (acceptView);
			_acceptController.View.Hide ();
		}

		private void InitDicesController () {
			GameObject dice_left = GameObject.Find ("Btn_Dice_Left");
			IDiceView diceView_left = dice_left.GetComponent<IDiceView> ();
			GameObject dice_right = GameObject.Find ("Btn_Dice_Right");
			IDiceView diceView_right = dice_right.GetComponent<IDiceView> ();
			
			IDiceController diceController_left = new DiceController (diceView_left);
			IDiceController diceController_right = new DiceController (diceView_right);
			_dicesController = new DicesController (diceController_left, diceController_right);
			_dicesController.DiceControllerArray [0].View.Disable ();
			_dicesController.DiceControllerArray [1].View.Disable ();
		}

		private void InitHintController () {
			GameObject hint = GameObject.Find ("Hint");
			HintView hintView = hint.GetComponent<HintView> ();
			_hintController = new HintController (hintView);
		}

		public DicesController DicesController {
			get {
				return _dicesController;
			}
		}

		public AcceptController AcceptController {
			get {
				return _acceptController;
			}
		}

		public HintController HintController {
			get {
				return _hintController;
			}
		}
	}

}