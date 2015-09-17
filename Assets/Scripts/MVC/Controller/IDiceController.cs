using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public interface IDiceController {

		IDiceModel Model { get; }

		IDiceView View { get; }

		void StartThrow();
	}

}