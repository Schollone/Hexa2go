using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class NextCharacterController : AbstractButtonController {

		public NextCharacterController (NextCharacterView view) : base(view) {
		}

		protected override void HandleOnClicked (object sender, ButtonClickedEventArgs e) {
			GameManager.Instance.GridHandler.SelectNextCharacter ();
		}
	}

}