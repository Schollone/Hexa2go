using System;
using UnityEngine;

namespace Hexa2Go {

	public class Multiplayer : AbstractGameMode {

		public Multiplayer ():base(2) {
		}

		public override void Init () {
			stateMap.Clear ();
			stateMap.Add (MatchStates.NullState, new NullState ());
			stateMap.Add (MatchStates.ThrowDice, new ThrowDice ());
			stateMap.Add (MatchStates.Throwing, new Throwing ());
			stateMap.Add (MatchStates.SelectCharacter, new SelectCharacter ());
			stateMap.Add (MatchStates.SelectHexagon, new SelectHexagon ());
			stateMap.Add (MatchStates.Moving, new Moving ());
			stateMap.Add (MatchStates.GameOver, new GameOverMP ());

			SetMatchState (MatchStates.NullState);
			
			players [0] = new Player (TeamColor.RED, 1);
			players [1] = new Player (TeamColor.BLUE, 2);
			
			base.Init ();
		}
	}
}

