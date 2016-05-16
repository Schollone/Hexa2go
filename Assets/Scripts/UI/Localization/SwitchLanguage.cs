using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Hexa2Go {

	public class SwitchLanguage : MonoBehaviour {

		public Languages language;

		private GameObject _FlagBtn;
		private GameObject _FlagSelection;

		// Use this for initialization
		void Start () {
			_FlagBtn = transform.GetChild(0).gameObject;
			_FlagSelection = transform.GetChild(1).gameObject;

			_FlagBtn.GetComponent<Button> ().onClick.AddListener (OnClick);
		}
	
		// Update is called once per frame
		void Update () {
	
		}

		void OnClick () {
			_FlagSelection.SetActive(true);

			LocalizationManager.Instance.LoadLanguage (language.ToString ().ToLower ());
		}

		public void ResetFlag() {
			_FlagSelection.SetActive(false);
		}
	}
}