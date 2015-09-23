using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hexa2Go {

	public class CharacterHandler {

		private readonly ICharacterController _characterController_Circle;
		private readonly ICharacterController _characterController_Triangle;
		private readonly ICharacterController _characterController_Square;

		private IDictionary<CharacterType, ICharacterController> _characters;

		private ArrayList _selectedCharacters = null;
		private int _selectedCharacterIndex = 0;

		public CharacterHandler (TeamColor teamColor) {
			if (teamColor == TeamColor.RED) {

				_characterController_Circle = new CharacterController (new GridPos (5, 2), "Character_Circle", teamColor);
				_characterController_Square = new CharacterController (new GridPos (6, 3), "Character_Square", teamColor);
				_characterController_Triangle = new CharacterController (new GridPos (6, 4), "Character_Triangle", teamColor);

				_characterController_Circle.View.Tint (HexagonColors.RED);
				_characterController_Square.View.Tint (HexagonColors.RED);
				_characterController_Triangle.View.Tint (HexagonColors.RED);

			} else {

				_characterController_Circle = new CharacterController (new GridPos (4, 2), "Character_Circle", teamColor);
				_characterController_Square = new CharacterController (new GridPos (3, 2), "Character_Square", teamColor);
				_characterController_Triangle = new CharacterController (new GridPos (3, 3), "Character_Triangle", teamColor);
				
				_characterController_Circle.View.Tint (HexagonColors.BLUE);
				_characterController_Square.View.Tint (HexagonColors.BLUE);
				_characterController_Triangle.View.Tint (HexagonColors.BLUE);

			}

			_selectedCharacters = new ArrayList ();

			_characters = new Dictionary<CharacterType, ICharacterController> ();
			_characters.Add (CharacterType.CIRCLE, _characterController_Circle);
			_characters.Add (CharacterType.SQUARE, _characterController_Square);
			_characters.Add (CharacterType.TRIANGLE, _characterController_Triangle);
		}

		public IDictionary<CharacterType, ICharacterController> Characters {
			get {
				return _characters;
			}
			set {
				_characters = value;
			}
		}

		public void InitSelectedCharacters (CharacterType type1, CharacterType type2) {
			_selectedCharacters.Clear ();
			_selectedCharacterIndex = 0;
			ICharacterController characterController = null;
			_characters.TryGetValue (type1, out characterController);
			if (characterController.Model.IsInGame) {
				_selectedCharacters.Add (characterController);
			}
			_characters.TryGetValue (type2, out characterController);
			if (characterController.Model.IsInGame) {
				_selectedCharacters.Add (characterController);
			}
		}

		public ICharacterController SelectNextCharacter () {
			ICharacterController character = null;
			if (_selectedCharacters.Count > 0) {
				character = (ICharacterController)_selectedCharacters [_selectedCharacterIndex];
				character.Model.Deselect ();
				GameManager.Instance.GridHandler.HexagonHandler.Deselect (character.Model.GridPos);
				_selectedCharacterIndex++;
				if (_selectedCharacterIndex >= _selectedCharacters.Count) {
					_selectedCharacterIndex = 0;
				}
				character = (ICharacterController)_selectedCharacters [_selectedCharacterIndex];
				GameManager.Instance.GridHandler.HexagonHandler.Select (character.Model.GridPos);
				character.Model.Select ();
			}
			return character;
		}

		public IList<ICharacterController> GetCharacters (GridPos gridPos) {
			IList<ICharacterController> result = new List<ICharacterController> ();

			foreach (ICharacterController character in Characters.Values) {
				if (character.Model.GridPos.Equals (gridPos)) {
					result.Add (character);
				}
			}

			return result;
		}

	}

}