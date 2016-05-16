using System;
using System.Collections;
using UnityEngine;

namespace Hexa2Go {

	public interface ICharacterModel {

		GridPos GridPos { get; set; }
		CharacterType Type { get; }
		TeamColor TeamColor { get; }
		ICharacterState State { get; set; }

		event EventHandler<EventArgs> OnUpdatedData;
		event EventHandler<EventArgs> OnGridPosChanged;
		event EventHandler<EventArgs> OnTargetReached;


		bool IsInGame { get; }

		OffsetPosition OffsetPosition { get; set; }

		void Remove ();
	}

}