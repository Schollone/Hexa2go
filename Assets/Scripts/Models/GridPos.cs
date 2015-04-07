using UnityEngine;
using System.Collections;

public struct GridPos {

	private int x;
	private int y;

	public GridPos(int x, int y) {
		this.x = x;
		this.y = y;

		if (this.x < 0) this.x = 0;
		if (this.y < 0) this.y = 0;

		if (this.x >= World.width) this.x = World.width - 1;
		if (this.y >= World.height) this.y = World.height - 1;
	}

	public int X {
		get {
			return x;
		}
	}

	public int Y {
		get {
			return y;
		}
	}

	public override string ToString () {
		return string.Format ("({0}, {1})", X, Y);
	}
}
