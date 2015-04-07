using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid {

	private int width = 0;
	private int height = 0;
	private Dictionary<GridPos, Hexagon> grid;
	private Dictionary<GridPos, Vector3> gridPostionsVector3;
	private Hexagon selectedHexagon;

	public Grid(int width, int height) {
		this.width = width;
		this.height = height;

		this.grid = new Dictionary<GridPos, Hexagon> ();
		this.gridPostionsVector3 = new Dictionary<GridPos, Vector3> ();

		initGridLayer ();

		selectedHexagon = getHexagon (new GridPos(2,3));
	}

	public int Width {
		get {
			return width;
		}
	}

	public int Height {
		get {
			return height;
		}
	}

	public bool hasHexagon(int x, int y) {
		/*if (grid [x, y] != null) {
			return true;
		}*/
		return false;
	}

	public Hexagon getHexagon(GridPos gridPos) {
		Hexagon result;
		Debug.Log ("GridPos: " + gridPos);
		this.grid.TryGetValue (gridPos, out result);

		Debug.Log ("GridPos Result: " + result.GridPos);
		return result;
	}

	public void updateHexagon(GridPos gridPos, Hexagon hexagon) {
		this.grid.Remove (gridPos);
		this.grid.Add (gridPos, hexagon);
	}

	public Vector3 getPositionVector3(GridPos gridPos) {
		Vector3 tmp = new Vector3 ();
		gridPostionsVector3.TryGetValue(gridPos, out tmp);
		return tmp;
	}

	public Hexagon SelectedHexagon {
		get {
			return selectedHexagon;
		}
		set {
			Hexagon currentHexagon = this.selectedHexagon;
			currentHexagon.IsSelected = false;
			updateHexagon(currentHexagon.GridPos, currentHexagon);

			selectedHexagon = value;
			selectedHexagon.IsSelected = true;
			updateHexagon(selectedHexagon.GridPos, selectedHexagon);

		}
	}

	private void initGridLayer() {
		float yOffset = 2.5f;
		
		for (int y = 0; y < this.height; ++y) {
			for (int x = 0; x < this.width; ++x) {

				float zValue = -5 * y;
				if ( (x & 1) == 1 ) {
					zValue -= yOffset;
				}

				this.gridPostionsVector3.Add(new GridPos(x, y), new Vector3 (4.33f * x, 0, zValue) );

				GridPos gridPos = new GridPos(x, y);
				Hexagon hexagon = new Hexagon(gridPos);
				this.grid.Add(gridPos, hexagon);
			}
		}
	}

}
