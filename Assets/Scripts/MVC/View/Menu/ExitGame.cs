using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Hexa2Go {

	public class ExitGame : MonoBehaviour {

		private Button button;

		// Use this for initialization
		void Start () {
			button = transform.GetComponent<Button> ();
			button.onClick.AddListener (Exit);
		}

		void Exit () {
			Debug.Log ("Exit");
			Application.Quit ();
		}
	}

}