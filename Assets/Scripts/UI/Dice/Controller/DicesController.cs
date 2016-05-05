using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public class DicesController {

		private IDiceController[] _diceControllerArray;

		private bool _diceThrowed;
		private bool _double = false;
		
		public DicesController (IDiceController diceController_left, IDiceController diceController_right) {
			_diceControllerArray = new IDiceController[2];
			_diceControllerArray [0] = diceController_left;
			_diceControllerArray [1] = diceController_right;

			_diceControllerArray [0].Model.OnDiceValueChanged += HandleOnDiceValueChanged;
			_diceControllerArray [1].Model.OnDiceValueChanged += HandleOnDiceValueChanged;
		}

		public IDiceController[] DiceControllerArray {
			get {
				return _diceControllerArray;
			}
		}

		public void ResetDicesBackground () {
			_diceControllerArray [0].View.UpdateBackground(false);
			_diceControllerArray [1].View.UpdateBackground(false);
		}

		public void SelectCharacter (CharacterType type, TeamColor teamColor) {
			for (int i=0; i < _diceControllerArray.Length; i++) {
				IDiceController controller = _diceControllerArray[i];

				if (controller.Model.CharacterType == type) {
					controller.View.UpdateBackground(true);
					break;
				}
			}
		}

		void HandleOnDiceValueChanged (object sender, DiceValueChangedEventArgs e) {

			if (!_diceThrowed) {
				_diceThrowed = true;
			} else {
				//Debug.LogWarning (_diceController_left.Model.CharacterType + " - " + _diceController_right.Model.CharacterType);
				if (Double) {
					_diceControllerArray [0].View.PlayDoubleDice();
					GameManager.Instance.GetGameMode().SetMatchState(MatchStates.SelectHexagon);
				} else {
					GameManager.Instance.GetGameMode().SetMatchState(MatchStates.SelectCharacter);
				}

				_diceThrowed = false;
			}
		}

		public void Enable () {
			_diceControllerArray [0].View.Enable ();
			_diceControllerArray [1].View.Enable ();
		}

		public void Disable () {
			_diceControllerArray [0].View.Disable ();
			_diceControllerArray [1].View.Disable ();
		}

		public void Show () {
			_diceControllerArray [0].View.Show ();
			_diceControllerArray [1].View.Show ();
		}
		
		public void Hide () {
			_diceControllerArray [0].View.Hide ();
			_diceControllerArray [1].View.Hide ();
		}

		public void StartThrow () {
			_diceControllerArray [0].StartThrow ();
			_diceControllerArray [1].StartThrow ();

			_diceControllerArray [0].View.PlayDiceRoll();
		}

		public bool Double {
			get {
				_double = false;
				if (_diceControllerArray [0].Model.TeamColor == TeamColor.NONE)
					return _double;
				if (_diceControllerArray [0].Model.CharacterType == _diceControllerArray [1].Model.CharacterType &&
					_diceControllerArray [0].Model.TeamColor == _diceControllerArray [1].Model.TeamColor) {
					//Debug.LogWarning (_diceController_left.Model.CharacterType + " == " + _diceController_right.Model.CharacterType);
					//Debug.LogWarning (_diceController_left.Model.TeamColor + " == " + _diceController_right.Model.TeamColor);
					_double = true;
				}
				return _double;
				//return true;
			}
		}
	}

}