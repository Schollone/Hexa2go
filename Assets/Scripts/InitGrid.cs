using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hexa2Go;

public class InitGrid : MonoBehaviour {

	public GameObject prefabHexagon;
	public GameObject prefabCharacterCircle;
	public GameObject prefabCharacterTriangle;
	public GameObject prefabCharacterSquare;

	private World world;
	
	void Start () {

		//world = World.Instance;

		//initGrid ();

	}

	/*private void initGrid() {
		GameObject gridContainer = GameObject.Find ("Grid");


		for (int x = 0; x < world.grid.width; ++x) {
			for (int y = 0; y < world.grid.height; ++y) {

				GridPos gridPos = new GridPos(x, y);
				
				GameObject gameObject = Instantiate (prefabHexagon) as GameObject;
				gameObject.transform.position = world.grid.getPositionVector3(gridPos);
				gameObject.transform.parent = gridContainer.transform;

				HexagonModel hexagon = null;

				if (world.grid.gridStartPositions.Contains(gridPos)) {
					//hexagon = new HexagonModel(gridPos, gameObject, true);
					hexagon = new HexagonModel(gridPos);
				} else {
					//hexagon = new HexagonModel(gridPos, gameObject);
					hexagon = new HexagonModel(gridPos);
				}

				if (gridPos.Equals(new GridPos(2, 3))) { // target Hexagon RED and Character BLUE
					//hexagon = new HexagonModel(gridPos, gameObject, true, TeamColor.RED);
					hexagon = new HexagonModel(gridPos);

					GameObject gameObjectCharacterCircle = Instantiate (prefabCharacterCircle) as GameObject;
					gameObjectCharacterCircle.transform.position = world.grid.getPositionVector3(gridPos);
					hexagon.character1 = new CharacterModel(gameObjectCharacterCircle, TeamColor.BLUE, CharacterType.CIRCLE);
					hexagon.character1.characterPosition = CharacterPosition.Position_1;

				} else if (gridPos.Equals(new GridPos(3, 2))) { // Character BLUE
					GameObject gameObjectCharacterTriangle = Instantiate (prefabCharacterTriangle) as GameObject;
					gameObjectCharacterTriangle.transform.position = world.grid.getPositionVector3(gridPos);
					hexagon.character1 = new CharacterModel(gameObjectCharacterTriangle, TeamColor.BLUE, CharacterType.TRIANGLE);
					hexagon.character1.characterPosition = CharacterPosition.Position_1;

				} else if (gridPos.Equals(new GridPos(3, 3))) { // Character BLUE
					GameObject gameObjectCharacterSquare = Instantiate (prefabCharacterSquare) as GameObject;
					gameObjectCharacterSquare.transform.position = world.grid.getPositionVector3(gridPos);
					hexagon.character1 = new CharacterModel(gameObjectCharacterSquare, TeamColor.BLUE, CharacterType.SQUARE);
					hexagon.character1.characterPosition = CharacterPosition.Position_1;


				} else if (gridPos.Equals(new GridPos(7, 3))) { // target Hexagon BLUE and Characters BLUE
					//hexagon = new HexagonModel(gridPos, gameObject, true, TeamColor.BLUE);
					hexagon = new HexagonModel(gridPos);

					GameObject gameObjectCharacterCircle = Instantiate (prefabCharacterCircle) as GameObject;
					gameObjectCharacterCircle.transform.position = world.grid.getPositionVector3(gridPos);
					hexagon.character1 = new CharacterModel(gameObjectCharacterCircle, TeamColor.RED, CharacterType.CIRCLE);
					hexagon.character1.characterPosition = CharacterPosition.Position_1;

				} else if (gridPos.Equals(new GridPos(6, 3))) { // Characters BLUE
					GameObject gameObjectCharacterTriangle = Instantiate (prefabCharacterTriangle) as GameObject;
					gameObjectCharacterTriangle.transform.position = world.grid.getPositionVector3(gridPos);
					hexagon.character1 = new CharacterModel(gameObjectCharacterTriangle, TeamColor.RED, CharacterType.TRIANGLE);
					hexagon.character1.characterPosition = CharacterPosition.Position_1;

				} else if (gridPos.Equals(new GridPos(6, 4))) { // Characters BLUE
					GameObject gameObjectCharacterSquare = Instantiate (prefabCharacterSquare) as GameObject;
					gameObjectCharacterSquare.transform.position = world.grid.getPositionVector3(gridPos);
					hexagon.character1 = new CharacterModel(gameObjectCharacterSquare, TeamColor.RED, CharacterType.SQUARE);
					hexagon.character1.characterPosition = CharacterPosition.Position_1;
				}

				world.grid.hexagons[gridPos] = hexagon;

				if (hexagon.IsField) {
					world.grid.getUsedHexagons.Add(hexagon);
				}
			}
		}

		//initNeighbors ();
	}*/

	/*private void initNeighbors() {

		for (int y = 0; y < world.grid.height; ++y) {
			for (int x = 0; x < world.grid.width; ++x) {

				HexagonModel hexagon = world.grid.getHexagon (new GridPos (x, y));

				List<HexagonModel> neighbors = (List<HexagonModel>) hexagon.neighbors;

				if ((hexagon.GridPos.x & 1) == 0) {
					neighbors.Add(world.grid.getHexagon(new GridPos(x, y-1)));
					neighbors.Add(world.grid.getHexagon(new GridPos(x+1, y-1)));
					neighbors.Add(world.grid.getHexagon(new GridPos(x+1, y)));
					neighbors.Add(world.grid.getHexagon(new GridPos(x, y+1)));
					neighbors.Add(world.grid.getHexagon(new GridPos(x-1, y)));
					neighbors.Add(world.grid.getHexagon(new GridPos(x-1, y-1)));
				} else {
					neighbors.Add(world.grid.getHexagon(new GridPos(x, y-1)));
					neighbors.Add(world.grid.getHexagon(new GridPos(x+1, y)));
					neighbors.Add(world.grid.getHexagon(new GridPos(x+1, y+1)));
					neighbors.Add(world.grid.getHexagon(new GridPos(x, y+1)));
					neighbors.Add(world.grid.getHexagon(new GridPos(x-1, y+1)));
					neighbors.Add(world.grid.getHexagon(new GridPos(x-1, y)));
				}

				neighbors.RemoveAll (item => item == null);

			}
		}

	}*/

}
