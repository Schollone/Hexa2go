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
			//GameManager.Instance.GetCurrentMatchState ().OnClickDice (this);
			IMatchState state = GameManager.Instance.GameModeHandler.GetGameMode ().GetMatchState ();
			if (state is ThrowDice) {
				ClickHandler.Instance.OnClick (ClickTypes.ThrowDice);
			} else if (state is SelectCharacter) {
				ClickHandler.Instance.OnClick (ClickTypes.AcceptCharacter);
			}
			/*if (GameManager.Instance.MatchState == MatchState.ThrowDice) {
				//GameManager.Instance.MatchState = MatchState.Throwing;
			} else if (GameManager.Instance.MatchState == MatchState.SelectCharacter) {
				GameManager.Instance.GridHandler.SelectNextCharacter ();
				Debug.Log (sender.ToString ());
				ICharacterController controller = GameManager.Instance.GridHandler.CharacterHandler_P1.GetCharacter (Model.CharacterType);
				controller.Model.Deselect ();
				GameManager.Instance.GridHandler.HexagonHandler.Deselect (controller.Model.GridPos);
			}*/
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