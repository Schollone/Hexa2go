using UnityEngine;

namespace Hexa2Go {

	public interface ICharacterView {

		void UpdateState (ICharacterState state);

		void Init (GridPos gridPos, OffsetPosition offsetPosition, Color color);

		void Tint (Color areaColor, Color borderColor);

		void Select ();

		void Deselect ();

		void Move (GridPos gridPos, OffsetPosition offsetPosition, bool jump = false);

		void Remove ();

		void Rotate ();

	}
	
}