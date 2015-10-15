using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class CameraView : MonoBehaviour {

		public int State = 0;

		// Use this for initialization
		void Start () {
			Zoom (3);
		}
	
		// Update is called once per frame
		void Update () {
	
		}

		public void Zoom (int state) {
			Debug.LogWarning ("Zoom: " + State + " == " + state);
			if (State == state) {
				return;
			}
			GetComponent<Animator> ().SetTrigger ("Zoom_State_" + state);
			State = state;
		}
	}

}