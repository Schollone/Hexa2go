using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hexa2Go {

	public class GridFacade {

		private HexagonFacade _hexagonFacade;
		private CharacterFacade _characterFacade;

		public GridFacade () {
			_hexagonFacade = new HexagonFacade ();
			_characterFacade = new CharacterFacade ();

			ReferCharactersToItsHexagons ();

		}

		public HexagonFacade HexagonFacade {
			get {
				return _hexagonFacade;
			}
		}

		public CharacterFacade CharacterFacade {
			get {
				return _characterFacade;
			}
		}

		private void ReferCharactersToItsHexagons () {
			// register characters to its hexagons
			IList<ICharacterController> characterControllers = _characterFacade.GetCharacters ();
			foreach (ICharacterController character in characterControllers) {
				_hexagonFacade.ReferCharacterToItsHexagon (character.Model);
			}
		}

		public bool InitCharacterSelection () {
			return _characterFacade.InitCharacterSelection ();
		}

		public void SelectCharacter (CharacterType type, TeamColor teamColor) {
			if (_characterFacade.SelectedCharacter == null || _characterFacade.SelectedCharacter.Type != type) {
				UIHandler.Instance.DicesController.ResetDicesBackground ();

				_characterFacade.SelectCharacter (type, teamColor);
				_hexagonFacade.SelectCharacter (_characterFacade.SelectedCharacter.GridPos);

				UIHandler.Instance.DicesController.SelectCharacter (type, teamColor);
			}
		}

		public void MoveCharacter () {
			_characterFacade.SelectedCharacter.GridPos = _hexagonFacade.FocusedHexagon.GridPos;
			_hexagonFacade.FocusedHexagon.AddCharacter (_characterFacade.SelectedCharacter);
			_hexagonFacade.SelectedHexagon.RemoveCharacter (_characterFacade.SelectedCharacter);

			CheckReachedTarget ();
		}

		private void CheckReachedTarget () {
			if (_hexagonFacade.FocusedHexagon.State.TeamColor == _characterFacade.SelectedCharacter.TeamColor) {
				_characterFacade.SelectedCharacter.Remove ();
				GameManager.Instance.GetGameMode ().CurrentPlayer.Model.RemoveCharacter ();

				_hexagonFacade.RemoveCharacter (_characterFacade.SelectedCharacter);
			}
		}

		public void MoveHexagon () {
			IList<ICharacterModel> characters = _hexagonFacade.SelectedHexagon.GetCharacters ();

			ICharacterModel[] models = new ICharacterModel[characters.Count];
			characters.CopyTo (models, 0);

			_hexagonFacade.FocusedHexagon.State.Activate (_hexagonFacade.SelectedHexagon.State.TeamColor);

			foreach (ICharacterModel character in models) {
				_hexagonFacade.FocusedHexagon.AddCharacter (character, true);
				_hexagonFacade.SelectedHexagon.RemoveCharacter (character);
				character.GridPos = _hexagonFacade.FocusedHexagon.GridPos;
			}

			_hexagonFacade.SelectedHexagon.State.Deactivate ();
		}

		public void Reset () {
			ResetField ();
			ResetSelectionInfos ();
		}

		public void ResetField () {
			_characterFacade.ResetCharacters ();
			_hexagonFacade.ResetField ();
		}

		public void ResetSelectionInfos () {
			_characterFacade.ResetSelectionInfos ();
			_hexagonFacade.ResetSelectionInfos ();
		}
	}
}

