using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public class StatsView : MonoBehaviour {

		[SerializeField]
		private Text
			playername;
		[SerializeField]
		private Text
			progress;
		
		// Use this for initialization
		void Start () {
			//playername.text = GameManager.Instance.GetGameMode ().GetPlayers () [0].Model.Name; // UIHandler.PlayerController_One.Model.Name;
			//playername2.text = GameManager.Instance.GetGameMode ().GetPlayers () [1].Model.Name;
			
			//progress.text = "0 / 3";
			//progress2.text = "0 / 3";
		}
		
		// Update is called once per frame
		/*void Update () {
			int savedCharacters1 = GameManager.Instance.GetGameMode().GetPlayers()[0].Model.SavedCharacters;
			int savedCharacters2 = GameManager.Instance.GetGameMode().GetPlayers()[1].Model.SavedCharacters;
			//ICollection<ICharacterController> collectionPlayerTwo = GameManager.Instance.GridHandler.CharacterHandler_P2.Characters.Values;
			progress1.text = savedCharacters1 + " / " + "3";
			progress2.text = savedCharacters2 + " / " + "3";
		}*/

		public void UpdateStats (string name, int savedCharacters) {
			playername.text = name;
			progress.text = savedCharacters + " / 3";
		}
	}

}