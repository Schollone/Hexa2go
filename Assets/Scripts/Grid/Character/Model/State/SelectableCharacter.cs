using UnityEngine;

namespace Hexa2Go {

	public class SelectableCharacter : ICharacterState {

		private ICharacterModel _character;

		public SelectableCharacter(ICharacterModel character) {
			_character = character;
		}

		#region ICharacterState implementation
		public Color AreaColor {
			get {
				return HexagonColors.ORANGE;
			}
		}
		public Color BorderColor {
			get {
				return HexagonColors.GetColor(_character.TeamColor);
			}
		}
		public bool IsSelected {
			get {
				return false;
			}
		}

		public void MarkAsNormal () {
			_character.State = new NormalCharacter(_character);
		}

		public void MarkAsSelectable () {

		}

		public void MarkAsSelected () {
			_character.State = new SelectedCharacter(_character);
		}

		public void MarkAsBlocked () {
			_character.State = new BlockedCharacter(_character);
		}

		#endregion
	}
}

