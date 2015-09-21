﻿using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public interface IPlayerController {

		IPlayerModel Model { get; }
		
		IPlayerView View { get; }

	}

}