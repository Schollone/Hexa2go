using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexa2Go {

	public class CharacterFacade {

		private CharacterHandler _characterHandler;
		private ICharacterModel _selectedCharacter;

		public CharacterFacade () {
			_characterHandler = new CharacterHandler ();
		}

		public static Dictionary<CharacterType, string> PrefabNames = new Dictionary<CharacterType, string> {
			{CharacterType.CIRCLE, "Character_Circle"},
			{CharacterType.TRIANGLE, "Character_Triangle"},
			{CharacterType.SQUARE, "Character_Square"}
		};

		public ICharacterModel SelectedCharacter {
			get {
				return _selectedCharacter;
			}
		}

		public void InitCharacterSelection () {
			IList<ICharacterController> useableCharacters = _characterHandler.GetCharactersByDices ();

			foreach (ICharacterController character in useableCharacters) {
				if (GameManager.Instance.GridFacade.HexagonFacade.CheckCharacterMoveable(character)) {
					_selectedCharacter = character.Model;
					_selectedCharacter.State.MarkAsSelected ();
					return;
				}
			}

			GameManager.Instance.GetGameMode().SwitchPlayer();
			GameManager.Instance.GetGameMode().SetMatchState(MatchStates.ThrowDice);

		}

		public void SelectCharacter (CharacterType type, TeamColor teamColor) {
			ICharacterController controller = _characterHandler.GetCharacter (type, teamColor);
			Debug.Log ("CharacterFacade.SelectCharacter: " + controller.Model.Type + " - " + controller.Model.TeamColor);
			if (controller.Model.IsInGame) {
				// Remark last selected character
				if (_selectedCharacter != null) {
					_selectedCharacter.State.MarkAsNormal ();
				}

				// Mark next character
				_selectedCharacter = controller.Model;
				_selectedCharacter.State.MarkAsSelected ();
			}

		}

		public IList<ICharacterController> GetCharactersByDices () {
			return _characterHandler.GetCharactersByDices ();
		}

		public IList<ICharacterController> GetCharacters () {
			return _characterHandler.GetCharacters ();
		}

		public void ResetCharacters () {
			_characterHandler.ResetCharacters ();
		}

		public void ResetSelectionInfos () {
			_selectedCharacter = null;
		}

	}
}

