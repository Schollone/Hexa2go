using UnityEngine;

namespace Hexa2Go {
	
	public class HintController {

		private HintView _view;

		public HintController (HintView view) {
			_view = view;
		}

		public virtual HintView View {
			get {
				return _view;
			}
		}
	}

}
