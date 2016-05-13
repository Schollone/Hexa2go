using System;
using UnityEngine;

namespace Hexa2Go {

	public abstract class AbstractPlayer : IPlayer {

		protected IPlayerModel _model;
		
		protected IPlayerView _view;

		public AbstractPlayer () {
			GameObject player_change = GameObject.Find ("Player_Change");
			IPlayerView playerView = player_change.GetComponent<IPlayerView> ();
			
			_view = playerView;
		}

		protected void HandleOnMatchFinished (object sender, EventArgs e) {
			GameManager.Instance.GetGameMode().SetMatchState(MatchStates.GameOver);
		}

		#region IPlayer implementation
		public IPlayerModel Model {
			get {
				return _model;
			}
		}
		public IPlayerView View {
			get {
				return _view;
			}
		}

		public virtual void ThrowDice () {
			throw new NotImplementedException ();
		}

		public virtual void Throwing () {
			throw new NotImplementedException ();
		}

		public virtual void SelectCharacter () {
			throw new NotImplementedException ();
		}

		public virtual void SelectHexagon () {
			throw new NotImplementedException ();
		}

		public virtual void HandleAcceptButton () {
			throw new NotImplementedException ();
		}

		public virtual void Moving () {
			throw new NotImplementedException ();
		}

		public virtual void GameOver () {
			GameManager.Instance.GridFacade.HexagonFacade.PlayEndlessExplosion(_model.TeamColor);
		}
		#endregion
	}
}

