using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public interface IHexagonController {

		IHexagonModel Model { get; }
		
		IHexagonView View { get; }

		GridPos Pred { get; set; }
	}

}