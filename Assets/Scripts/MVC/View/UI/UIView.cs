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
			playername1.text = GameManager.Instance.GameModeHandler.PlayerHandler.PlayerController_One.Model.Name;
			playername2.text = GameManager.Instance.GameModeHandler.PlayerHandler.PlayerController_Two.Model.Name;

			progress1.text = "0 / 3";
			progress2.text = "0 / 3";
		}
		
		// Update is called once per frame
		void Update () {
			ICollection<ICharacterController> collectionPlayerOne = GameManager.Instance.GridHandler.CharacterHandler_P1.Characters.Values;
			ICollection<ICharacterController> collectionPlayerTwo = GameManager.Instance.GridHandler.CharacterHandler_P2.Characters.Values;
			progress1.text = getProgress (collectionPlayerOne) + " / " + collectionPlayerOne.Count;
			progress2.text = getProgress (collectionPlayerTwo) + " / " + collectionPlayerTwo.Count;
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