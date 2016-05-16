using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class Rotate : MonoBehaviour {

		public float speed = 10f;
	
		// Update is called once per frame
		void Update () {
			if (transform != null) {
				transform.Rotate (0f, 0f, -1f * Time.deltaTime * speed);
			}
		}
	}

}