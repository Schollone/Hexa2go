using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public interface ICharacterView {

		void Init(GridPos gridPos);

		void Tint(Color color);

		void Select ();

		void Deselect ();

	}
	
}