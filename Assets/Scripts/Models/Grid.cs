using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid {

	private int _width = 0;
	private int _height = 0;
	private Dictionary<GridPos, Hexagon> _hexagons;
	private Dictionary<GridPos, Vector3> _gridPostionsVector3;
	private Hexagon _selectedField;
	
	private List<GridPos> _gridStartPositions;

	public Grid(int width, int height) {
		this._width = width;
		this._height = height;

		this._hexagons = new Dictionary<GridPos, Hexagon> ();
		this._gridPostionsVector3 = new Dictionary<GridPos, Vector3> ();

		this._gridStartPositions = new List<GridPos>();
		this._gridStartPositions.Add(new GridPos(2, 3));
		this._gridStartPositions.Add(new GridPos(3, 2));
		this._gridStartPositions.Add(new GridPos(3, 3));
		this._gridStartPositions.Add(new GridPos(4, 2));
		this._gridStartPositions.Add(new GridPos(4, 3));
		this._gridStartPositions.Add(new GridPos(4, 4));
		this._gridStartPositions.Add(new GridPos(5, 2));
		this._gridStartPositions.Add(new GridPos(5, 3));
		this._gridStartPositions.Add(new GridPos(5, 4));
		this._gridStartPositions.Add(new GridPos(6, 3));
		this._gridStartPositions.Add(new GridPos(6, 4));
		this._gridStartPositions.Add(new GridPos(7, 3));
		
		initGridPositions ();
	}

	public int width {get { return _width; }}

	public int height {get { return _height; }}

	public Dictionary<GridPos, Hexagon> hexagons {get { return _hexagons; }}
	
	public List<GridPos> gridStartPositions {get { return _gridStartPositions; }}

	public Hexagon getHexagon(GridPos gridPos) {
		Hexagon hexagon;
		this._hexagons.TryGetValue (gridPos, out hexagon);
		return hexagon;
	}

	/*public void updateHexagon(Hexagon hexagon) {
		this.grid [hexagon.gridPos] = hexagon;
		//this.grid.Remove (field.GridPos);
		//this.grid.Add (field.GridPos, field);
	}*/

	public Vector3 getPositionVector3(GridPos gridPos) {
		Vector3 tmp = new Vector3 ();
		_gridPostionsVector3.TryGetValue(gridPos, out tmp);
		return tmp;
	}

	public Hexagon selectedField {
		get {
			return this._selectedField;
		}
		set {
			Hexagon currentField = this._selectedField;
			if (currentField != null) {
				currentField.isSelected = false;
			}

			this._selectedField = value;
			this._selectedField.isSelected = true;
		}
	}

	private void initGridPositions() {
		float yOffset = 4.75f;
		
		for (int y = 0; y < this._height; ++y) {
			for (int x = 0; x < this._width; ++x) {

				float zValue = -9.5f * y;
				if ( (x & 1) == 1 ) {
					zValue -= yOffset;
				}

				this._gridPostionsVector3.Add(new GridPos(x, y), new Vector3 (8.25f * x, 0, zValue) );
			}
		}
	}

}
