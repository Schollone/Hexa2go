using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hexa2Go {

	public abstract class AbstractGameMode : IGameMode {

		protected IPlayer[] players;
		protected IMatchState matchState;
		protected IPlayer _currentPlayer;
		protected IDictionary<MatchStates, IMatchState> stateMap;

		public AbstractGameMode (int amountOfPlayers) {
			Debug.Log ("AbstractGameMode");
			stateMap = new Dictionary<MatchStates, IMatchState> ();

			matchState = new NullState ();

			players = new IPlayer[amountOfPlayers];
		}

		public virtual IPlayer CurrentPlayer {
			get {
				return _currentPlayer;
			}
		}

		public virtual void Init () {
			Debug.Log ("Init GameMode");
			System.Random r = new System.Random ();
			int randomPlayer = r.Next (0, 2);
			//_currentPlayer = players [randomPlayer];
			_currentPlayer = players [0];
		}

		public virtual IPlayer[] GetPlayers () {
			return this.players;
		}

		public virtual void Operate () {
			Debug.LogWarning ("Operate " + matchState + " currentPlayer: " + _currentPlayer + " TeamColor: " + _currentPlayer.Model.TeamColor);
			matchState.Operate (_currentPlayer);
		}

		public virtual IMatchState GetMatchState () {
			return matchState;
		}

		public virtual MatchStates GetMatchStateName (IMatchState state) {
			return stateMap.Where(p => p.Value == state).Select(p => p.Key).First();
		}

		public virtual void SetMatchState (MatchStates stateName) {
			IMatchState state = null;
			stateMap.TryGetValue(stateName, out state);

			matchState.OnExitState (_currentPlayer);
			matchState = state;
			GameManager.Instance.MatchStateChanged = true;
			Debug.Log ("SetMatchState: " + matchState);
		}

		public virtual void SwitchToNextState () {
			MatchStates nextState = matchState.GetNextState ();
			if (nextState != MatchStates.NullState) {
				SetMatchState (nextState);
			}
		}

		public virtual void SwitchPlayer () {
			if (_currentPlayer.Equals(players[0])) {
				Debug.LogWarning("Player 2");
				_currentPlayer = players[1];
			} else {
				Debug.LogWarning("Player 1");
				_currentPlayer = players[0];
			}
		}

	}
}

