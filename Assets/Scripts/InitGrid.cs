using UnityEngine;
using System.Collections;

public class InitGrid : MonoBehaviour {

	public GameObject prefabHexagon_Normal;
	public GameObject prefabHexagon_Empty;

	private World world;
	
	void Start () {

		world = World.getInstance ();

		initGridLayer ();

		setNormalHexagon (2, 3);
		setNormalHexagon (3, 2);
		setNormalHexagon (3, 3);
		setNormalHexagon (4, 2);
		setNormalHexagon (4, 3);
		setNormalHexagon (4, 4);
		setNormalHexagon (5, 2);
		setNormalHexagon (5, 3);
		setNormalHexagon (5, 4);
		setNormalHexagon (6, 3);
		setNormalHexagon (6, 4);
		setNormalHexagon (7, 3);
	}

	private void initGridLayer() {
		GameObject gridContainer = GameObject.Find ("GridLayer");
		Vector3 gridContainerPosition = gridContainer.transform.position;
		
		for (int y = 0; y < world.Grid.Height; ++y) {
			for (int x = 0; x < world.Grid.Width; ++x) {
				
				GameObject hexagonLayer = Instantiate (prefabHexagon_Empty) as GameObject;
				hexagonLayer.transform.position = world.Grid.getPositionVector3(new GridPos(x, y));
				hexagonLayer.transform.parent = gridContainer.transform;
				
				Hexagon hexagon = world.Grid.getHexagon(new GridPos(x, y));
				hexagon.HexagonLayer = hexagonLayer;
				//world.Grid.setHexagon(new GridPos(x, y), hexagon);
			}
		}
		
		gridContainer.transform.position = new Vector3(gridContainerPosition.x, -0.5f, gridContainerPosition.z);
	}

	private void setNormalHexagon(int x, int y) {
		Transform gridTransform = GameObject.Find ("Grid").transform;

		GameObject hexagonObject = Instantiate (prefabHexagon_Normal) as GameObject;
		hexagonObject.transform.position = world.Grid.getPositionVector3(new GridPos(x, y));
		hexagonObject.transform.parent = gridTransform;

		Hexagon hexagon = world.Grid.getHexagon(new GridPos(x, y));
		hexagon.HexagonObject = hexagonObject;
		//world.Grid.setHexagon(new GridPos(x, y), hexagon);
	}
}
