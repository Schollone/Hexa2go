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
			Hexagon currentHexagon = world.Grid.SelectedHexagon;
			GridPos newGridPos = new GridPos (currentHexagon.GridPos.X - 1, currentHexagon.GridPos.Y);
			Hexagon newHexagon = world.Grid.getHexagon (newGridPos); 
			world.Grid.SelectedHexagon = newHexagon;
		} else if (Input.GetKeyUp (KeyCode.RightArrow)) {
			Debug.LogWarning("Right");
			Hexagon currentHexagon = world.Grid.SelectedHexagon;
			GridPos newGridPos = new GridPos (currentHexagon.GridPos.X + 1, currentHexagon.GridPos.Y);
			Hexagon newHexagon = world.Grid.getHexagon (newGridPos);
			world.Grid.SelectedHexagon = newHexagon;
		} else if (Input.GetKeyUp (KeyCode.UpArrow)) {
			Debug.LogWarning("Up");
			Hexagon currentHexagon = world.Grid.SelectedHexagon;
			GridPos newGridPos = new GridPos (currentHexagon.GridPos.X, currentHexagon.GridPos.Y - 1);
			Hexagon newHexagon = world.Grid.getHexagon (newGridPos); 
			world.Grid.SelectedHexagon = newHexagon;
		} else if (Input.GetKeyUp (KeyCode.DownArrow)) {
			Debug.LogWarning("Down");
			Hexagon currentHexagon = world.Grid.SelectedHexagon;
			GridPos newGridPos = new GridPos (currentHexagon.GridPos.X, currentHexagon.GridPos.Y + 1);
			Hexagon newHexagon = world.Grid.getHexagon (newGridPos);
			world.Grid.SelectedHexagon = newHexagon;
		}
	}
}
