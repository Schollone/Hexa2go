using System;

namespace Hexa2Go {

	public struct DiceObject {

		private CharacterType _characterType;
		private TeamColor _teamColor;

		public DiceObject (CharacterType type, TeamColor color) {
			this._characterType = type;
			this._teamColor = color;
		}

		public CharacterType CharacterType {
			get {
				return _characterType;
			}
			set {
				_characterType = value;
			}
		}

		public TeamColor TeamColor {
			get {
				return _teamColor;
			}
			set {
				_teamColor = value;
			}
		}
	}

}