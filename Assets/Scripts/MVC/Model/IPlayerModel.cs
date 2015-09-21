using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public interface IPlayerModel {

		TeamColor TeamColor { get; }

		int SavedCharacters { get; }

		string Name { get; set; }

	}

}