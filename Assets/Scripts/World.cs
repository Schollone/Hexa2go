using UnityEngine;
using System.Collections;

public class World : Object {

	private static World world;

	private const int height = 7;
	private const int width = 10;
	private Hexagon[,] grid;

	private World() {
		grid = new Hexagon[width, height];
	}

	public static World getInstance() {
		if (world == null) {
			world = new World();
		}
		return world;
	}

	public Hexagon[,] Grid {
		get {
			return grid;
		}
	}
}
