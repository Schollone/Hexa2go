using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public abstract class AbstractButtonController : IButtonController {

		private readonly IButtonView _view;

		protected IDictionary<ClickTypes, IClickCommand> clickCommandMap;

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