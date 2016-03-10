using UnityEngine;
using System.Collections;
using System;

namespace Hexa2Go {

	public interface IHexagonView {

		event EventHandler<EventArgs> OnClicked;

		Color DefaultAreaColor { get; set; }

		Color DefaultBorderColor { get; set; }

		bool IsActivated { get; }

		void Init (GridPos gridPos, IHexagonState state);

		void UpdateState (IHexagonState state);
		void Tint (IHexagonState state);
		void Activate (IHexagonState state, bool animated);
		void Deactivate (bool animated = false);


		void Select ();

		void Deselect ();

		void Focus ();

		void Focusable ();

		void ResetTint ();

		void Activate (Color? color = null, bool animate = false);

		//void Deactivate (bool animate = false);

		void PlayExplosion (bool playLoop = false);

		TeamColor TeamColor { get; set; }

	}

}