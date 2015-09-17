﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public class CharacterHandler {

		private readonly ICharacterController _characterController_Circle;
		private readonly ICharacterController _characterController_Triangle;
		private readonly ICharacterController _characterController_Square;

		//private ArrayList _characterControllers;
		private IDictionary<CharacterType, ICharacterController> _characters;

		private ArrayList _selectedCharacters = null;
		private int _selectedCharacterIndex = 0;

		//private IDictionary

		public CharacterHandler(TeamColor teamColor) {
			if (teamColor == TeamColor.RED) {

				_characterController_Circle = new CharacterController(new GridPos(2, 3), "Character_Circle");
				_characterController_Square = new CharacterController(new GridPos(3, 2), "Character_Square");
				_characterController_Triangle = new CharacterController(new GridPos(3, 3), "Character_Triangle");

				_characterController_Circle.View.Tint(HexagonColors.RED);
				_characterController_Square.View.Tint(HexagonColors.RED);
				_characterController_Triangle.View.Tint(HexagonColors.RED);

			} else {

				_characterController_Circle = new CharacterController(new GridPos(7, 3), "Character_Circle");
				_characterController_Square = new CharacterController(new GridPos(6, 4), "Character_Square");
				_characterController_Triangle = new CharacterController(new GridPos(6, 3), "Character_Triangle");
				
				_characterController_Circle.View.Tint(HexagonColors.BLUE);
				_characterController_Square.View.Tint(HexagonColors.BLUE);
				_characterController_Triangle.View.Tint(HexagonColors.BLUE);

			}

			_selectedCharacters = new ArrayList();

			_characters = new Dictionary<CharacterType, ICharacterController>();
			_characters.Add(CharacterType.CIRCLE, _characterController_Circle);
			_characters.Add(CharacterType.SQUARE, _characterController_Square);
			_characters.Add(CharacterType.TRIANGLE, _characterController_Triangle);

			/*
			_characterControllers = new ArrayList();
			_characterControllers.Add(_characterController_Circle);
			_characterControllers.Add(_characterController_Square);
			_characterControllers.Add(_characterController_Triangle);*/
		}

		public IDictionary<CharacterType, ICharacterController> Characters {
			get {
				return _characters;
			}
			set {
				_characters = value;
			}
		}

		public void InitSelectedCharacters(CharacterType type1, CharacterType type2) {
			_selectedCharacters.Clear();
			ICharacterController characterController = null;
			_characters.TryGetValue(type1, out characterController);
			_selectedCharacters.Add(characterController);
			_characters.TryGetValue(type2, out characterController);
			_selectedCharacters.Add(characterController);
		}

		public ICharacterController SelectNextCharacter() {
			ICharacterController character = (ICharacterController) _selectedCharacters[_selectedCharacterIndex];
			character.Model.Deselect();
			GameManager.Instance.GridHandler.HexagonHandler.Deselect(character.Model.GridPos);
			_selectedCharacterIndex++;
			if (_selectedCharacterIndex >= _selectedCharacters.Count) {
				_selectedCharacterIndex = 0;
			}
			character = (ICharacterController) _selectedCharacters[_selectedCharacterIndex];
			GameManager.Instance.GridHandler.HexagonHandler.Select(character.Model.GridPos);
			character.Model.Select();
			return character;
			//GameManager.Instance.GridHandler.CharacterHandler_P1.Characters;
			//GameManager.Instance.ButtonHandler.DicesController.DiceController_left.Model.CharacterType;

		}

	}

}