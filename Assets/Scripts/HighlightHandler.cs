using UnityEngine;
using System.Collections;

public class HighlightHandler : MonoBehaviour {

	private World world;

	// Use this for initialization
	void Start () {
		world = World.getInstance ();

		//world.Grid [1, 1].IsSelected = true;
	}
	
	// Update is called once per frame
	void Update () {
	


	}

	private void updateColor() {
		//Hexagon[,] grid = world.Grid;
		
		/*foreach (Hexagon hexagon in grid) {
			//Debug.Log (hexagon.GridPos);
			if (hexagon.IsSelected) {
				Vector3 tmpPos = hexagon.HexagonObject.transform.position;
				hexagon.HexagonObject.transform.position = new Vector3(tmpPos.x, tmpPos.y + 0.01f, tmpPos.z);
				hexagon.HexagonObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
			}
		}*/
	}
}
