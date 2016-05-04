using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public class UIView : MonoBehaviour {

		[SerializeField]
		private Text
			playername1;
		[SerializeField]
		private Text
			playername2;
		[SerializeField]
		private Text
			progress1;
		[SerializeField]
		private Text
			progress2;

		// Use this for initialization
		void Start () {
			playername1.text = GameManager.Instance.GetGameMode ().GetPlayers () [0].Model.Name; // UIHandler.PlayerController_One.Model.Name;
			playername2.text = GameManager.Instance.GetGameMode ().GetPlayers () [1].Model.Name;

			progress1.text = "0 / 3";
			progress2.text = "0 / 3";
		}
		
		// Update is called once per frame
		void Update () {
			int savedCharacters1 = GameManager.Instance.GetGameMode().GetPlayers()[0].Model.SavedCharacters;
			int savedCharacters2 = GameManager.Instance.GetGameMode().GetPlayers()[1].Model.SavedCharacters;
			//ICollection<ICharacterController> collectionPlayerTwo = GameManager.Instance.GridHandler.CharacterHandler_P2.Characters.Values;
			progress1.text = savedCharacters1 + " / " + "3";
			progress2.text = savedCharacters2 + " / " + "3";
		}

		int getProgress (ICollection<ICharacterController> collection) {
			int progress = collection.Count;
			foreach (ICharacterController character in collection) {
				if (character.Model.IsInGame) {
					progress--;
				}
			}

			return progress;
		}
	}

}