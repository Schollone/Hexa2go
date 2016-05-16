using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hexa2Go {

	public class CharacterHandler {

		private IDictionary<TeamColor, IDictionary<CharacterType, ICharacterController>> _characters;

		public CharacterHandler () {
			_characters = new Dictionary<TeamColor, IDictionary<CharacterType, ICharacterController>> ();

			IDictionary<CharacterType, ICharacterController> characterControllers = new Dictionary<CharacterType, ICharacterController> ();
			characterControllers.Add (CharacterType.CIRCLE, new CharacterController (new GridPos (7, 3), TeamColor.BLUE, CharacterType.CIRCLE));
			characterControllers.Add (CharacterType.SQUARE, new CharacterController (new GridPos (6, 4), TeamColor.BLUE, CharacterType.SQUARE));
			characterControllers.Add (CharacterType.TRIANGLE, new CharacterController (new GridPos (6, 3), TeamColor.BLUE, CharacterType.TRIANGLE));
			_characters.Add (TeamColor.BLUE, characterControllers);

			characterControllers = new Dictionary<CharacterType, ICharacterController> ();
			characterControllers.Add (CharacterType.CIRCLE, new CharacterController (new GridPos (2, 3), TeamColor.RED, CharacterType.CIRCLE));
			characterControllers.Add (CharacterType.SQUARE, new CharacterController (new GridPos (3, 2), TeamColor.RED, CharacterType.SQUARE));
			characterControllers.Add (CharacterType.TRIANGLE, new CharacterController (new GridPos (3, 3), TeamColor.RED, CharacterType.TRIANGLE));

			ICharacterController controller = null;
			characterControllers.TryGetValue (CharacterType.TRIANGLE, out controller);
			if (controller != null) {
				controller.View.Rotate ();
			}
			_characters.Add (TeamColor.RED, characterControllers);
		}

		public ICharacterController GetCharacter (CharacterType type, TeamColor teamColor) {
			ICharacterController result = null;

			IDictionary<CharacterType, ICharacterController> controllers = null;
			if (_characters.TryGetValue (teamColor, out controllers)) {
				controllers.TryGetValue (type, out result);
			}

			return result;
		}

		public IList<ICharacterController> GetCharactersByDices () {
			IList<ICharacterController> result = new List<ICharacterController> ();
			
			for (int i=0; i<2; i++) {
				CharacterType type = UIHandler.Instance.DicesController.DiceControllerArray [i].Model.CharacterType;
				ICharacterController controller = GetCharacter (type, GameManager.Instance.GetGameMode ().CurrentPlayer.Model.TeamColor);
				if (controller.Model.IsInGame) {
					result.Add (controller);
				}
			}

			return result;     
		}

		public IList<ICharacterController> GetCharacters () {
			IList<ICharacterController> result = new List<ICharacterController> ();
			
			foreach (IDictionary<CharacterType, ICharacterController> l in _characters.Values) {
				foreach (ICharacterController controller in l.Values) {
					result.Add (controller);
				}
			}
			
			return result;
		}

		public IList<ICharacterController> GetCharacters (TeamColor teamColor) {
			IList<ICharacterController> result = new List<ICharacterController> ();

			IDictionary<CharacterType, ICharacterController> characterControllers = new Dictionary<CharacterType, ICharacterController> ();
			_characters.TryGetValue(teamColor, out characterControllers);
			result = characterControllers.Values.ToList();
			
			return result;
		}

		public void ResetCharacters () {
			foreach (IDictionary<CharacterType, ICharacterController> l in _characters.Values) {
				foreach (ICharacterController controller in l.Values) {
					controller.Model.State.MarkAsNormal ();
				}
			}
		}

	}

}