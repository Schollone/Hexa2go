using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public interface ICharacterController {

		ICharacterModel Model { get; }
		
		ICharacterView View { get; }

		int Distance { get; set; }
	}

}