using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public interface ICharacterView {

		void Init (GridPos gridPos, GridHelper.OffsetPosition offsetPosition);

		void Tint (Color color);

		void Select ();

		void Deselect ();

		void Move (GridPos gridPos, GridHelper.OffsetPosition offsetPosition, bool jump = false);

		void Remove ();

		void Rotate ();

	}
	
}