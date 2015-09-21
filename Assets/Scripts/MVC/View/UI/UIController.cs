using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public class UIController : MonoBehaviour {

		[SerializeField] private Text playername1;
		[SerializeField] private Text playername2;
		[SerializeField] private Text progress1;
		[SerializeField] private Text progress2;

		// Use this for initialization
		void Start () {
			if (GameManager.Instance.GameMode == GameMode.Multiplayer) {
				playername1.text = "Spieler 1";
				playername2.text = "Spieler 2";
			} else {
				playername1.text = "Spieler";
				playername2.text = "Computer";
			}

			progress1.text = "0 / 3";
			progress2.text = "0 / 3";
		}
		
		// Update is called once per frame
		void Update () {
			progress1.text = getProgress (GameManager.Instance.GridHandler.CharacterHandler_P1.Characters.Values) + " / 3";
			progress2.text = getProgress (GameManager.Instance.GridHandler.CharacterHandler_P2.Characters.Values) + " / 3";
		}

		int getProgress (ICollection<ICharacterController> collection) {
			int progress = 3;
			foreach (ICharacterController character in collection) {
				if (character.Model.IsInGame) {
					progress--;
				}
			}
			return progress;
		}
	}

}