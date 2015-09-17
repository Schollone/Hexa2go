using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class PlayerController : IPlayerController {

		private readonly IPlayerModel _playerModel_One;
		private readonly IPlayerModel _playerModel_Two;
		
		private readonly IPlayerView _playerView_One;
		private readonly IPlayerView _playerView_Two;
	}

}