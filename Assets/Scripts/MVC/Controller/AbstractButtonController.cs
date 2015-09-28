using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {

	public abstract class AbstractButtonController : IButtonController {

		private readonly IButtonView _view;

		public AbstractButtonController (IButtonView view) {
			_view = view;
			_view.OnClicked += HandleOnClicked;
		}

		protected virtual void HandleOnClicked (object sender, ButtonClickedEventArgs e) {
			Debug.Log ("virtual HandleOnClicked");
			throw new NotImplementedException ();
		}

		#region IButtonController implementation
		public virtual IButtonView View {
			get {
				return _view;
			}
		}
		#endregion
	}

}