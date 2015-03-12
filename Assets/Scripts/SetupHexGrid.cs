using UnityEngine;
using System.Collections;

public class SetupHexGrid : MonoBehaviour {

	private const int height = 7;
	private const int width = 10;

	public GameObject prefabHexagon_Normal;
	public GameObject prefabHexagon_Empty;

	private World world;
	
	// Use this for initialization
	void Start () {

		world = World.getInstance ();

		initGrid (width, height);

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

	private void initGrid(int width, int height) {
		Transform gridTransform = GameObject.Find ("Grid").transform;

		
		float yOffset = 2.415f;
		
		for (int y = 0; y < height; ++y) {
			for (int x = 0; x < width; ++x) {

				GameObject hexagonObject = Instantiate (prefabHexagon_Empty) as GameObject;
				Hexagon hexagon = new Hexagon ();

				float zValue = -4.83f * y;
				if ( (x & 1) == 1 ) {
					zValue =- yOffset;
				}

				hexagonObject.transform.position = new Vector3 (4.33f * x, 0, zValue);
				hexagonObject.transform.parent = gridTransform;
				hexagon.HexagonObject = hexagonObject;
				
				world.Grid[x, y] = hexagon;
			}
		}
	}

	private void setNormalHexagon(int x, int y) {
		Transform gridTransform = GameObject.Find ("Grid").transform;
		Hexagon hexagon = new Hexagon ();

		Hexagon oldHexagon = world.Grid [x, y];
		GameObject oldHexagonObject = oldHexagon.HexagonObject;

		GameObject hexagonObject = Instantiate (prefabHexagon_Normal) as GameObject;
		hexagon.HexagonObject = hexagonObject;
		hexagon.GridPos = oldHexagonObject.transform.position;
		Destroy (oldHexagon);

		hexagonObject.transform.parent = gridTransform;
		world.Grid [x, y] = hexagon;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
