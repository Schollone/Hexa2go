using UnityEngine;

namespace Hexa2Go {

	public interface ICharacterController {

		ICharacterModel Model { get; }
		
		ICharacterView View { get; }

		int Distance { get; set; }
	}

}