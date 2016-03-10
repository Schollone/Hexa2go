using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class AcceptController : AbstractButtonController {

		public AcceptController (AcceptView view) : base(view) {
		}

		protected override void HandleOnClicked (object sender, ButtonClickedEventArgs e) {
			//MatchState matchState = GameManager.Instance.MatchState;
			//GameManager.Instance.GetCurrentMatchState ().OnClickAccept ();
			IMatchState state = GameManager.Instance.GameModeHandler.GetGameMode ().GetMatchState ();
			if (state is SelectCharacter) {
				ClickHandler.Instance.OnClick (ClickTypes.AcceptCharacter);
			}
			/*switch (matchState) {
				case MatchState.SelectCharacter:
					{
						GameManager.Instance.SetCurrentMatchState (new FocusCharacterTarget ());
						//GameManager.Instance.MatchState = MatchState.FocusCharacterTarget;
						break;
					}
				case MatchState.FocusCharacterTarget:
				case MatchState.FocusHexagonTarget:
					{
						GameManager.Instance.SetCurrentMatchState (new Moving ());
						//GameManager.Instance.MatchState = MatchState.Moving;
						break;
					}
				case MatchState.SelectHexagon:
					{
						GameManager.Instance.SetCurrentMatchState (new FocusHexagonTarget ());
						//GameManager.Instance.MatchState = MatchState.FocusHexagonTarget;
						break;
					}
				case MatchState.Win:
					{
						Application.LoadLevel (0);
						break;
					}
			}*/

		}
	}

}