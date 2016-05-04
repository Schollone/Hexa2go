using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hexa2Go {

	public class HexagonHandler {

		private IDictionary<GridPos ,IHexagonController> _hexagons;
		private int _width, _height;
		private Queue queue;

		public HexagonHandler (int width, int height) {
			Debug.LogWarning ("HexagonHandler");
			_width = width;
			_height = height;
			_hexagons = new Dictionary<GridPos, IHexagonController> ();

			for (int x = 0; x < _width; ++x) {
				for (int y = 0; y < _height; ++y) {
					GridPos gridPos = new GridPos (x, y);

					IHexagonController hexagon = new HexagonController (gridPos);
					_hexagons.Add (gridPos, hexagon);
				}
			}

			Setup ();
		}

		private void Setup () {
			GridPos[] gridPosArray = new GridPos[12];
			gridPosArray [0] = new GridPos (2, 3);
			gridPosArray [1] = new GridPos (3, 2);
			gridPosArray [2] = new GridPos (3, 3);
			gridPosArray [3] = new GridPos (4, 2);
			gridPosArray [4] = new GridPos (4, 3);
			gridPosArray [5] = new GridPos (4, 4);
			gridPosArray [6] = new GridPos (5, 2);
			gridPosArray [7] = new GridPos (5, 3);
			gridPosArray [8] = new GridPos (5, 4);
			gridPosArray [9] = new GridPos (6, 3);
			gridPosArray [10] = new GridPos (6, 4);
			gridPosArray [11] = new GridPos (7, 3);

			IList gridPosList = new ArrayList ();
			for (int x = 0; x < _width; ++x) {
				for (int y = 0; y < _height; ++y) {
					GridPos gridPos = new GridPos (x, y);
					gridPosList.Add (gridPos);
				}
			}
			foreach (GridPos gridPos in gridPosArray) {
				gridPosList.Remove (gridPos);
			}

			IHexagonController hexagon = null;

			hexagon = Get (new GridPos (2, 3));
			hexagon.Model.State = new ActivatedHomeHexagon (hexagon.Model, TeamColor.BLUE);
			hexagon = Get (new GridPos (7, 3));
			hexagon.Model.State = new ActivatedHomeHexagon (hexagon.Model, TeamColor.RED);

			foreach (GridPos gridPos in gridPosList) {
				hexagon = Get (gridPos);
				hexagon.Model.State = new DeactivatedNormalHexagon (hexagon.Model);
			}
		}

		public void InitFocusableNeighbors (GridPos gridPos) {
			IHexagonController hexagonController = Get (gridPos);

			List<GridPos> gridPosNeighbors = (List<GridPos>)hexagonController.Model.Neighbors;
			foreach (GridPos pos in gridPosNeighbors) {
				IHexagonController neighbor = Get (pos);

				if (neighbor == null) {
					break;
				}

				if (neighbor.Model.State.IsActivated && !neighbor.Model.IsBlocked) { // isFocusable
					neighbor.Model.State.MarkAsFocusable ();
				}
			}
		}

		public void InitFocusableNeighborsOfHexagon (GridPos gridPos) {
			IHexagonController selectedHexagon = Get (gridPos);
			foreach (GridPos neighborPos in selectedHexagon.Model.Neighbors) {
				IHexagonController neighbor = Get (neighborPos);
				
				if (!neighbor.Model.State.IsActivated && IsFocusableForHexagon (selectedHexagon, neighbor)) {
					neighbor.Model.State.MarkAsFocusable ();
				}
			}
		}

		public void ResetField () {
			foreach (IHexagonController controller in _hexagons.Values) {
				if (controller.Model.IsBlocked) {
					controller.Model.State.MarkAsBlocked ();
				} else {
					controller.Model.State.MarkAsNormal ();
				}
			}
		}

		public void ResetDeactivatedField () {
			foreach (IHexagonController controller in _hexagons.Values) {
				if (!controller.Model.State.IsActivated) {
					controller.Model.State.MarkAsNormal ();
				}
			}
		}

		public IHexagonController Get (GridPos gridPos) {
			IHexagonController result = null;
			_hexagons.TryGetValue (gridPos, out result);
			return result;
		}

		public IHexagonController GetTarget (TeamColor teamColor) {
			IHexagonController[] l = _hexagons.Values.ToArray ();
			foreach (IHexagonController hexagon in l) {
				if (hexagon.Model.State.TeamColor == teamColor) {
					return hexagon;
				}
			}

			return null;
		}

		public bool IsFocusableForHexagon (IHexagonController selectedHexagon, IHexagonController focusableHexagon) {
			bool resetAsHome = false;
			TeamColor teamColor = selectedHexagon.Model.State.TeamColor;
			if (selectedHexagon.Model.State.IsHome) {
				resetAsHome = true;
			}

			selectedHexagon.Model.State.Deactivate (true);
			bool resetSelectedHexagon = false;
			if (!focusableHexagon.Model.State.IsActivated) {
				resetSelectedHexagon = true;
			}
			focusableHexagon.Visited = true;
			focusableHexagon.Model.State.Activate (TeamColor.NONE, true);

			int fields = 1 + look (focusableHexagon);

			foreach (IHexagonController hexagon in _hexagons.Values) {
				hexagon.Visited = false;
			}

			selectedHexagon.Model.State.Activate (TeamColor.NONE, true);
			if (resetAsHome) {
				selectedHexagon.Model.State.MarkAsHome(teamColor);
			}

			if (resetSelectedHexagon) {
				focusableHexagon.Model.State.Deactivate (true);
			}
			
			if (fields == 12) {
				return true;
			}
			return false;
		}

		private int look (IHexagonController hexagon) {
			int result = 0;
			
			foreach (GridPos neighborGridPos in hexagon.Model.Neighbors) {
				IHexagonController neighborHexagon = Get (neighborGridPos);
				
				if (neighborHexagon != null && neighborHexagon.Model.State.IsActivated && !neighborHexagon.Visited) {
					neighborHexagon.Visited = true;
					result += 1 + look (neighborHexagon);
				}
			}
			return result;
		}

		public void InitSelectableHexagons () {
			foreach (IHexagonController hexagon in _hexagons.Values) {
				if (hexagon.Model.State.IsActivated) {

					foreach (GridPos neighborGridPos in hexagon.Model.Neighbors) {
						IHexagonController neighbor = Get (neighborGridPos);
						if (neighbor == null) {
							break;
						}

						if (!neighbor.Model.State.IsActivated && IsFocusableForHexagon (hexagon, neighbor)) {
							hexagon.Model.State.MarkAsSelectable ();
							break;
						}
					}

				}
			}
		}

		public void resetHexagonVisit () {
			foreach (IHexagonController hexagon in _hexagons.Values) {
				hexagon.Visited = false;
				hexagon.Pred = new GridPos ();
				hexagon.Distance = int.MaxValue;
			}
		}

		public Nullable<GridPos> GetNextHexagonToFocus (GridPos start, GridPos targetPos, bool counterclockwiseNeighborSearch = false) {
			Nullable<GridPos> result = null;
			IHexagonController root = Get (start);
			BFS (root, targetPos, counterclockwiseNeighborSearch);
			GridPos predHexagon = targetPos;
			while (!predHexagon.Equals(start)) {
				IHexagonController hexagon = Get (predHexagon);
				predHexagon = (GridPos)hexagon.Pred;
				result = hexagon.Model.GridPos;
			}
			resetHexagonVisit ();
			//Debug.Log (result);
			return result;
		}

		public Nullable<GridPos> StrategyFromCharacterToTarget (ICharacterController startCharacter, bool checkForShortDistance) {
			IHexagonController start = Get (startCharacter.Model.GridPos);
			IHexagonController target = GetTarget (startCharacter.Model.TeamColor);
			if (target == null) {
				return null;
			}
			//Debug.LogWarning ("Start Pos: " + start.Model.GridPos);
			//Debug.LogWarning ("Ziel Pos: " + target.Model.GridPos);
			int distanceFromOldPosition = GetDistance (start, target.Model.GridPos);

			Nullable<GridPos> result = CheckDistanceFromNeighbors (target, start, distanceFromOldPosition, checkForShortDistance);
			if (result != null) {
				//GameManager.Instance.GridHandler.SelectedHexagon = Get (start.Model.GridPos);
			}
			return result;
		}

		public Nullable<GridPos> StrategyFromTargetToCharacter (ICharacterController targetCharacter, bool checkForShortDistance) {
			IHexagonController target = Get (targetCharacter.Model.GridPos);
			IHexagonController start = GetTarget (targetCharacter.Model.TeamColor);

			if (start == null) {
				return null;
			}
			//Debug.LogWarning ("Start Pos: " + start.Model.GridPos);
			//Debug.LogWarning ("Ziel Pos: " + target.Model.GridPos);

			int distanceFromOldPosition = GetDistance (start, target.Model.GridPos);
			if (distanceFromOldPosition == 1) {
				return null;
			}

			Nullable<GridPos> result = CheckDistanceFromNeighbors (target, start, distanceFromOldPosition, checkForShortDistance);
			if (result != null) {
				//GameManager.Instance.GridHandler.SelectedHexagon = Get (start.Model.GridPos);
			}
			return result;
		}

		private Nullable<GridPos> CheckDistanceFromNeighbors (IHexagonController target, IHexagonController start, int distanceFromOldPosition, bool checkForShortDistance) {
			foreach (GridPos neighborPos in start.Model.Neighbors) {
				IHexagonController neighbor = Get (neighborPos);
				if (IsFocusableForHexagon (start, neighbor)) {
					int distanceFromNewPosition = GetDistance (neighbor, target.Model.GridPos);
					bool condition = checkForShortDistance ? (distanceFromNewPosition < distanceFromOldPosition) : (distanceFromNewPosition > distanceFromOldPosition);
					//Debug.LogWarning (distanceFromNewPosition + " < " + distanceFromOldPosition + " --> " + neighborPos);
					if (condition) {
						return neighborPos;
					}
				}
			}
			return null;
		}

		public ICharacterController[] SortCharacterByDistance (ICharacterController[] characters) {
			IList<ICharacterController> l = new List<ICharacterController> ();

			for (int i = 0; i < characters.Length; i++) {
				ICharacterController character = characters [i];
				if (!character.Model.IsInGame) {
					continue;
				}
				IHexagonController start = Get (character.Model.GridPos);
				IHexagonController target = GetTarget (character.Model.TeamColor);
				character.Distance = GetDistance (start, target.Model.GridPos);
				l.Add (character);
			}
			return l.OrderBy (x => x.Distance).ToArray ();
		}

		private int GetDistance (IHexagonController root, GridPos targetPos) {
			IHexagonController target = BFS (root, targetPos);
			int result = target.Distance;
			resetHexagonVisit ();
			Debug.Log (result);
			return result;
		}

		private IHexagonController BFS (IHexagonController root, GridPos targetPos, bool counterclockwise = false) {
			IHexagonController target = null;

			queue = new Queue ();
			resetHexagonVisit ();

			//root.Model.Visited = true;
			root.Pred = null;
			root.Distance = 0;
			queue.Enqueue (root);
			
			while (queue.Count > 0) {
				IHexagonController hexagon = (IHexagonController)queue.Dequeue ();

				IList<GridPos> neighbors = hexagon.Model.Neighbors;

				if (counterclockwise) {
					neighbors = neighbors.Reverse ().ToList ();
				}
				
				//foreach (GridPos neighborPos in neighbors) {
					//IHexagonController neighbor = Get (neighborPos);
					/*if (neighbor.Model.IsActivated && !neighbor.Model.Visited) {
						neighbor.Model.Visited = true;
						neighbor.Pred = hexagon.Model.GridPos;
						neighbor.Distance = hexagon.Distance + 1;
						
						//Debug.Log ("Current: " + hexagon.Model.GridPos + " ; Nachbar: " + neighbor.Model.GridPos + " ; Distance: " + neighbor.Distance);
						if (neighborPos.Equals (targetPos)) {
							target = neighbor;
							queue.Clear ();
							break;
						}
						
						queue.Enqueue (neighbor);
					}*/
				//}
			}

			return target;
		}
	}

}