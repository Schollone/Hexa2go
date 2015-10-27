using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class DicesController {

		private readonly IDiceController _diceController_left;
		private readonly IDiceController _diceController_right;

		private bool _diceThrowed;
		private bool _double = false;
		
		public DicesController (IDiceController diceController_left, IDiceController diceController_right) {
			_diceController_left = diceController_left;
			_diceController_right = diceController_right;

			_diceController_left.Model.OnDiceValueChanged += HandleOnDiceValueChanged;
			_diceController_right.Model.OnDiceValueChanged += HandleOnDiceValueChanged;
		}

		public IDiceController DiceController_left {
			get {
				return _diceController_left;
			}
		}

		public IDiceController DiceController_right {
			get {
				return _diceController_right;
			}
		}

		void HandleOnDiceValueChanged (object sender, DiceValueChangedEventArgs e) {

			if (!_diceThrowed) {
				_diceThrowed = true;
			} else {
				//Debug.LogWarning (_diceController_left.Model.CharacterType + " - " + _diceController_right.Model.CharacterType);
				if (Double) {
					GameManager.Instance.MatchState = MatchState.SelectHexagon;
				} else {
					GameManager.Instance.MatchState = MatchState.SelectCharacter;
				}

				_diceThrowed = false;
			}
		}

		public void Enable () {
			_diceController_left.View.Enable ();
			_diceController_right.View.Enable ();
		}

		public void Disable () {
			_diceController_left.View.Disable ();
			_diceController_right.View.Disable ();
		}

		public void Show () {
			_diceController_left.View.Show ();
			_diceController_right.View.Show ();
		}
		
		public void Hide () {
			_diceController_left.View.Hide ();
			_diceController_right.View.Hide ();
		}

		public void StartThrow () {
			_diceController_left.StartThrow ();
			_diceController_right.StartThrow ();
		}

		public bool Double {
			get {
				_double = false;
				if (_diceController_left.Model.TeamColor == TeamColor.NONE)
					return _double;
				if (_diceController_left.Model.CharacterType == _diceController_right.Model.CharacterType &&
					_diceController_left.Model.TeamColor == _diceController_right.Model.TeamColor) {
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