using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class PrevHexagonController : AbstractButtonController {

		public PrevHexagonController(PrevHexagonView view) : base(view) {
			
		}
		
		protected override void HandleOnClicked (object sender, ButtonClickedEventArgs e) {
			if (GameManager.Instance.MatchState == MatchState.SelectCharacter) {
				Debug.LogWarning("Shouldn't call");
				GameManager.Instance.MatchState = MatchState.FocusCharacterTarget;
			}
			GameManager.Instance.GridHandler.FocusPrevHexagon();
		}
	}

}