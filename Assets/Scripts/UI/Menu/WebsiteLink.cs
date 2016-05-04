using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class WebsiteLink : MonoBehaviour {

		// Use this for initialization
		void Start () {
	
		}
	
		// Update is called once per frame
		void Update () {
	
		}

		public void OpenWebsite () {
			Application.OpenURL ("http://www.markus-woltersdorf.de/");
		}
	}

}