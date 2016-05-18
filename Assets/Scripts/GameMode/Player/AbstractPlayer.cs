using System;
using UnityEngine;

namespace Hexa2Go {

	public abstract class AbstractPlayer : IPlayer {

		protected IPlayerModel _model;
		
		protected IPlayerView _view;

		protected StatsView _statsView;

		public AbstractPlayer () {
			GameObject currentPlayerDisplay = GameObject.Find ("CurrentPlayer");
			_view = currentPlayerDisplay.GetComponent<IPlayerView> ();
		}

		protected void HandleOnCharacterRemoved (object sender, EventArgs e) {
			_statsView.UpdateStats (_model.Name, _model.SavedCharacters);
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
		public StatsView StatsView {
			get {
				return _statsView;
			}
		}

		public virtual void ThrowDice () {
			throw new NotImplementedException ();
		}

		public virtual void Throwing () {
			throw new NotImplementedException ();
		}

		public virtual void SelectCharacter (bool hasFoundACharacter) {
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

