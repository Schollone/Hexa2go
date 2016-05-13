using System;

namespace Hexa2Go {

	public struct ContainerObject {

		private GridPos _gridPos;
		private Object _object1;

		public ContainerObject (GridPos gridPos, Object object1) {
			this._gridPos = gridPos;
			this._object1 = object1;
		}

		public GridPos GridPos {
			get {
				return _gridPos;
			}
			set {
				_gridPos = value;
			}
		}

		public Object Object1 {
			get {
				return _object1;
			}
			set {
				_object1 = value;
			}
		}
	}

}