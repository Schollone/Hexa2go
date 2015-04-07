using UnityEngine;
using System.Collections;

public class Hexagon {

	private GameObject hexagonLayer;
	private GameObject hexagonObject;
	private GridPos gridPos;
	private Character character1;
	private Character character2;
	private bool isBlocked = false;
	private bool isTargetHexagon = false;
	private bool isSelected = false;
	private bool isHighlighed = false;

	public Hexagon(GridPos gridPos) {
		Debug.Log ("Hexagon created");
		this.gridPos = gridPos;
	}

	public GameObject HexagonLayer {
		get {
			return hexagonLayer;
		}
		set {
			hexagonLayer = value;
		}
	}

	public GameObject HexagonObject {
		get {
			return hexagonObject;
		}
		set {
			hexagonObject = value;
		}
	}

	public GridPos GridPos {
		get {
			return gridPos;
		}
	}

	public Character Character1 {
		get {
			return character1;
		}
		set {
			character1 = value;
		}
	}

	public Character Character2 {
		get {
			return character2;
		}
		set {
			character2 = value;
		}
	}

	public bool IsBlocked {
		get {
			return isBlocked;
		}
		set {
			isBlocked = value;
		}
	}

	public bool IsTargetHexagon {
		get {
			return isTargetHexagon;
		}
		set {
			isTargetHexagon = value;
		}
	}

	public bool IsSelected {
		get {
			return isSelected;
		}
		set {
			Debug.Log("isSelected: " + value + " : " + this.gridPos.ToString());
			isSelected = value;
			if (value == false) {
				this.hexagonLayer.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f);
			}
			if (value == true) {
				this.hexagonLayer.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.yellow;
			}
		}
	}

	public bool IsHighlighed {
		get {
			return isHighlighed;
		}
		set {
			isHighlighed = value;
		}
	}


}

