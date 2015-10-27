using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public class HexagonModel : IHexagonModel {

		private GridPos _gridPos;
		private bool _isActivated = false;
		private bool _isSelected = false;
		private bool _isTarget = false;
		private bool _visited = false;
		private TeamColor _teamColor = TeamColor.NONE;
		private IList<GridPos> _neighbors;
		
		public HexagonModel (GridPos gridPos) {

			this._gridPos = gridPos;
			this._isActivated = false;
			this._isSelected = false;

			InitNeighbors ();
		}

		#region IHexagonModel implementation
		public event System.EventHandler<HexagonValueChangedEventArgs> OnSelectionChanged;
		public event System.EventHandler<HexagonValueChangedEventArgs> OnFocusChanged;
		public event System.EventHandler<HexagonValueChangedEventArgs> OnActivationChanged;
		#endregion
		
		public GridPos GridPos {
			get {
				return _gridPos;
			}
		}

		public bool IsActivated {
			get {
				return _isActivated;
			}
		}

		public bool IsSelected {
			get {
				return _isSelected;
			}
		}

		public bool IsTarget {
			get {
				return _isTarget;
			}
		}

		public bool Visited {
			get {
				return _visited;
			}
			set {
				_visited = value;
			}
		}

		public bool IsFocusableForCharacter {
			get {
				if (_isActivated && !IsBlocked) {
					return true;
				}
				return false;
			}
		}

		public bool IsBlocked {
			get {
				bool result = false;
				int count = 0;

				List<ICharacterController> characters1 = (List<ICharacterController>)GameManager.Instance.GridHandler.CharacterHandler_P1.GetCharacters (_gridPos);
				List<ICharacterController> characters2 = (List<ICharacterController>)GameManager.Instance.GridHandler.CharacterHandler_P2.GetCharacters (_gridPos);
				characters1.AddRange (characters2);
				foreach (ICharacterController character in characters1) {
					if (character.Model.IsInGame) {
						count++;
					}
				}
				if (count >= 2) {
					result = true;
				}

				return result;
			}
		}
		
		public TeamColor TeamColor {
			get {
				return _teamColor;
			}
		}

		public IList<GridPos> Neighbors {
			get {
				return _neighbors;
			}
		}

		public void Activate (bool ignoreView = false, TeamColor teamColor = TeamColor.NONE) {
			_isActivated = true;
			_isTarget = (teamColor == TeamColor.NONE) ? false : true;

			if (!ignoreView) {
				_teamColor = teamColor;
				HexagonValueChangedEventArgs eventArgs = new HexagonValueChangedEventArgs ();
				OnActivationChanged (this, eventArgs);
			}
		}

		public void Deactivate (bool ignoreView = false) {
			_isActivated = false;
			_isTarget = false;

			if (!ignoreView) {
				_teamColor = TeamColor.NONE;
				HexagonValueChangedEventArgs eventArgs = new HexagonValueChangedEventArgs ();
				OnActivationChanged (this, eventArgs);
			}
		}

		public void Select () {
			_isSelected = true;
			HexagonValueChangedEventArgs eventArgs = new HexagonValueChangedEventArgs ();
			OnSelectionChanged (this, eventArgs);
		}

		public void Deselect () {
			_isSelected = false;
			HexagonValueChangedEventArgs eventArgs = new HexagonValueChangedEventArgs ();
			OnSelectionChanged (this, eventArgs);
		}

		private void InitNeighbors () {
			this._neighbors = new List<GridPos> ();

			int x = _gridPos.x;
			int y = _gridPos.y;
			
			List<GridPos> neighbors = (List<GridPos>)_neighbors;
			
			if ((x & 1) == 0) {
				neighbors.Add (new GridPos (x, y - 1));
				neighbors.Add (new GridPos (x + 1, y - 1));
				neighbors.Add (new GridPos (x + 1, y));
				neighbors.Add (new GridPos (x, y + 1));
				neighbors.Add (new GridPos (x - 1, y));
				neighbors.Add (new GridPos (x - 1, y - 1));
			} else {
				neighbors.Add (new GridPos (x, y - 1));
				neighbors.Add (new GridPos (x + 1, y));
				neighbors.Add (new GridPos (x + 1, y + 1));
				neighbors.Add (new GridPos (x, y + 1));
				neighbors.Add (new GridPos (x - 1, y + 1));
				neighbors.Add (new GridPos (x - 1, y));
			}

			neighbors.RemoveAll (item => item.x < 0);
			neighbors.RemoveAll (item => item.y < 0);
			neighbors.RemoveAll (item => item.x >= GridHandler.WIDTH);
			neighbors.RemoveAll (item => item.y >= GridHandler.HEIGHT);

			_neighbors = neighbors;
		}

	}

}