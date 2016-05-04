using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexa2Go {

	public interface IHexagonController {

		IHexagonModel Model { get; }
		
		IHexagonView View { get; }

		Nullable<GridPos> Pred { get; set; }

		int Distance { get; set; }

		bool Visited { get; set; }
	}

}