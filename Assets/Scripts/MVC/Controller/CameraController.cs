using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class CameraController {

		private readonly CameraView _view;

		public CameraController (CameraView view) {
			_view = view;

			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
		}

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			if (GameManager.Instance.ButtonHandler.DicesController.Pasch) {
				View.Zoom (2);
			}
		}

		public void Unregister () {
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
		}

		public CameraView View {
			get {
				return _view;
			}
		}

	}

}