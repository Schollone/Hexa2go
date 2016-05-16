using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {

	public interface IPlayerModel {

		TeamColor TeamColor { get; }

		int SavedCharacters { get; }

		string Name { get; }

		void RemoveCharacter();

		event EventHandler<EventArgs> OnMatchFinished;
		event EventHandler<EventArgs> OnCharacterRemoved;

	}

}