using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hexa2Go {

	public class Graph {

		int nodeCount = 0;
		private IList<Node> nodes;

		private Queue queue;

		public Graph () {
			nodes = new List<Node> ();
		}

		public IList<Node> Nodes {
			get {
				return nodes;
			}
		}

		public void AddNode (Node node) {
			nodes.Add (node);
			nodeCount = nodes.Count;
		}

		public Node Get (GridPos target) {
			foreach (Node node in nodes) {
				if (node.Value.Equals (target)) {
					return node;
				}
			}

			return null;
		}

		public Node BFS (Node start, GridPos target) {
			
			queue = new Queue ();
			
			foreach (Node node in Nodes) {
				node.Color = ColorStatus.White;
				node.Dist = int.MaxValue;
				node.Pred = null;
				Debug.LogWarning ("Dequeue: " + node.Value + " Nachbarn: " + node.Neighbors.Count);
			}
			
			start.Color = ColorStatus.Gray;
			start.Dist = 0;
			start.Pred = null;

			Debug.Log ("Queue: " + start.Value);
			queue.Enqueue (start);
			
			while (queue.Count > 0) {
				Node u = (Node)queue.Dequeue ();
				Debug.LogWarning ("Dequeue: " + u.Value + " Nachbarn: " + u.Neighbors.Count);
				foreach (Node n in u.Neighbors) {
					Debug.Log ("Nachbar: " + n.Value);

					if (n.Color == ColorStatus.White) {
						n.Color = ColorStatus.Gray;
						n.Dist = u.Dist + 1;
						n.Pred = u;
						Debug.Log ("Queue: " + n.Value);
						queue.Enqueue (n);
					}
				}
				Debug.Log ("Nachbar ENDE: " + u.Value);
				u.Color = ColorStatus.Black;

				Debug.LogWarning (u.Value + " : " + u.Dist + " - " + target);
				if (u.Value.Equals (target)) {
					return u;
				}
			}

			return null;
			
		}
	}
}

