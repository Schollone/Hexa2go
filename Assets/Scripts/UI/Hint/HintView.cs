using UnityEngine;
using UnityEngine.UI;
using System;

namespace Hexa2Go {

	public class HintView : MonoBehaviour {

		private Text hint;

		void Awake () {
			hint = gameObject.GetComponent<Text> ();
		}

		
		public void UpdateHint (string text) {
			if (hint != null) {
				hint.text = text;
			}
		}
	}
}

