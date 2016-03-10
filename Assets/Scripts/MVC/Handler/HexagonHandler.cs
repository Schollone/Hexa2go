using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Hexa2Go {

	public class HexagonHandler {

		private IDictionary<GridPos ,IHexagonController> _hexagons;
		private int _width, _height;
		private ArrayList _neighborHexagons = null;
		private IHexagonController _focusedHexagon = null;
		private int _focusedHexagonIndex = 0;
		private ArrayList _selectableHexagons = null;
		private int _selectedHexagonIndex = 0;

		private Queue queue;

		public HexagonHandler (int width, int height) {
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

			_selectableHexagons = new ArrayList ();
			_neighborHexagons = new ArrayList ();

			Setup ();
		}

		private void Setup () {
			IHexagonController hexagon = null;

			hexagon = Get (new GridPos (2, 3));
			//hexagon.Model.Activate (false, TeamColor.BLUE);
			hexagon.Model.State = new ActivatedHomeHexagon(hexagon.Model, TeamColor.BLUE);

			hexagon = Get (new GridPos (3, 2));
			hexagon.Model.Activate ();
			hexagon = Get (new GridPos (3, 3));
			hexagon.Model.Activate ();
			hexagon = Get (new GridPos (4, 2));
			hexagon.Model.Activate ();
			hexagon = Get (new GridPos (4, 3));
			hexagon.Model.State = new ActivatedNormalHexagon(hexagon.Model);
			hexagon.Model.Activate ();
			hexagon = Get (new GridPos (4, 4));
			hexagon.Model.Activate ();
			hexagon = Get (new GridPos (5, 2));
			hexagon.Model.Activate ();
			hexagon = Get (new GridPos (5, 3));
			hexagon.Model.Activate ();
			hexagon = Get (new GridPos (5, 4));
			hexagon.Model.Activate ();
			hexagon = Get (new GridPos (6, 3));
			hexagon.Model.Activate ();
			hexagon = Get (new GridPos (6, 4));
			hexagon.Model.Activate ();

			hexagon = Get (new GridPos (7, 3));
			hexagon.Model.Activate (false, TeamColor.RED);

		}

		public IHexagonController Get (GridPos gridPos) {
			IHexagonController result = null;
			_hexagons.TryGetValue (gridPos, out result);
			return result;
		}

		public IHexagonController GetTarget (TeamColor teamColor) {
			IHexagonController[] l = _hexagons.Values.ToArray ();
			foreach (IHexagonController hexagon in l) {
				if (hexagon.Model.TeamColor == teamColor) {
					return hexagon;
				}
			}

			return null;
		}

		public void Select (GridPos gridPos) {
			IHexagonController hexagonController = Get (gridPos);

			hexagonController.Model.Select ();
		}

		public void Deselect (GridPos gridPos) {
			IHexagonController hexagonController = Get (gridPos);

			hexagonController.Model.Deselect ();
		}

		public void InitNeighbors (GridPos gridPos, bool onlyFocusable = false, bool useForPasch = false) {
			_neighborHexagons.Clear ();
			IHexagonController hexagonController = Get (gridPos);

			List<GridPos> gridPosNeighbors = (List<GridPos>)hexagonController.Model.Neighbors;
			foreach (GridPos pos in gridPosNeighbors) {
				IHexagonController neighbor = Get (pos);

				if (onlyFocusable) {
					if (neighbor == null) {
						break;
					}

					if (useForPasch) {
						if (!neighbor.Model.IsActivated && IsFocusableForHexagon (hexagonController, neighbor)) {
							_neighborHexagons.Add (neighbor);
						}
					} else {
						if (neighbor.Model.IsFocusableForCharacter)
							_neighborHexagons.Add (neighbor);
					}
				} else {
					_neighborHexagons.Add (neighbor);
				}

			}
		}

		public bool IsFocusableForHexagon (IHexagonController selectedHexagon, IHexagonController focusableHexagon) {
			TeamColor originalTeamColor = selectedHexagon.Model.TeamColor;
			selectedHexagon.Model.Deactivate (true);
			bool resetSelectedHexagon = false;
			if (!focusableHexagon.Model.IsActivated) {
				resetSelectedHexagon = true;
			}
			focusableHexagon.Model.Visited = true;
			focusableHexagon.Model.Activate (true);

			int fields = 1 + look (focusableHexagon);

			foreach (IHexagonController hexagon in _hexagons.Values) {
				hexagon.Model.Visited = false;
			}

			selectedHexagon.Model.Activate (true, originalTeamColor);
			if (resetSelectedHexagon) {
				focusableHexagon.Model.Deactivate (true);
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
				
				if (neighborHexagon != null && neighborHexagon.Model.IsActivated && !neighborHexagon.Model.Visited) {
					neighborHexagon.Model.Visited = true;
					result += 1 + look (neighborHexagon);
				}
			}
			return result;
		}

		public void TintFocusableNeighbors (bool useForPasch = false) {
			foreach (IHexagonController neighbor in _neighborHexagons) {
				neighbor.View.Focusable ();
			}
		}

		public void ResetFocusableNeighbors () {
			foreach (IHexagonController neighbor in _neighborHexagons) {
				neighbor.View.ResetTint ();
			}
		}

		public bool HasFocusableNeighbors () {
			return (_neighborHexagons.Count > 0);
		}

		private void ResetLastHexagons () {
			if (_focusedHexagon != null) {
				_focusedHexagon.View.ResetTint ();
				_focusedHexagon.View.Focusable ();
				_focusedHexagon = null;
			}
		}

		private void FocusHexagon () {
			if (_neighborHexagons.Count > 0) {
				_focusedHexagon = (IHexagonController)_neighborHexagons [_focusedHexagonIndex];
				_focusedHexagon.View.Focus ();
			}
		}

		public void FocusNextHexagon (GridPos gridPos, bool useForPasch = false) {
			InitNeighbors (gridPos, true, useForPasch);
			ResetLastHexagons ();
			_focusedHexagonIndex++;
			if (_focusedHexagonIndex >= _neighborHexagons.Count) {
				_focusedHexagonIndex = 0;
			}
			FocusHexagon ();
		}

		public void FocusPrevHexagon (GridPos gridPos, bool useForPasch = false) {
			InitNeighbors (gridPos, true, useForPasch);
			ResetLastHexagons ();
			_focusedHexagonIndex--;
			if (_focusedHexagonIndex < 0) {
				_focusedHexagonIndex = _neighborHexagons.Count - 1;
			}
			FocusHexagon ();
		}

		public void ResetFocusedHexagon () {
			_focusedHexagon = null;
		}

		public IHexagonController FocusedHexagon {
			get {
				return _focusedHexagon;
			}
		}

		public void InitSelectableHexagons () {
			_selectableHexagons.Clear ();
			_selectedHexagonIndex = 0;

			foreach (IHexagonController hexagon in _hexagons.Values) {
				if (hexagon.Model.IsActivated) {

					foreach (GridPos neighborGridPos in hexagon.Model.Neighbors) {
						IHexagonController neighbor = Get (neighborGridPos);
						if (neighbor == null) {
							break;
						}

						if (!neighbor.Model.IsActivated && IsFocusableForHexagon (hexagon, neighbor)) {
							_selectableHexagons.Add (hexagon);
							break;
						}
					}

				}
			}
		}

		public ArrayList GetSelectableHexagons () {
			return _selectableHexagons;
		}

		public IHexagonController SelectNextHexagon () {
			IHexagonController hexagon = null;
			if (_selectableHexagons.Count > 0) {
				hexagon = (IHexagonController)_selectableHexagons [_selectedHexagonIndex];
				hexagon.Model.Deselect ();
				_selectedHexagonIndex++;
				if (_selectedHexagonIndex >= _selectableHexagons.Count) {
					_selectedHexagonIndex = 0;
				}
				hexagon = (IHexagonController)_selectableHexagons [_selectedHexagonIndex];
				hexagon.Model.Select ();
			}
			return hexagon;
		}

		public IHexagonController SelectPrevHexagon () {
			IHexagonController hexagon = null;
			if (_selectableHexagons.Count > 0) {
				hexagon = (IHexagonController)_selectableHexagons [_selectedHexagonIndex];
				hexagon.Model.Deselect ();
				_selectedHexagonIndex--;
				if (_selectedHexagonIndex < 0) {
					_selectedHexagonIndex = _selectableHexagons.Count - 1;
				}
				hexagon = (IHexagonController)_selectableHexagons [_selectedHexagonIndex];
				hexagon.Model.Select ();
			}
			return hexagon;
		}

		public void resetHexagonVisit () {
			foreach (IHexagonController hexagon in _hexagons.Values) {
				hexagon.Model.Visited = false;
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
				GameManager.Instance.GridHandler.SelectedHexagon = Get (start.Model.GridPos);
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
				GameManager.Instance.GridHandler.SelectedHexagon = Get (start.Model.GridPos);
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

			root.Model.Visited = true;
			root.Pred = null;
			root.Distance = 0;
			queue.Enqueue (root);
			
			while (queue.Count > 0) {
				IHexagonController hexagon = (IHexagonController)queue.Dequeue ();

				IList<GridPos> neighbors = hexagon.Model.Neighbors;

				if (counterclockwise) {
					neighbors = neighbors.Reverse ().ToList ();
				}
				
				foreach (GridPos neighborPos in neighbors) {
					IHexagonController neighbor = Get (neighborPos);
					if (neighbor.Model.IsActivated && !neighbor.Model.Visited) {
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
					}
				}
			}

			return target;
		}

		public void SetHexagonToPosition (GridPos newPosition) {
			_focusedHexagon = Get (newPosition);
		}
	}

}