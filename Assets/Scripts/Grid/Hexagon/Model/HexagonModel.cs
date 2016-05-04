using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexa2Go {

	public class HexagonModel : IHexagonModel {

		private bool _dontPropagate = false;

		private const int MAX_AMOUNT_OF_ALLOWED_CHARACTERS = 2;


		private GridPos _gridPos;
		private IHexagonState _state;
		private IList<GridPos> _neighbors;
		private IList<ICharacterModel> _characters;

		
		public HexagonModel (GridPos gridPos) {
			this._dontPropagate = false;

			_gridPos = gridPos;
			_state = new ActivatedNeutralHexagon (this);
			_characters = new List<ICharacterModel> ();

			InitNeighbors ();
		}

		public GridPos GridPos {
			get {
				return _gridPos;
			}
		}

		public IHexagonState State {
			get {
				return _state;
			}
			set {
				_state = value;
				if (!_dontPropagate) {
					PropagateUpdate ();
				}
				_dontPropagate = false;
			}
		}

		public IList<GridPos> Neighbors {
			get {
				return _neighbors;
			}
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

		public void PropagateUpdate () {
			EventArgs eventArgs = new EventArgs ();
			if (OnUpdatedData != null) {
				OnUpdatedData (this, eventArgs);
			}
		}

		public bool AddCharacter (ICharacterModel character) {
			bool result = false;
			if (_characters.Count < MAX_AMOUNT_OF_ALLOWED_CHARACTERS) {
				_characters.Add (character);
				result = true;
			}

			if (_characters.Count >= MAX_AMOUNT_OF_ALLOWED_CHARACTERS) {
				State.MarkAsBlocked ();
			}
			return result;
		}

		public bool RemoveCharacter (ICharacterModel character) {
			bool result = false;
			if (HasCharacter (character)) {
				_characters.Remove (character);
				result = true;
			}

			if (_characters.Count < MAX_AMOUNT_OF_ALLOWED_CHARACTERS) {
				State.MarkAsNormal ();
			}

			return result;
		}

		public bool HasCharacter (ICharacterModel character) {
			return _characters.Contains (character);
		}

		public bool HasCharacter (TeamColor teamColor) {
			bool result = false;

			foreach (ICharacterModel character in _characters) {

				if (character.TeamColor == teamColor) {
					result = true;
					break;
				}
			}

			return result;
		}

		public IList<ICharacterModel> GetCharacters () {
			return _characters;
		}

		public IList<ICharacterModel> GetCharacters (TeamColor teamColor) {
			IList<ICharacterModel> result = new List<ICharacterModel> ();

			foreach (ICharacterModel character in _characters) {
				if (character.TeamColor == teamColor) {
					result.Add (character);
				}
			}

			return result;
		}

		public IList<ICharacterModel> GetCharacters (CharacterType type) {
			IList<ICharacterModel> result = new List<ICharacterModel> ();
			
			foreach (ICharacterModel character in _characters) {
				if (character.Type == type) {
					result.Add (character);
				}
			}
			
			return result;
		}

		public IList<ICharacterModel> GetCharacters (CharacterType type, TeamColor teamColor) {
			IList<ICharacterModel> result = new List<ICharacterModel> ();
			
			foreach (ICharacterModel character in _characters) {
				if (character.Type == type && character.TeamColor == teamColor) {
					result.Add (character);
				}
			}
			
			return result;
		}

		public bool IsBlocked {
			get {
				bool result = false;
				IList<ICharacterModel> characters = GetCharacters ();
				if (characters.Count >= 2) {
					result = true;
				}
				return result;
			}
		}

		public bool DontPropagate {
			get {
				return _dontPropagate;
			}
			set {
				_dontPropagate = value;
			}
		}

		#region IHexagonModel implementation
		public event EventHandler<EventArgs> OnUpdatedData;
		#endregion
	}

}