using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class PlayerController : IPlayerController {

		private readonly IPlayerModel _model;
		
		private readonly IPlayerView _view;

		public PlayerController (TeamColor teamColor, string name) {
			GameObject player_change = GameObject.Find ("Player_Change");
			IPlayerView playerView = player_change.GetComponent<IPlayerView> ();
			
			_view = playerView;

			_model = new PlayerModel (teamColor, name);
		}

		#region IPlayerController implementation
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
		#endregion
	}

}