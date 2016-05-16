using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Hexa2Go {

	public class MenuHandler : MonoBehaviour {

		public Button BtnPlay;
		public Button BtnSettings;

		private GameObject _mainMenu;
		private GameObject _play;
		private GameObject _settings;

		// Use this for initialization
		void Start () {
			BtnPlay.onClick.AddListener (OpenPlay);
			BtnSettings.onClick.AddListener (OpenSettings);

			Transform canvas = GameObject.Find ("Canvas").transform;
			_mainMenu = canvas.GetChild(1).gameObject;
			_play = canvas.GetChild(2).gameObject;
			_settings = canvas.GetChild(3).gameObject;
		}

		void OpenPlay () {
			_mainMenu.SetActive(false);
			_play.SetActive(true);
		}

		void OpenSettings () {
			_mainMenu.SetActive(false);
			_settings.SetActive(true);
		}
	}

}