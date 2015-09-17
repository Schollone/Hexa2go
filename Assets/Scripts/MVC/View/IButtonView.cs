using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {

	public class ButtonClickedEventArgs : EventArgs {}

	public interface IButtonView {

		GameObject GameObject { get; }

		event EventHandler<ButtonClickedEventArgs> OnClicked;

		void Show();
		
		void Hide();
		
		void Enable();
		
		void Disable();
	}

}