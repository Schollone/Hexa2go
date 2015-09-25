using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hexa2Go {

	public class CharacterModel : ICharacterModel {

		private GridPos _gridPos;
		private TeamColor _teamColor;
		private bool _isSelected = false;
		private bool _isInGame = true;
		private CharacterPosition _characterPosition;
		private GridHelper.OffsetPosition _offsetPosition;

		private static List<int> _numbers;
		private static int _offsetPositionIndex = 0;

		public CharacterModel (GridPos gridPos, TeamColor teamColor) {
			_gridPos = gridPos;

			System.Random rnd = new System.Random (Guid.NewGuid ().GetHashCode ());
			_offsetPositionIndex = rnd.Next (0, 4);
			_offsetPosition = (GridHelper.OffsetPosition)_offsetPositionIndex;

			_teamColor = teamColor;
		}

		#region ICharacterModel implementation

		public event EventHandler<CharacterValueChangedEventArgs> OnSelectionChanged;
		public event EventHandler<CharacterValueChangedEventArgs> OnGridPosChanged;
		public event EventHandler<CharacterValueChangedEventArgs> OnTargetReached;

		public GridPos GridPos {
			get {
				return _gridPos;
			}
			set {
				GridPos oldGridPos = _gridPos;
				_gridPos = value;
				
				List<ICharacterController> characters1 = (List<ICharacterController>)GameManager.Instance.GridHandler.CharacterHandler_P1.GetCharacters (_gridPos);
				List<ICharacterController> characters2 = (List<ICharacterController>)GameManager.Instance.GridHandler.CharacterHandler_P2.GetCharacters (_gridPos);
				characters1.AddRange (characters2);
				characters1.RemoveAll (characterController => characterController.Model.Equals (this));

				_offsetPosition = GetOffsetPosition (characters1);
				
				CharacterValueChangedEventArgs eventArgs = new CharacterValueChangedEventArgs ();
				eventArgs.GridPos = oldGridPos;
				OnGridPosChanged (this, eventArgs);
			}
		}

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

		public GridHelper.OffsetPosition OffsetPosition {
			get {
				return _offsetPosition;
			}
			set {
				_offsetPosition = value;
			}
		}

		public void Select () {
			_isSelected = true;
			CharacterValueChangedEventArgs eventArgs = new CharacterValueChangedEventArgs ();
			OnSelectionChanged (this, eventArgs);
		}

		public void Deselect () {
			_isSelected = false;
			CharacterValueChangedEventArgs eventArgs = new CharacterValueChangedEventArgs ();
			OnSelectionChanged (this, eventArgs);
		}

		public void Remove () {
			_isInGame = false;
			CharacterValueChangedEventArgs eventArgs = new CharacterValueChangedEventArgs ();
			OnTargetReached (this, eventArgs);
		}

		#endregion

		private GridHelper.OffsetPosition GetOffsetPosition (IList<ICharacterController> characters) {
			GridHelper.OffsetPosition offsetPosition;
			if (characters.Count > 0) {
				offsetPosition = characters [0].Model.OffsetPosition;
				switch (offsetPosition) {
					case GridHelper.OffsetPosition.TopLeft:
						{
							offsetPosition = GridHelper.OffsetPosition.BottomRight;
							break;
						}
					case GridHelper.OffsetPosition.TopRight:
						{
							offsetPosition = GridHelper.OffsetPosition.BottomLeft;
							break;
						}
					case GridHelper.OffsetPosition.BottomLeft:
						{
							offsetPosition = GridHelper.OffsetPosition.TopRight;
							break;
						}
					case GridHelper.OffsetPosition.BottomRight:
						{
							offsetPosition = GridHelper.OffsetPosition.TopLeft;
							break;
						}
				}
			} else {
				int offsetPositionIndex = new System.Random ().Next (0, 4);
				offsetPosition = (GridHelper.OffsetPosition)offsetPositionIndex;
			}

			return offsetPosition;
		}

	}

}