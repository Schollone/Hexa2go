using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class CharacterModel : ICharacterModel {

		private GameObject _gameObject;
		private GridPos _gridPos;
		private TeamColor _teamColor;
		private CharacterType _type;
		private bool _isSelected = false;
		private CharacterPosition _characterPosition;
		
		
		public CharacterModel(GameObject newGameObject, TeamColor newTeamColor, CharacterType characterType) {
			this._gameObject = newGameObject;
			this._teamColor = newTeamColor;
			this._type = characterType;
			
			if (this._teamColor == TeamColor.BLUE) {
				this._gameObject.transform.GetChild (0).GetComponent<MeshRenderer> ().material.color = HexagonColors.BLUE;
			} else {
				this._gameObject.transform.GetChild (0).GetComponent<MeshRenderer> ().material.color = HexagonColors.RED;
			}
		}

		public CharacterModel(GridPos gridPos) {
			this._gridPos = gridPos;
		}

		public void Select() {
			this._isSelected = true;
			CharacterValueChangedEventArgs eventArgs = new CharacterValueChangedEventArgs();
			eventArgs.IsSelected = _isSelected;
			OnSelectionChanged(this, eventArgs);
		}

		public void Deselect() {
			this._isSelected = false;
			CharacterValueChangedEventArgs eventArgs = new CharacterValueChangedEventArgs();
			eventArgs.IsSelected = _isSelected;
			OnSelectionChanged(this, eventArgs);
		}
		
		public GameObject gameObject {
			get {
				return _gameObject;
			}
		}
		
		public GridPos GridPos {
			get {
				return _gridPos;
			}
			set {
				_gridPos = value;
			}
		}
		
		public TeamColor teamColor {
			get {
				return _teamColor;
			}
		}
		
		public CharacterType type {
			get {
				return _type;
			}
		}
		
		public bool isSelected {
			get {
				return _isSelected;
			}
			set {
				_isSelected = value;
			}
		}
		
		public CharacterPosition characterPosition {
			get {
				return _characterPosition;
			}
			set {
				_characterPosition = value;
			}
		}
		
		public override string ToString ()
		{
			return string.Format ("[Character: gameObject={0}, vectorPos={1}, teamColor={2}, type={3}, isSelected={4}, position={5}]", gameObject, GridPos, teamColor, type, isSelected, characterPosition);
		}

		#region ICharacterModel implementation
		public event System.EventHandler<CharacterValueChangedEventArgs> OnSelectionChanged;
		#endregion
	}

}