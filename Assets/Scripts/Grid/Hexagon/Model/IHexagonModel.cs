using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexa2Go {

	public interface IHexagonModel {

		event EventHandler<EventArgs> OnUpdatedData;

		GridPos GridPos { get; }
		IHexagonState State { get; set; }
		IList<GridPos> Neighbors { get; }
		bool AddCharacter (ICharacterModel character);
		bool RemoveCharacter (ICharacterModel character);
		bool HasCharacter (ICharacterModel character);
		bool HasCharacter (TeamColor teamColor);
		IList<ICharacterModel> GetCharacters ();
		IList<ICharacterModel> GetCharacters (TeamColor teamColor);
		IList<ICharacterModel> GetCharacters (CharacterType type);
		IList<ICharacterModel> GetCharacters (CharacterType type, TeamColor teamColor);
		bool IsBlocked { get; }
		bool DontPropagate { get; set; }

	}

}