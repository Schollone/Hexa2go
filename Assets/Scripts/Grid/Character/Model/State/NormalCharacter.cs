using UnityEngine;

namespace Hexa2Go {

	public class NormalCharacter : ICharacterState {

		private ICharacterModel _character;

		public NormalCharacter(ICharacterModel character) {
			_character = character;
		}

		#region ICharacterState implementation
		public Color Color {
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

		}

		public void MarkAsSelectable () {
			_character.State = new SelectableCharacter(_character);
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

