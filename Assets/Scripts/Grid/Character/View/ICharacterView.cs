using UnityEngine;

namespace Hexa2Go {

	public interface ICharacterView {

		void UpdateState (ICharacterState state);

		void Init (GridPos gridPos, GridHelper.OffsetPosition offsetPosition, Color color);

		void Tint (Color color);

		void Select ();

		void Deselect ();

		void Move (GridPos gridPos, GridHelper.OffsetPosition offsetPosition, bool jump = false);

		void Remove ();

		void Rotate ();

	}
	
}