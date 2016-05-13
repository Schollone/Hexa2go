using System;
using System.Collections.Generic;

namespace Hexa2Go {

	public interface IGameMode {

		void Init ();
		void Operate ();
		IMatchState GetMatchState ();
		MatchStates GetMatchStateName (IMatchState state);
		void SetMatchState (MatchStates state);
		void SwitchToNextMatchState ();
		IPlayer[] GetPlayers ();
		IPlayer CurrentPlayer { get; }
		//IDictionary<MatchStates, IMatchState> GetStateMap ();
		void SwitchPlayer ();

	}
}

