using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class PrevHexagonController : AbstractButtonController {

		public PrevHexagonController (PrevHexagonView view) : base(view) {
		}
		
		protected override void HandleOnClicked (object sender, ButtonClickedEventArgs e) {
			//GameManager.Instance.GetCurrentMatchState ().OnClickPrevHexagon ();
			IMatchState state = GameManager.Instance.GameModeHandler.GetGameMode ().GetMatchState ();
			if (state is FocusCharacterTarget) {
				ClickHandler.Instance.OnClick (ClickTypes.FocusCharacterTarget);
			} else if (state is SelectHexagon) {
				ClickHandler.Instance.OnClick (ClickTypes.SelectHexagon);
			} else if (state is FocusHexagonTarget) {
				ClickHandler.Instance.OnClick (ClickTypes.FocusHexagonTarget);
			}
			/*if (GameManager.Instance.MatchState == MatchState.FocusCharacterTarget) {
				GameManager.Instance.GridHandler.FocusPrevHexagon ();
			} else if (GameManager.Instance.MatchState == MatchState.SelectHexagon) {
				GameManager.Instance.GridHandler.SelectPrevHexagon ();
			} else if (GameManager.Instance.MatchState == MatchState.FocusHexagonTarget) {
				GameManager.Instance.GridHandler.FocusPrevHexagon (true);
			}*/

		}
	}

}