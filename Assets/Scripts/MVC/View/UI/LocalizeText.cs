using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Hexa2Go {

	public class LocalizeText : MonoBehaviour {

		public TextIdentifier textIdentifier;

		// Use this for initialization
		void Start () {
			GetComponent<Text> ().text = LocalizationManager.GetText (textIdentifier.ToString ());
		}
	
		// Update is called once per frame
		void Update () {
	
		}
	}

}