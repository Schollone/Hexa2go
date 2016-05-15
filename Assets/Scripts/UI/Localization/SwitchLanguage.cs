using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Hexa2Go {

	public class SwitchLanguage : MonoBehaviour {

		public Languages language;

		private Image _FlagBg;
		private GameObject _FlagBtn;
		private GameObject _FlagSelection;

		// Use this for initialization
		void Start () {
			_FlagBg = transform.GetChild(0).gameObject.GetComponent<Image>();
			_FlagBtn = transform.GetChild(1).gameObject;
			_FlagSelection = transform.GetChild(2).gameObject;

			_FlagBtn.GetComponent<Button> ().onClick.AddListener (OnClick);
		}
	
		// Update is called once per frame
		void Update () {
	
		}

		void OnClick () {
			_FlagBg.color = HexagonColors.DARK_GREEN;
			_FlagSelection.SetActive(true);

			LocalizationManager.Instance.LoadLanguage (language.ToString ().ToLower ());
		}

		public void ResetFlag() {
			_FlagBg.color = HexagonColors.DARK_GRAY;
			_FlagSelection.SetActive(false);
		}
	}
}