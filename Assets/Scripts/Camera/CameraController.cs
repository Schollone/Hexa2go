using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class CameraController {

		private readonly CameraView _view;

		public CameraController (CameraView view) {
			_view = view;
		}

		public void ActivateMovingCamera () {
			_view.GetComponent<Animator>().enabled = false;
		}

		public CameraView View {
			get {
				return _view;
			}
		}

	}

}