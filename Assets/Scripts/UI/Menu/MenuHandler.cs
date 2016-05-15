using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Hexa2Go {

	public class MenuHandler : MonoBehaviour {

		public Button BtnPlay;
		public Button BtnSettings;
		//public Button BtnBackToMainMenu;
		public GameObject SoundToggle;
		public Sprite SoundOn;
		public Sprite SoundOff;

		private GameObject _mainMenu;
		private GameObject _play;
		private GameObject _settings;
		private GameObject _languages;

		private Button BtnSound;
		private Image ImageSound;

		// Use this for initialization
		void Start () {
			BtnSound = SoundToggle.GetComponent<Button>();
			ImageSound = SoundToggle.GetComponent<Image>();

			//button = transform.GetComponent<Button> ();
			BtnPlay.onClick.AddListener (OpenPlay);
			BtnSettings.onClick.AddListener (OpenSettings);
			//BtnBackToMainMenu.onClick.AddListener (Back);
			BtnSound.onClick.AddListener (ToggleSound);

			Transform canvas = GameObject.Find ("Canvas").transform;
			_mainMenu = canvas.GetChild(1).gameObject;
			_play = canvas.GetChild(2).gameObject;
			_settings = canvas.GetChild(3).gameObject;
			//_languages = canvas.GetChild(3).gameObject;

			//_settings.transform.GetChild(1).GetComponent<SwitchLanguage>().

		}

		/*void Back () {
			_mainMenu.SetActive(true);

			//_play.SetActive(false);
			//_settings.SetActive(false);
			//_languages.SetActive(false);
		}*/

		void OpenLanguages () {
			_mainMenu.SetActive(false);
			_languages.SetActive(true);
		}

		void OpenPlay () {
			_mainMenu.SetActive(false);
			_play.SetActive(true);
		}

		void OpenSettings () {
			_mainMenu.SetActive(false);
			_settings.SetActive(true);
		}

		void ToggleSound () {
			Debug.Log("ToggleSound");
			if (SoundManager.Instance.Muted) {
				Debug.Log("Unmute!");
				SoundManager.Instance.UnmuteClips ();
				ImageSound.sprite = SoundOn;
			} else {
				Debug.Log("Mute!");
				SoundManager.Instance.MuteClips ();
				ImageSound.sprite = SoundOff;
			}
			//BtnSound.GetComponent<Image>().sprite = Image;
		}
	}

}