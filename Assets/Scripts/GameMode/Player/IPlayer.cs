using UnityEngine;
using System;

namespace Hexa2Go {

	public interface IPlayer {
		IPlayerModel Model { get; }
		
		IPlayerView View { get; }

		void ThrowDice ();
		void Throwing ();
		void SelectCharacter ();
		void SelectHexagon ();
		void HandleAcceptButton ();
		void Moving ();		
		void GameOver ();
	}
}

