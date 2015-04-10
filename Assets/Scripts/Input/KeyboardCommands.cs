using UnityEngine;
using System.Collections;

public class KeyboardCommands : MonoBehaviour {

	private World world;

	// Use this for initialization
	void Start () {
		world = World.getInstance ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			Debug.LogWarning("Left");
			Hexagon hexagon = world.grid.selectedField;
			GridPos newGridPos = new GridPos (hexagon.gridPos.x - 1, hexagon.gridPos.y);
			Hexagon newHexagon = world.grid.getHexagon(newGridPos);
			world.grid.selectedField = newHexagon;
		} else if (Input.GetKeyUp (KeyCode.RightArrow)) {
			Debug.LogWarning("Right");
			Hexagon hexagon = world.grid.selectedField;
			GridPos newGridPos = new GridPos (hexagon.gridPos.x + 1, hexagon.gridPos.y);
			Hexagon newHexagon = world.grid.getHexagon(newGridPos);
			world.grid.selectedField = newHexagon;
		} else if (Input.GetKeyUp (KeyCode.UpArrow)) {
			Debug.LogWarning("Up");
			Hexagon hexagon = world.grid.selectedField;
			GridPos newGridPos = new GridPos (hexagon.gridPos.x, hexagon.gridPos.y - 1);
			Hexagon newHexagon = world.grid.getHexagon(newGridPos);
			world.grid.selectedField = newHexagon;
		} else if (Input.GetKeyUp (KeyCode.DownArrow)) {
			Debug.LogWarning("Down");
			Hexagon hexagon = world.grid.selectedField;
			GridPos newGridPos = new GridPos (hexagon.gridPos.x, hexagon.gridPos.y + 1);
			Hexagon newHexagon = world.grid.getHexagon(newGridPos);
			world.grid.selectedField = newHexagon;
		}
	}
}
