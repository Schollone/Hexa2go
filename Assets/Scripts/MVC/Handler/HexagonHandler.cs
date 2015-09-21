using UnityEngine;
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

		public HexagonHandler(int width, int height) {
			_width = width;
			_height = height;

			_hexagons = new Dictionary<GridPos, IHexagonController>();

			for (int x = 0; x < _width; ++x) {
				for (int y = 0; y < _height; ++y) {
					GridPos gridPos = new GridPos(x, y);

					IHexagonController hexagon = new HexagonController(gridPos);
					_hexagons.Add(gridPos, hexagon);
				}

			}

			_selectableHexagons = new ArrayList();
			_neighborHexagons = new ArrayList();

			Setup();

		}

		private void Setup() {
			IHexagonController hexagon = null;

			_hexagons.TryGetValue(new GridPos(2,3), out hexagon);
			hexagon.Model.Activate();
			hexagon.Model.DeclareTarget(TeamColor.BLUE);

			_hexagons.TryGetValue(new GridPos(3,2), out hexagon);
			hexagon.Model.Activate();
			_hexagons.TryGetValue(new GridPos(3,3), out hexagon);
			hexagon.Model.Activate();
			_hexagons.TryGetValue(new GridPos(4,2), out hexagon);
			hexagon.Model.Activate();
			_hexagons.TryGetValue(new GridPos(4,3), out hexagon);
			hexagon.Model.Activate();
			_hexagons.TryGetValue(new GridPos(4,4), out hexagon);
			hexagon.Model.Activate();
			_hexagons.TryGetValue(new GridPos(5,2), out hexagon);
			hexagon.Model.Activate();
			_hexagons.TryGetValue(new GridPos(5,3), out hexagon);
			hexagon.Model.Activate();
			_hexagons.TryGetValue(new GridPos(5,4), out hexagon);
			hexagon.Model.Activate();
			_hexagons.TryGetValue(new GridPos(6,3), out hexagon);
			hexagon.Model.Activate();
			_hexagons.TryGetValue(new GridPos(6,4), out hexagon);
			hexagon.Model.Activate();

			_hexagons.TryGetValue(new GridPos(7,3), out hexagon);
			hexagon.Model.Activate();
			hexagon.Model.DeclareTarget(TeamColor.RED);

		}

		public IHexagonController Get(GridPos gridPos) {
			IHexagonController result = null;
			_hexagons.TryGetValue(gridPos, out result);
			return result;
		}

		public void Select(GridPos gridPos) {
			IHexagonController hexagonController = null;
			_hexagons.TryGetValue(gridPos, out hexagonController);
			
			hexagonController.Model.Select();
		}

		public void Deselect(GridPos gridPos) {
			IHexagonController hexagonController = null;
			_hexagons.TryGetValue(gridPos, out hexagonController);

			hexagonController.Model.Deselect();
		}

		private void InitNeighbors(GridPos gridPos, bool onlyFocusable = false, bool useForPasch = false) {
			_neighborHexagons.Clear();
			IHexagonController hexagonController = null;
			_hexagons.TryGetValue(gridPos, out hexagonController);
			
			List<GridPos> gridPosNeighbors = (List<GridPos>) hexagonController.Model.Neighbors;
			foreach (GridPos pos in gridPosNeighbors) {
				IHexagonController neighbor = null;
				_hexagons.TryGetValue(pos, out neighbor);

				if (onlyFocusable) {
					if (useForPasch) {
						if (neighbor.Model.IsFocusableForHexagon) _neighborHexagons.Add(neighbor);
					} else {
						if (neighbor.Model.IsFocusableForCharacter) _neighborHexagons.Add(neighbor);
					}
				} else {
					_neighborHexagons.Add(neighbor);
				}

			}
		}

		public void TintFocusableNeighbors(GridPos gridPos) {
			InitNeighbors(gridPos, true);
			foreach(IHexagonController neighbor in _neighborHexagons) {
				if (neighbor.Model.IsFocusableForCharacter) neighbor.View.Focusable();
			}
		}

		public void ResetFocusableNeighbors(GridPos gridPos) {
			InitNeighbors(gridPos);
			foreach(IHexagonController neighbor in _neighborHexagons) {
				neighbor.View.ResetTint();
			}
		}

		private void ResetLastHexagons() {
			if (_focusedHexagon != null) {
				_focusedHexagon.View.ResetTint();
				_focusedHexagon.View.Focusable();
				_focusedHexagon = null;
			}
		}

		private void FocusHexagon() {
			_focusedHexagon = (IHexagonController) _neighborHexagons[_focusedHexagonIndex];
			_focusedHexagon.View.Focus();
		}

		public void FocusNextHexagon(GridPos gridPos, bool useForPasch) {
			InitNeighbors(gridPos, true, useForPasch);
			ResetLastHexagons();
			_focusedHexagonIndex++;
			if (_focusedHexagonIndex >= _neighborHexagons.Count) {
				_focusedHexagonIndex = 0;
			}
			FocusHexagon ();
		}

		public void FocusPrevHexagon(GridPos gridPos) {
			InitNeighbors(gridPos, true);
			ResetLastHexagons();
			_focusedHexagonIndex--;
			if (_focusedHexagonIndex < 0) {
				_focusedHexagonIndex = _neighborHexagons.Count-1;
			}
			FocusHexagon ();
		}

		public void ResetFocusedHexagon() {
			_focusedHexagon = null;
		}

		public IHexagonController FocusedHexagon {
			get {
				return _focusedHexagon;
			}
		}

		public void InitSelectableHexagons() {
			_selectableHexagons.Clear();

			foreach(IHexagonController hexagon in _hexagons.Values) {
				Debug.Log(hexagon.Model.IsActivated);
				if (hexagon.Model.IsActivated) {
					_selectableHexagons.Add(hexagon);
				}
			}
		}

		public IHexagonController SelectNextHexagon() {
			IHexagonController hexagon = (IHexagonController) _selectableHexagons[_selectedHexagonIndex];
			hexagon.Model.Deselect();
			GameManager.Instance.GridHandler.HexagonHandler.Deselect(hexagon.Model.GridPos);
			_selectedHexagonIndex++;
			if (_selectedHexagonIndex >= _selectableHexagons.Count) {
				_selectedHexagonIndex = 0;
			}
			hexagon = (IHexagonController) _selectableHexagons[_selectedHexagonIndex];
			GameManager.Instance.GridHandler.HexagonHandler.Select(hexagon.Model.GridPos);
			hexagon.Model.Select();
			return hexagon;
		}

		public IHexagonController SelectPrevHexagon() {
			IHexagonController hexagon = (IHexagonController) _selectableHexagons[_selectedHexagonIndex];
			hexagon.Model.Deselect();
			GameManager.Instance.GridHandler.HexagonHandler.Deselect(hexagon.Model.GridPos);
			_selectedHexagonIndex--;
			if (_selectedHexagonIndex < 0) {
				_selectedHexagonIndex = _selectableHexagons.Count-1;
			}
			hexagon = (IHexagonController) _selectableHexagons[_selectedHexagonIndex];
			GameManager.Instance.GridHandler.HexagonHandler.Select(hexagon.Model.GridPos);
			hexagon.Model.Select();
			return hexagon;
		}
	}

}