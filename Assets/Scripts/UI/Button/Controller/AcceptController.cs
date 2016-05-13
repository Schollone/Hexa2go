using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class AcceptController : AbstractButtonController {

		public AcceptController (AcceptView view) : base(view) {
		}

		protected override void HandleOnClicked (object sender, ButtonClickedEventArgs e) {
			IMatchState state = GameManager.Instance.GetGameMode ().GetMatchState ();
			MatchStates matchStateName = GameManager.Instance.GetGameMode().GetMatchStateName(state);

			switch(matchStateName) {
				case MatchStates.SelectCharacter: {
					ClickHandler.Instance.OnClick (ClickTypes.FinishCharacterMove);
					break;
				}
				case MatchStates.SelectHexagon: {
					ClickHandler.Instance.OnClick (ClickTypes.FinishHexagonMove);
					break;
				}
				case MatchStates.GameOver: {
					Application.LoadLevel (0);
					break;
				}
				
			}

		}
	}

}