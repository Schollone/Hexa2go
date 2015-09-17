using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class NextHexagonController : AbstractButtonController {

		public NextHexagonController(NextHexagonView view) : base(view) {
			
		}
		
		protected override void HandleOnClicked (object sender, ButtonClickedEventArgs e) {
			Debug.Log("HandleOnClicked");
			Debug.Log("Sender: " + sender.GetType() + " ; " + GameManager.Instance.MatchState);
			if (GameManager.Instance.MatchState == MatchState.SelectCharacter) {
				//GameManager.Instance.SetMatchState(MatchState.FocusCharacterTarget);
			}
			//View.Hide();
			GameManager.Instance.GridHandler.FocusNextHexagon();
		}

	}

}