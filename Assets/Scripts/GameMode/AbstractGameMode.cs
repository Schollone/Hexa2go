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
			System.Random r = new System.Random (Guid.NewGuid ().GetHashCode ());
			int randomPlayer = r.Next (0, 2);
			_currentPlayer = players [randomPlayer];
		}

		public virtual IPlayer[] GetPlayers () {
			return this.players;
		}

		public virtual void Operate () {
			Debug.LogWarning ("Operate: " + matchState + " - " + _currentPlayer);
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
			Debug.Log("SetMatchState: " + stateName);
			matchState.OnExitState (_currentPlayer);
			matchState = state;
			GameManager.Instance.MatchStateChanged = true;
		}

		public virtual void SwitchToNextMatchState () {
			MatchStates nextState = matchState.GetNextState ();
			if (nextState != MatchStates.NullState) {
				SetMatchState (nextState);
			}
		}

		public virtual void SwitchPlayer () {
			if (_currentPlayer.Equals(players[0])) {
				_currentPlayer = players[1];
			} else {
				_currentPlayer = players[0];
			}
		}

	}
}

