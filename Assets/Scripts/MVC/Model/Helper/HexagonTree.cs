using System;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {
	public class HexagonTree<T> {

		private T data;
		private LinkedList<HexagonTree<T>> children;

		public HexagonTree (T data) {
			this.data = data;
			children = new LinkedList<HexagonTree<T>> ();
		}

		public void AddChild (T data) {
			children.AddFirst (new HexagonTree<T> (data));
		}

		public HexagonTree<T> GetChild (int i) {
			foreach (HexagonTree<T> n in children) {
				if (--i == 0) {
					return n;
				}
			}
			return null;
		}
	}
}

