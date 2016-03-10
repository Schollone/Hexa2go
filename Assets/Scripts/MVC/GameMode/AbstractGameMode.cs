using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Hexa2Go {

	public abstract class AbstractGameMode : IGameMode {

		protected IPlayer[] players;
		protected IMatchState matchState;
		protected IPlayer currentPlayer;
		protected IDictionary<MatchStates, IMatchState> stateMap;

		public AbstractGameMode (int amountOfPlayers) {
			Debug.Log ("AbstractGameMode");
			stateMap = new Dictionary<MatchStates, IMatchState> ();

			matchState = new NullState ();

			players = new IPlayer[amountOfPlayers];
		}

		public virtual void Init () {
			Debug.Log ("Init GameMode");
			System.Random r = new System.Random ();
			int randomPlayer = r.Next (0, 2);
			currentPlayer = players [randomPlayer];
		}

		public virtual IPlayer[] GetPlayers () {
			return this.players;
		}

		public virtual void Operate () {
			Debug.Log ("Operate " + matchState + " currentPlayer: " + currentPlayer);
			matchState.Operate (currentPlayer);
		}

		public virtual IMatchState GetMatchState () {
			return matchState;
		}

		public virtual void SetMatchState (IMatchState state) {
			matchState = state;
			GameManager.Instance.MatchStateChanged = true;
			Debug.Log ("SetMatchState: " + matchState);
		}

		public virtual void SwitchToNextState () {
			IMatchState nextState = matchState.GetNextState ();
			SetMatchState (nextState);
		}

		public virtual IDictionary<MatchStates, IMatchState> GetStateMap () {
			return stateMap;
		}

		/*protected PlayerHandler _playerHandler;

		public AbstractGameMode () {
		}

		public PlayerHandler PlayerHandler {
			get {
				return _playerHandler;
			}
		}

		public void Init () {
			_playerHandler = new PlayerHandler ();

			GameManager.Instance.OnMatchStateChange += HandleOnMatchStateChange;
			
			System.Random r = new System.Random ();
			int randomPlayer = r.Next (1, 3);
			GameManager.Instance.PlayerState = (PlayerState)randomPlayer;
		}

		public void updateGUI () {

		}

		public void UpdateAI () {

		}

		public void updatePlayers () {

		}

		public void Unregister () {
			GameManager.Instance.OnMatchStateChange -= HandleOnMatchStateChange;
			if (_playerHandler != null) {
				_playerHandler.Unregister ();
			}
		}

		public void HandleOnMatchStateChange (MatchState prevMatchState, MatchState nextMatchState) {
			switch (nextMatchState) {
				case MatchState.SelectCharacter:
					{
						if (GameManager.Instance.GridHandler.SelectedCharacter != null) {
							GameManager.Instance.GridHandler.TintCharacter ();
						
						} else {
							SwitchToNextPlayer ();
						}
						break;
					
					}
				case MatchState.Moving:
					{
						if (GameManager.Instance.GameFinished) {
							GameManager.Instance.GameFinished = false;
							GameManager.Instance.MatchState = MatchState.Win;
						} else {
							SwitchToNextPlayer ();
						}
						break;
					
					}
			}
			
		}

		public void SwitchToNextPlayer () {
			PlayerState playerState = (GameManager.Instance.PlayerState == PlayerState.Player) ? PlayerState.Opponent : PlayerState.Player;
			GameManager.Instance.PlayerState = playerState;
			//GameManager.Instance.MatchState = MatchState.ThrowDice;
			GameManager.Instance.SetCurrentMatchState (new ThrowDice ());
		}*/

	}
}

