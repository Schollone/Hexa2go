﻿using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public interface IHexagonView {

		Color DefaultAreaColor { get; set; }

		Color DefaultBorderColor { get; set; }

		void Init (GridPos gridPos);

		void Select ();

		void Deselect ();

		void Focus ();

		void Focusable ();

		void ResetTint ();

		void Activate (Color? color = null, bool animate = false);

		void Deactivate (bool animate = false);

		void PlayExplosion (bool playLoop = false);

		Vector3 SlotPosition1 { get; }

		Vector3 SlotPosition2 { get; }

		TeamColor TeamColor { get; set; }

	}

}