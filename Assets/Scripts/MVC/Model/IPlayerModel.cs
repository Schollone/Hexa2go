using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public interface IPlayerModel {

		TeamColor teamColor { get; }

		bool isComputer { get; }

	}

}