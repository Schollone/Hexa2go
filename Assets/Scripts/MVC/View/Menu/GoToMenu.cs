using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Hexa2Go {

	public class GoToMenu : MonoBehaviour {

		// Use this for initialization
		void Start () {
			Button start = GetComponent<Button> ();	
			start.onClick.AddListener (HandleOnClicked);
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		
		private void HandleOnClicked () {
			Debug.Log ("GoTo Menu");

			Application.LoadLevel (0);

		}
	}

}