using UnityEngine;
using System.Collections;

public class Hexagon : Object {

	private GameObject hexagonObject;
	private Vector3 gridPos;
	private Character character1;
	private Character character2;
	private bool isBlocked = false;
	private bool isTargetHexagon = false;
	private bool isSelected = false;
	private bool isHighlighed = false;

	public Hexagon() {
		Debug.Log ("Hexagon created");
	}

	public GameObject HexagonObject {
		get {
			return hexagonObject;
		}
		set {
			hexagonObject = value;
		}
	}

	public Vector3 GridPos {
		get {
			return gridPos;
		}
		set {
			gridPos = value;
			hexagonObject.transform.position = gridPos;
		}
	}
}

