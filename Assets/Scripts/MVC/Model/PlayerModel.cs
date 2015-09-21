using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class PlayerModel : IPlayerModel {

		private TeamColor _teamColor;
		private int _savedCharacters = 0;
		private string _name = "";
		
		public PlayerModel (TeamColor teamColor) {
			_teamColor = teamColor;
			_savedCharacters = 0;
		}
		
		public TeamColor TeamColor {
			get {
				return _teamColor;
			}
		}

		public int SavedCharacters {
			get {
				return _savedCharacters;
			}
		}

		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}
	}

}