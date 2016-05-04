using System;
using System.Collections.Generic;

namespace Hexa2Go {

	public interface IGameMode {

		void Init ();
		void Operate ();
		IMatchState GetMatchState ();
		MatchStates GetMatchStateName (IMatchState state);
		void SetMatchState (MatchStates state);
		void SwitchToNextState ();
		IPlayer[] GetPlayers ();
		IPlayer CurrentPlayer { get; }
		//IDictionary<MatchStates, IMatchState> GetStateMap ();
		void SwitchPlayer ();

		/*
		void Init ();
		void updateGUI ();

		void updatePlayers ();

		PlayerHandler PlayerHandler { get; }

		void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState);
		void Unregister ();

		void SwitchToNextPlayer ();



		void UpdateThrowDiceGUI ();
		void UpdateThrowingGUI ();
		void UpdateSelectCharacterGUI ();
		void UpdateFocusCharacterTargetGUI ();
		void UpdateSelectHexagonGUI ();
		void UpdateFocusHexagonTargetGUI ();
		void UpdateMovingGUI ();
		void UpdateGameOverGUI ();

		void UpdateAIThrowDice ();
		void UpdateAIFocusCharacterTarget ();*/

	}
}

