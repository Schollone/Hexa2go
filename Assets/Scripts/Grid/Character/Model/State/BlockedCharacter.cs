using UnityEngine;

namespace Hexa2Go {

	public class BlockedCharacter : ICharacterState {

		private ICharacterModel _character;

		public BlockedCharacter(ICharacterModel character) {
			_character = character;
		}

		#region ICharacterState implementation
		public Color AreaColor {
			get {
				return HexagonColors.GetColor(_character.TeamColor);
			}
		}
		public Color BorderColor {
			get {
				return HexagonColors.BLACK;
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
			_character.State = new SelectableCharacter(_character);
		}

		public void MarkAsSelected () {
			_character.State = new SelectedCharacter(_character);
		}

		public void MarkAsBlocked () {

		}

		#endregion
	}
}

