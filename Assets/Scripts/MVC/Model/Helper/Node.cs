using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hexa2Go {

	public enum ColorStatus {
		White,
		Gray,
		Black
	}

	public class Node {

		private GridPos value;
		private IList<Node> neighbors;
		private ColorStatus color;
		private int dist;
		private Node pred;
		
		public Node (GridPos value) {
			this.value = value;
			neighbors = new List<Node> ();
		}

		public GridPos Value {
			get {
				return this.value;
			}
		}

		public IList<Node> Neighbors {
			get {
				return neighbors;
			}
			set {
				neighbors = value;
			}
		}

		public ColorStatus Color {
			get {
				return color;
			}
			set {
				color = value;
			}
		}

		public int Dist {
			get {
				return dist;
			}
			set {
				dist = value;
			}
		}

		public Node Pred {
			get {
				return pred;
			}
			set {
				pred = value;
			}
		}
		
		public void AddNeighbor (Node node) {
			// adds a node to the graph
			neighbors.Add (node);
		}

	}



}