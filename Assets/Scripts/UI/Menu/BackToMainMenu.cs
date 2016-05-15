using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hexa2Go {

	public class BackToMainMenu : MonoBehaviour {

		private GameObject _mainMenu;

		void Start() {

			GetComponent<Button>().onClick.AddListener (GoBack);

			Transform canvas = GameObject.Find ("Canvas").transform;
			_mainMenu = canvas.GetChild(1).gameObject;
		}

		void GoBack() {
			gameObject.transform.parent.transform.parent.gameObject.SetActive(false);
			_mainMenu.SetActive(true);
		}
	}
}

