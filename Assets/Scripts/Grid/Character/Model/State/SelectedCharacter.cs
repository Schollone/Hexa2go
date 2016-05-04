using UnityEngine;

namespace Hexa2Go {

	public class SelectedCharacter : ICharacterState {

		private ICharacterModel _character;

		public SelectedCharacter(ICharacterModel character) {
			_character = character;
		}

		#region ICharacterState implementation
		public Color Color {
			get {
				return HexagonColors.ORANGE;
			}
		}
		public bool IsSelected {
			get {
				return true;
			}
		}

		public void MarkAsNormal () {
			_character.State = new NormalCharacter(_character);
		}

		public void MarkAsSelectable () {
			_character.State = new SelectableCharacter(_character);
		}

		public void MarkAsSelected () {

		}

		public void MarkAsBlocked () {
			_character.State = new BlockedCharacter(_character);
		}

		#endregion
	}
}

