using UnityEngine;
using System;

namespace Hexa2Go {

	public interface IPlayer {
		IPlayerModel Model { get; }
		
		IPlayerView View { get; }

		StatsView StatsView { get; }

		void ThrowDice ();
		void Throwing ();
		void SelectCharacter (bool hasFoundACharacter);
		void SelectHexagon ();
		void HandleAcceptButton ();
		void Moving ();		
		void GameOver ();
	}
}

