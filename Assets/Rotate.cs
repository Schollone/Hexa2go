using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float speed = 10f;

	// Use this for initialization
	void Start () {
		Debug.Log("Init Loading Screen");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform != null) {
			transform.Rotate(0f, 0f, -1f * speed);
		}
	}
}
