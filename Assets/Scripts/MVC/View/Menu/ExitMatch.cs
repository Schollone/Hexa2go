using UnityEngine;
using UnityEngine.UI;
using System;

namespace Hexa2Go {
	
	public class ExitMatch : MonoBehaviour {
		
		void Start () {
			GetComponent<Button> ().onClick.AddListener (OnExitMatch);
		}
		
		private void OnExitMatch () {
			Application.LoadLevel (0);
		}
	}
}