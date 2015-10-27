using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {

	public interface IHexagonController {

		IHexagonModel Model { get; }
		
		IHexagonView View { get; }

		Nullable<GridPos> Pred { get; set; }

		int Distance { get; set; }
	}

}