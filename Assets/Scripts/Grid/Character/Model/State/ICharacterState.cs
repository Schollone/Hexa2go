using UnityEngine;

namespace Hexa2Go {

	public interface ICharacterState {

		Color Color { get; }
		bool IsSelected { get; }

		void MarkAsNormal ();
		void MarkAsSelectable ();
		void MarkAsSelected ();
		void MarkAsBlocked ();

	}
}

