using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class NextCharacterController : AbstractButtonController {

		public NextCharacterController (NextCharacterView view) : base(view) {
		}

		protected override void HandleOnClicked (object sender, ButtonClickedEventArgs e) {
			/*if (GameManager.Instance.MatchState == MatchState.SelectCharacter) {
				GameManager.Instance.GridHandler.SelectNextCharacter ();
			}*/
			//GameManager.Instance.GetCurrentMatchState ().OnClickNextCharacter ();
			IMatchState state = GameManager.Instance.GameModeHandler.GetGameMode ().GetMatchState ();
			if (state is SelectCharacter) {
				ClickHandler.Instance.OnClick (ClickTypes.SelectCharacter);
			}
			//GameManager.Instance.GridHandler.SelectNextCharacter ();
		}
	}

}