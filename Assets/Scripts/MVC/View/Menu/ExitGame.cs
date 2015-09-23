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
	
		// Update is called once per frame
		void Update () {
	
		}

		void Exit () {
			Debug.Log ("Exit");
			Application.Quit ();
		}
	}

}