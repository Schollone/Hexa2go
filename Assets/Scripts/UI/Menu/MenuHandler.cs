using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Hexa2Go {

	public class MenuHandler : MonoBehaviour {

		public Button BtnLanguages;
		public Button BtnBackToMainMenu;
		public Button BtnSound;

		private GameObject _mainMenu;
		private GameObject _languages;

		// Use this for initialization
		void Start () {
			//button = transform.GetComponent<Button> ();
			BtnLanguages.onClick.AddListener (OpenLanguages);
			BtnBackToMainMenu.onClick.AddListener (Back);
			BtnSound.onClick.AddListener (ToggleSound);

			Transform canvas = GameObject.Find ("Canvas").transform;
			_mainMenu = canvas.GetChild(2).gameObject;
			_languages = canvas.GetChild(3).gameObject;
		}

		void Back () {
			_mainMenu.SetActive(true);
			_languages.SetActive(false);
		}

		void OpenLanguages () {
			_mainMenu.SetActive(false);
			_languages.SetActive(true);
		}

		void ToggleSound () {
			Debug.Log("ToggleSound");
			if (SoundManager.Instance.Muted) {
				Debug.Log("Unmute!");
				SoundManager.Instance.UnmuteClips ();
			} else {
				Debug.Log("Mute!");
				SoundManager.Instance.MuteClips ();
			}
			//BtnSound.GetComponent<Image>().sprite = Image;
		}
	}

}