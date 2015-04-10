using UnityEngine;
using System.Collections;

public class InitGrid : MonoBehaviour {

	public GameObject prefabHexagon;

	private World world;
	
	void Start () {

		world = World.getInstance ();

		initGrid ();
	}

	private void initGrid() {
		GameObject gridContainer = GameObject.Find ("Grid");


		for (int y = 0; y < world.grid.height; ++y) {
			for (int x = 0; x < world.grid.width; ++x) {
				
				GameObject gameObject = Instantiate (prefabHexagon) as GameObject;
				gameObject.transform.position = world.grid.getPositionVector3(new GridPos(x, y));
				gameObject.transform.parent = gridContainer.transform;

				GridPos gridPos = new GridPos(x, y);

				Hexagon hexagon = null;

				if (world.grid.gridStartPositions.Contains(gridPos)) {
					hexagon = new Hexagon(gridPos, gameObject, true);
				} else {
					hexagon = new Hexagon(gridPos, gameObject);
				}

				if (gridPos.Equals(new GridPos(2, 3))) {
					hexagon = new Hexagon(gridPos, gameObject, true, Hexagon.TeamColor.RED);
				} else if (gridPos.Equals(new GridPos(7, 3))) {
					hexagon = new Hexagon(gridPos, gameObject, true, Hexagon.TeamColor.BLUE);
				}

				world.grid.hexagons[gridPos] = hexagon;
			}
		}

		world.grid.selectedField = world.grid.getHexagon (new GridPos(2,3));

	}

}
