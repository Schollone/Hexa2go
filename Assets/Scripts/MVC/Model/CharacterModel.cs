using UnityEngine;
using System;
using System.Collections;

namespace Hexa2Go {

	public class CharacterModel : ICharacterModel {

		private GridPos _gridPos;
		private TeamColor _teamColor;
		private bool _isSelected = false;
		private bool _isInGame = true;
		private CharacterPosition _characterPosition;

		public CharacterModel(GridPos gridPos, TeamColor teamColor) {
			_gridPos = gridPos;
			_teamColor = teamColor;
		}

		#region ICharacterModel implementation

		public event EventHandler<CharacterValueChangedEventArgs> OnSelectionChanged;
		public event EventHandler<CharacterValueChangedEventArgs> OnGridPosChanged;
		public event EventHandler<CharacterValueChangedEventArgs> OnTargetReached;
		
		public bool IsSelected {
			get {
				return _isSelected;
			}
			set {
				_isSelected = value;
			}
		}

		public bool IsInGame {
			get {
				return _isInGame;
			}
		}

		public TeamColor TeamColor {
			get {
				return _teamColor;
			}
		}

		public CharacterPosition CharacterPosition {
			get {
				return _characterPosition;
			}
			set {
				_characterPosition = value;
			}
		}

		public GridPos GridPos {
			get {
				return _gridPos;
			}
			set {
				GridPos oldGridPos = _gridPos;
				_gridPos = value;
				CharacterValueChangedEventArgs eventArgs = new CharacterValueChangedEventArgs();
				eventArgs.GridPos = oldGridPos;
				OnGridPosChanged(this, eventArgs);
			}
		}

		public void Select() {
			_isSelected = true;
			CharacterValueChangedEventArgs eventArgs = new CharacterValueChangedEventArgs();
			OnSelectionChanged(this, eventArgs);
		}

		public void Deselect() {
			_isSelected = false;
			CharacterValueChangedEventArgs eventArgs = new CharacterValueChangedEventArgs();
			OnSelectionChanged(this, eventArgs);
		}

		public void Remove() {
			_isInGame = false;
			CharacterValueChangedEventArgs eventArgs = new CharacterValueChangedEventArgs();
			OnTargetReached(this, eventArgs);
		}

		#endregion


	}

}