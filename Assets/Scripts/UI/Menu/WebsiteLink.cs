using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class WebsiteLink : MonoBehaviour {

		public void OpenWebsite () {
			Application.OpenURL ("http://www.markus-woltersdorf.de/");
		}
	}

}