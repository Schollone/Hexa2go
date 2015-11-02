using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Hexa2Go {

	public class SwitchLanguage : MonoBehaviour {

		public Languages language;

		// Use this for initialization
		void Start () {
			GetComponent<Button> ().onClick.AddListener (OnClick);
		}
	
		// Update is called once per frame
		void Update () {
	
		}

		void OnClick () {
			LocalizationManager.Instance.LoadLanguage (language.ToString ().ToLower ());
		}
	}
}