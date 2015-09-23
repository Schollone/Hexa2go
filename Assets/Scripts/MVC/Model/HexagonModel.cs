using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public class HexagonModel : IHexagonModel {
		
		//public enum TeamColor { NONE, RED, BLUE };
		
		// -- Basic object related
		private GridPos _gridPos;
		private bool _isActivated = false;
		private bool _canReceiveHexagon = false;
		private IList<GridPos> _neighbors;
		private int _neighborIndex = 0;
		private bool _wasVisit = false;
		
		// -- Color related --
		private bool _isSelected = false;
		//private bool _isFocusable = false;
		private bool _isFocused = false;

		// -- Character related --
		private ICharacterModel _character1;
		private ICharacterModel _character2;
		private bool _hasMoveableCharacter = false;
		private bool _hasCharacterWithTeamColor = false;
		private bool _canReceiveCharacter = false;
		private bool _isBlocked = false;
		private bool _isTarget = false;
		private TeamColor _teamColor = TeamColor.NONE;
		
		public HexagonModel (GridPos gridPos) {

			this._gridPos = gridPos;
			this._isActivated = false;
			this._isSelected = false;

			InitNeighbors ();

			/*this._canReceiveHexagon = false;


			isField = this._isField;
			this._isSelected = false;
			this._isFocusable = false;
			this._isFocused = false;
			
			this._character1 = null;
			this._character2 = null;
			this._hasMoveableCharacter = false;
			this._hasCharacterWithTeamColor = false;
			this._canReceiveCharacter = false;
			this._isBlocked = false;
			this._isTarget = false;
			
			this._teamColor = TeamColor.NONE; */
			/*if (this._teamColor == TeamColor.BLUE) {
				this._gameObject.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material.color = HexagonModel.BLUE;
				this._defaultBorderColor = HexagonModel.BLUE;
			}
			if (this._teamColor == TeamColor.RED) {
				this._gameObject.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material.color = HexagonModel.RED;
				this._defaultBorderColor = HexagonModel.RED;
			}*/
		}

		#region IHexagonModel implementation
		public event System.EventHandler<HexagonValueChangedEventArgs> OnSelectionChanged;
		public event System.EventHandler<HexagonValueChangedEventArgs> OnFocusChanged;
		public event System.EventHandler<HexagonValueChangedEventArgs> OnActivationChanged;
		public event System.EventHandler<HexagonValueChangedEventArgs> OnDeclaredTargetChanged;
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

		public bool IsFocusableForCharacter {
			get {
				//if (canReceiveCharacter)
				if (_isActivated) {
					return true;
				}
				return false;
			}
		}

		public bool IsFocusableForHexagon {
			get {
				return false;
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

		public bool WasVisit {
			get {
				return _wasVisit;
			}
			set {
				_wasVisit = value;
			}
		}

		public void Activate (bool ignoreView = false) {
			_isActivated = true;
			if (!ignoreView) {
				HexagonValueChangedEventArgs eventArgs = new HexagonValueChangedEventArgs ();
				OnActivationChanged (this, eventArgs);
			}
		}

		public void Deactivate (bool ignoreView = false) {
			_isActivated = false;
			if (!ignoreView) {
				HexagonValueChangedEventArgs eventArgs = new HexagonValueChangedEventArgs ();
				OnActivationChanged (this, eventArgs);
			}
		}

		public void DeclareTarget (TeamColor teamColor) {
			_isTarget = true;
			_teamColor = teamColor;
			HexagonValueChangedEventArgs eventArgs = new HexagonValueChangedEventArgs ();
			//eventArgs.TeamColor = teamColor;
			OnDeclaredTargetChanged (this, eventArgs);
		}

		public void Select () {
			_isSelected = true;
			HexagonValueChangedEventArgs eventArgs = new HexagonValueChangedEventArgs ();
			//eventArgs.IsSelected = _isSelected;
			OnSelectionChanged (this, eventArgs);
		}

		public void Deselect () {
			_isSelected = false;
			HexagonValueChangedEventArgs eventArgs = new HexagonValueChangedEventArgs ();
			//eventArgs.IsSelected = _isSelected;
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
			
			neighbors.RemoveAll (item => item.x < 0 && item.y < 0);
			neighbors.RemoveAll (item => item.x >= GridHandler.WIDTH && item.y >= GridHandler.HEIGHT);
			_neighbors = neighbors;
		}





		
		public bool canReceiveHexagon {
			get {
				_canReceiveHexagon = false;
				if (!this._isActivated) {
					_canReceiveHexagon = true;
				}
				return _canReceiveHexagon;
			}
			set {
				_canReceiveHexagon = value;
			}
		}
		
		public int neighborIndex {
			get {
				return _neighborIndex;
			}
			set {
				_neighborIndex = value;
			}
		}
		
		// --------------------------------------------------------------------------------------
		
		public bool isFocused {
			get {
				return _isFocused;
			}
			set {
				_isFocused = value;
				/*if (!value) {
					this._gameObject.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = this._defaultAreaColor;
				}
				if (value) {
					changeAreaColor(HexagonModel.GREEN);
				}*/
			}
		}
		
		// --------------------------------------------------------------------------------------

		public ICharacterModel character1 {
			get {
				return _character1;
			}
			set {
				_character1 = value;
			}
		}

		public ICharacterModel character2 {
			get {
				return _character2;
			}
			set {
				_character2 = value;
			}
		}
		
		public bool hasMoveableCharacter {
			get {
				this._hasMoveableCharacter = false;
				if (this._character1 != null || this._character2 != null) {
					this._hasMoveableCharacter = true;
				}
				return this._hasMoveableCharacter;
			}
		}
		
		public bool hasCharacterWithTeamColor (TeamColor teamColor) {
			this._hasCharacterWithTeamColor = false;
			/*if ( (_character1 != null && _character1.teamColor == teamColor) || (_character2 != null && _character2.teamColor == teamColor) ) {
				this._hasCharacterWithTeamColor = true;
			}*/
			return this._hasCharacterWithTeamColor;
		}
		
		public ICharacterModel getCharacterWithTeamColor (TeamColor teamColor) {
			if (hasCharacterWithTeamColor (teamColor)) {
				/*if (_character1.teamColor == teamColor) {
					return _character1;
				} else if (_character2.teamColor == teamColor) {
					return _character2;
				}*/
			}
			return null;
		}
		
		public bool canReceiveCharacter {
			get {
				this._canReceiveCharacter = false;
				if (IsActivated && !isBlocked) {
					this._canReceiveCharacter = true;
				}
				return this._canReceiveCharacter;
			}
		}
		
		public bool isBlocked {
			get {
				this._isBlocked = false;
				if (this._character1 != null && this._character2 != null) {
					this._isBlocked = true;
				}
				return _isBlocked;
			}
		}
		
		public bool isTarget {
			get {
				if (this._isActivated && this._teamColor != TeamColor.NONE) {
					return true;
				}
				return _isTarget;
			}
		}
		
		//
		

		
		public bool hasEmptyCharacter1Slot () {
			if (character1 == null) {
				return true;
			}
			return false;
		}
		
		public bool hasEmptyCharacter2Slot () {
			if (character2 == null) {
				return true;
			}
			return false;
		}
		
		// --------------------------------------------------------------------------------------		

	}

}