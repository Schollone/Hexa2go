using UnityEngine;
using UnityEngine.UI;
using System;

namespace Hexa2Go {

	public class ResumeMatch : MonoBehaviour {

		void Start () {
			GetComponent<Button> ().onClick.AddListener (OnContinue);
		}

		private void OnContinue () {
			GameObject.Find ("PauseScreen").transform.GetChild (0).gameObject.SetActive (false);
		}
	}
}

