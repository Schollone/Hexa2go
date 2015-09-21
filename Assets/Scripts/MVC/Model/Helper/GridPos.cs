using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public struct GridPos {

		private int _x;
		private int _y;
		
		public GridPos(int x, int y) {
			this._x = x;
			this._y = y;
			
			//if (this._x < 0) this._x = 0;
			//if (this._y < 0) this._y = 0;
			
			//if (this._x >= World.width) this._x = World.width - 1;
			//if (this._y >= World.height) this._y = World.height - 1;
		}
		
		public int x {
			get {
				return _x;
			}
		}
		
		public int y {
			get {
				return _y;
			}
		}
		
		public override string ToString () {
			return string.Format ("({0}, {1})", _x, _y);
		}

		public override bool Equals (object obj) {
			GridPos gridPos = (GridPos) obj;
			return (this.x == gridPos.x && this.y == gridPos.y);
		}

	}

}