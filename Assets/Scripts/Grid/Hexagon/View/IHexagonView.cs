using System;
using System.Collections;
using UnityEngine;

namespace Hexa2Go {

	public interface IHexagonView {

		event EventHandler<EventArgs> OnClicked;
		event EventHandler<EventArgs> OnCheckIsBlocked;

		bool IsActivated { get; }

		void Init (GridPos gridPos, IHexagonState state);

		void UpdateState (IHexagonState state);
		void Tint (IHexagonState state);
		void Activate (IHexagonState state, bool animated = false);
		void Deactivate (IHexagonState state, bool animated = false);

		void PlayExplosion (bool playLoop = false);
		void PlaySelectionClip ();
		void PlayFocusClip ();

	}

}