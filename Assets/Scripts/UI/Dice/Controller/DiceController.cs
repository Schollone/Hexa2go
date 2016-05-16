using UnityEngine;
using System.Collections;
using System.Threading;

namespace Hexa2Go {

	public class DiceController : AbstractButtonController, IDiceController {

		private readonly IDiceView _view;
		private readonly IDiceModel _model;
		private Thread _thread;
		
		public DiceController (IDiceView view) : base(view) {
			_model = new DiceModel ();
			_model.OnDiceValueChanged += HandleOnDiceValueChanged;

			View.OnThrowed += HandleOnThrowed;
		}

		void HandleOnDiceValueChanged (object sender, DiceValueChangedEventArgs e) {
			View.UpdateView (e.DiceObject.CharacterType, e.DiceObject.TeamColor);
		}

		void HandleOnThrowed (object sender, DiceThrowedEventArgs e) {
			Model.SetDiceValue (e.DiceObject);
		}
		
		protected override void HandleOnClicked (object sender, ButtonClickedEventArgs e) {
			IMatchState state = GameManager.Instance.GetGameMode ().GetMatchState ();
			MatchStates matchStateName = GameManager.Instance.GetGameMode().GetMatchStateName(state);
			
			switch(matchStateName) {
				case MatchStates.ThrowDice: {
					ClickHandler.Instance.OnClick (ClickTypes.ThrowDice);
					break;
				}
				case MatchStates.SelectCharacter: {
					DiceObject diceObject = new DiceObject (Model.CharacterType, Model.TeamColor);
					ClickHandler.Instance.OnClick (ClickTypes.SelectCharacter, diceObject);
					break;
				}					
			}
		}

		#region IDiceController implementation
		public IDiceModel Model {
			get {
				return _model;
			}
		}

		public IDiceView View {
			get {
				return base.View as IDiceView;
			}
		}

		public void StartThrow () {
			View.StartThrow ();
		}
		#endregion
	}

}