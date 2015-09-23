﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public class HexagonHandler {

		private IDictionary<GridPos ,IHexagonController> _hexagons;
		private int _width, _height;
		private ArrayList _neighborHexagons = null;
		private IHexagonController _focusedHexagon = null;
		private int _focusedHexagonIndex = 0;
		private ArrayList _selectableHexagons = null;
		private int _selectedHexagonIndex = 0;

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
			hexagon.Model.Activate ();
			hexagon.Model.DeclareTarget (TeamColor.BLUE);

			hexagon = Get (new GridPos (3, 2));
			hexagon.Model.Activate ();
			hexagon = Get (new GridPos (3, 3));
			hexagon.Model.Activate ();
			hexagon = Get (new GridPos (4, 2));
			hexagon.Model.Activate ();
			hexagon = Get (new GridPos (4, 3));
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
			hexagon.Model.Activate ();
			hexagon.Model.DeclareTarget (TeamColor.RED);

		}

		public IHexagonController Get (GridPos gridPos) {
			IHexagonController result = null;
			_hexagons.TryGetValue (gridPos, out result);
			return result;
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

		private bool IsFocusableForHexagon (IHexagonController selectedHexagon, IHexagonController focusableHexagon) {
			selectedHexagon.Model.Deactivate (true);
			bool resetSelectedHexagon = false;
			if (!focusableHexagon.Model.IsActivated) {
				resetSelectedHexagon = true;
			}
			focusableHexagon.Model.WasVisit = true;
			focusableHexagon.Model.Activate (true);

			int fields = 1 + look (focusableHexagon);

			foreach (IHexagonController hexagon in _hexagons.Values) {
				hexagon.Model.WasVisit = false;
			}

			selectedHexagon.Model.Activate (true);
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
				IHexagonController neighborHexagon = GameManager.Instance.GridHandler.HexagonHandler.Get (neighborGridPos);
				
				if (neighborHexagon != null && neighborHexagon.Model.IsActivated && !neighborHexagon.Model.WasVisit) {
					neighborHexagon.Model.WasVisit = true;
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

			foreach (IHexagonController hexagon in _hexagons.Values) {
				if (hexagon.Model.IsActivated) {

					foreach (GridPos neighborGridPos in hexagon.Model.Neighbors) {
						IHexagonController neighbor = Get (neighborGridPos);
						if (!neighbor.Model.IsActivated) {
							_selectableHexagons.Add (hexagon);
							break;
						}
					}

				}
			}
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
	}

}