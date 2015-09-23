using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class AcceptController : AbstractButtonController {

		public AcceptController (AcceptView view) : base(view) {
		}

		protected override void HandleOnClicked (object sender, ButtonClickedEventArgs e) {
			if (GameManager.Instance.MatchState == MatchState.SelectCharacter) {

				GameManager.Instance.MatchState = MatchState.FocusCharacterTarget;

			} else if (GameManager.Instance.MatchState == MatchState.FocusCharacterTarget) {

				GameManager.Instance.MatchState = MatchState.Moving;
				// Start Move Character Animation

			} else if (GameManager.Instance.MatchState == MatchState.SelectHexagon) {

				GameManager.Instance.MatchState = MatchState.FocusHexagonTarget;

			} else if (GameManager.Instance.MatchState == MatchState.FocusHexagonTarget) {

				GameManager.Instance.MatchState = MatchState.Moving;
				// Start Move Hexagon Animation

			}

		}
	}

}