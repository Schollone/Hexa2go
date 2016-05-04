using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hexa2Go {

	public class CharacterModel : ICharacterModel {

		private GridPos _gridPos;
		private CharacterType _type;
		private TeamColor _teamColor;
		private ICharacterState _state;

		private bool _isInGame = true;
		private GridHelper.OffsetPosition _offsetPosition;

		public CharacterModel (GridPos gridPos, TeamColor teamColor, CharacterType type) {
			_gridPos = gridPos;

			System.Random rnd = new System.Random (Guid.NewGuid ().GetHashCode ());
			int offsetPositionIndex = rnd.Next (0, 4);
			_offsetPosition = (GridHelper.OffsetPosition) offsetPositionIndex;

			_teamColor = teamColor;

			_type = type;
			_state = new NormalCharacter(this);
		}

		#region ICharacterModel implementation

		public CharacterType Type { 
			get {
				return _type;
			}
		}

		public ICharacterState State {
			get {
				return _state;
			}
			set {
				_state = value;
				PropagateUpdate ();
			}
		}

		public TeamColor TeamColor {
			get {
				return _teamColor;
			}
		}

		public void PropagateUpdate () {
			if (OnUpdatedData != null) {
				OnUpdatedData (this, new EventArgs ());
			}
		}

		public event EventHandler<EventArgs> OnUpdatedData;
		public event EventHandler<EventArgs> OnGridPosChanged;
		public event EventHandler<EventArgs> OnTargetReached;

		public GridPos GridPos {
			get {
				return _gridPos;
			}
			set {
				_gridPos = value;

				if (OnGridPosChanged != null) {
					OnGridPosChanged (this, new EventArgs ());
				}
			}
		}

		public bool IsInGame {
			get {
				return _isInGame;
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

		public void Remove () {
			_isInGame = false;
			if (OnTargetReached != null) {
				OnTargetReached (this, new EventArgs ());
			}
		}

		#endregion

	}

}