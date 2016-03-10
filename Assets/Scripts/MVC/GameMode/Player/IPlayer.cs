using UnityEngine;
using System;

namespace Hexa2Go {

	public interface IPlayer {
		IPlayerModel Model { get; }
		
		IPlayerView View { get; }

		void ThrowDice ();
		void Throwing ();
		void SelectCharacter ();
		void FocusCharacterTarget ();
		void SelectHexagon ();
		void FocusHexagonTarget ();
		void Moving ();		
		void GameOver ();
	}
}

