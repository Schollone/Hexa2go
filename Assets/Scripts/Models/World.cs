using UnityEngine;
using System.Collections;

public class World {
	
	private static World world;
	
	public const int height = 7;
	public const int width = 10;

	private Grid _grid;

	private World() {
		this._grid = new Grid (width, height);
	}
	
	public static World getInstance() {
		if (world == null) {
			world = new World();
		}
		return world;
	}
	
	public Grid grid {
		get {
			return _grid;
		}
	}

}
