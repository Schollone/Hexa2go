using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Hexa2Go {

	public class LocalizeText : MonoBehaviour {

		public TextIdentifier textIdentifier;

		// Use this for initialization
		void Start () {
			GetComponent<Text> ().text = LocalizationManager.GetText (textIdentifier.ToString ());

			LocalizationManager.Instance.OnLanguageChanged += HandleOnLanguageChanged;
		}

		void HandleOnLanguageChanged () {
			GetComponent<Text> ().text = LocalizationManager.GetText (textIdentifier.ToString ());
		}

		void OnDestroy () {
			//Debug.Log ("Delete: " + GetComponent<Text> ().text);
			LocalizationManager.Instance.OnLanguageChanged -= HandleOnLanguageChanged;
		}
	}

}