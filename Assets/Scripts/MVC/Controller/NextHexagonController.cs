using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class NextHexagonController : AbstractButtonController {

		public NextHexagonController (NextHexagonView view) : base(view) {
		}
		
		protected override void HandleOnClicked (object sender, ButtonClickedEventArgs e) {
			if (GameManager.Instance.MatchState == MatchState.FocusCharacterTarget) {
				GameManager.Instance.GridHandler.FocusNextHexagon ();
			} else if (GameManager.Instance.MatchState == MatchState.SelectHexagon) {
				GameManager.Instance.GridHandler.SelectNextHexagon ();
			} else if (GameManager.Instance.MatchState == MatchState.FocusHexagonTarget) {
				GameManager.Instance.GridHandler.FocusNextHexagon (true);
			}
		}

	}

}