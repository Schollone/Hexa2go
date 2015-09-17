using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public class Grid {

		/*private int _width = 0;
		private int _height = 0;
		private Dictionary<GridPos, HexagonModel> _hexagons;
		private IList<HexagonModel> _usedHexagons;
		private int _usedHexagonIndex = 0;
		private Dictionary<GridPos, Vector3> _gridPostionsVector3;
		private ICharacterModel _selectedCharacter = null;
		private HexagonModel _selectedHexagon;
		private HexagonModel _focusedHexagon;
		private PlayerModel _activePlayer;

		private List<GridPos> _gridStartPositions;

		private List<HexagonModel> _outdatedNeighbars;

		public delegate void MyDelegate(HexagonModel hexagon, ICharacterModel character);
		public event MyDelegate OnCharacterReachedTarget;

		public Grid(int width, int height) {
			this._width = width;
			this._height = height;

			this._hexagons = new Dictionary<GridPos, HexagonModel> ();
			this._usedHexagons = new List<HexagonModel> ();
			this._usedHexagonIndex = 0;
			this._selectedCharacter = null;
			this._selectedHexagon = null;
			this._focusedHexagon = null;
			this._activePlayer = null;

			this._gridPostionsVector3 = new Dictionary<GridPos, Vector3> ();
			this._outdatedNeighbars = new List<HexagonModel> ();

			this._gridStartPositions = new List<GridPos>();
			this._gridStartPositions.Add(new GridPos(2, 3));
			this._gridStartPositions.Add(new GridPos(3, 2));
			this._gridStartPositions.Add(new GridPos(3, 3));
			this._gridStartPositions.Add(new GridPos(4, 2));
			this._gridStartPositions.Add(new GridPos(4, 3));
			this._gridStartPositions.Add(new GridPos(4, 4));
			this._gridStartPositions.Add(new GridPos(5, 2));
			this._gridStartPositions.Add(new GridPos(5, 3));
			this._gridStartPositions.Add(new GridPos(5, 4));
			this._gridStartPositions.Add(new GridPos(6, 3));
			this._gridStartPositions.Add(new GridPos(6, 4));
			this._gridStartPositions.Add(new GridPos(7, 3));
			
			initGridPositions ();
		}

		public int width {get { return _width; }}

		public int height {get { return _height; }}

		public Dictionary<GridPos, HexagonModel> hexagons {get { return _hexagons; }}
		
		public List<GridPos> gridStartPositions {get { return _gridStartPositions; }}

		public HexagonModel getHexagon(GridPos gridPos) {
			HexagonModel hexagon;
			this._hexagons.TryGetValue (gridPos, out hexagon);
			return hexagon;
		}

		public IList<HexagonModel> getUsedHexagons {
			get {
				return _usedHexagons;
			}
		}

		private void initGridPositions() {
			float yOffset = 4.75f;
			
			for (int y = 0; y < this._height; ++y) {
				for (int x = 0; x < this._width; ++x) {
					
					float zValue = -9.5f * y;
					if ( (x & 1) == 1 ) {
						zValue -= yOffset;
					}
					
					this._gridPostionsVector3.Add(new GridPos(x, y), new Vector3 (8.25f * x, 0, zValue) );
				}
			}
		}
		
		public Vector3 getPositionVector3(GridPos gridPos) {
			Vector3 tmp = new Vector3 ();
			_gridPostionsVector3.TryGetValue(gridPos, out tmp);
			return tmp;
		}

		private void tintNeighbors(IList<HexagonModel> filteredNeighbors, bool reset = false) {
			foreach (HexagonModel hex in filteredNeighbors) {
				//hex.isFocusable = !reset;
			}
		}

		public void resetNeighbors(HexagonModel hexagon) {
			tintNeighbors (this._outdatedNeighbars, true);
		}

		/*private void resetColors(Hexagon hexagon, List<Hexagon> hexagons) {
			foreach (Hexagon hex in hexagons) {
				hex.changeAreaColor(hex.defaultAreaColor);
				hex.changeBorderColor(hex.defaultBorderColor);
			}
			
			hexagon.changeAreaColor (hexagon.defaultAreaColor);
			hexagon.changeBorderColor (hexagon.defaultBorderColor);
		}*/
/*
		public ICharacterModel selectedCharacter {
			get {
				return _selectedCharacter;
			}
			set {
				_selectedCharacter = value;
				if (_selectedCharacter != null) {
					_selectedCharacter.isSelected = true;
				}
			}
		}
		
		public HexagonModel selectedHexagon {
			get {
				return _selectedHexagon;
			}
		}
		
		public HexagonModel focusedHexagon {
			get {
				return _focusedHexagon;
			}
		}
		
		public void focusHexagon(HexagonModel hexagon) {
			
			HexagonModel focusedHexagon = this._focusedHexagon;
			
			if (focusedHexagon != null) {
				focusedHexagon.isFocused = false;
			}
			
			if (hexagon != null) {
				hexagon.isFocused = true;
			}
			this._focusedHexagon = hexagon;
		}

		// Character related --------------------------------------------------

		private ICharacterModel getNextCharacter(TeamColor teamColor) {
			//List<Hexagon> usedHexagons = (List<Hexagon>) this._usedHexagons;

			World world = World.Instance;
			Debug.Log ("SelectedCharacter: " + this.selectedCharacter);
			
			foreach(HexagonModel hex in this._usedHexagons) {

				if (hex.character1 != null && hex.character1.teamColor == teamColor && !hex.character1.Equals(this.selectedCharacter) ) {
					//if (hex.character1.type == world.cube1.characterType || hex.character1.type == world.cube2.characterType) {
						Debug.Log("Used Hexagon Position 1: " + hex.ToString());
						return hex.character1;
					//}
				} 
				if (hex.character2 != null && hex.character2.teamColor == teamColor && !hex.character2.Equals(this.selectedCharacter) ) {
					//if (hex.character2.type == world.cube1.characterType || hex.character2.type == world.cube2.characterType) {
						Debug.Log("Used Hexagon Position 2: " + hex.ToString());
						return hex.character2;
					//}
				}
			}

			Debug.Log("Used Selected Hexagon!");
			return this.selectedCharacter;
		}

		private HexagonModel findNextCharacterHexagon(ICharacterModel character) {
			//List<Hexagon> usedHexagons = (List<Hexagon>) this._usedHexagons;
			
			foreach(HexagonModel hex in this._usedHexagons) {
				if (hex.character1 == character || hex.character2 == character) {
					return hex;
				}
			}
			
			return null;

			//Character character = findNextCharacter(teamColor);
			//character.gameObject.transform.position = 

		}

		private List<HexagonModel> findCharacterNeighbors (HexagonModel centeredHexagon) {
			//List<Hexagon> neighbors = (List<Hexagon>) centeredHexagon.neighbors;
			List<HexagonModel> filteredNeighbors = new List<HexagonModel> ();
			/*
			foreach (HexagonModel hex in centeredHexagon.neighbors) {
				if (hex.canReceiveCharacter) {
					filteredNeighbors.Add(hex);
				}
			}
			*/
			//_oldNeighbars = filteredNeighbors;
			/*
			return filteredNeighbors;
		}

		private HexagonModel findNextCharacterNeighbor (HexagonModel centeredHexagon) {
			HexagonModel[] neighbors = findCharacterNeighbors (centeredHexagon).ToArray();
			
			centeredHexagon.neighborIndex++;
			if (centeredHexagon.neighborIndex >= neighbors.Length) {
				centeredHexagon.neighborIndex = 0;
			}
			
			HexagonModel hex = neighbors[centeredHexagon.neighborIndex];
			
			return hex;
		}
		
		private HexagonModel findPrevCharacterNeighbor(HexagonModel centeredHexagon) {
			HexagonModel[] neighbors = findCharacterNeighbors (centeredHexagon).ToArray();
			
			centeredHexagon.neighborIndex--;
			if (centeredHexagon.neighborIndex < 0) {
				centeredHexagon.neighborIndex = neighbors.Length-1;
			}
			
			HexagonModel hex = neighbors[centeredHexagon.neighborIndex];
			
			return hex;
		}

		private void selectCharacterHexagon(ref HexagonModel hexagon) {
			
			HexagonModel selectedHexagon = this._selectedHexagon;
			
			if (selectedHexagon != null) {
				selectedHexagon.IsSelected = false;
				resetNeighbors(selectedHexagon);
			}
			
			if (hexagon != null) {
				hexagon.IsSelected = true;
				_outdatedNeighbars = findCharacterNeighbors(hexagon);
				tintNeighbors(_outdatedNeighbars);
			}
			this._selectedHexagon = hexagon;
		}

		private void selectCharacter(HexagonModel nextHexagon, ICharacterModel nextCharacter) {
			HexagonModel outdatedHexagon = this._selectedHexagon;

			if (outdatedHexagon != null) {
				if (this.selectedCharacter.characterPosition == CharacterPosition.Position_1) {
					outdatedHexagon.character1.isSelected = false;
				} else if (this.selectedCharacter.characterPosition == CharacterPosition.Position_2) {
					outdatedHexagon.character2.isSelected = false;
				}
			}

			nextCharacter.isSelected = true;
			Debug.Log ("Character: " + nextCharacter.ToString());
			selectedCharacter = nextCharacter;
			Debug.Log ("Hexagon: " + nextHexagon.ToString());
		}

		public void selectNextCharacter() {
			Debug.Log (World.Instance.activePlayer.teamColor);

			ICharacterModel character = getNextCharacter (World.Instance.activePlayer.teamColor);
			Debug.Log ("Next Character: " + character);

			HexagonModel hexagon = findNextCharacterHexagon (character);
			Debug.Log (hexagon.GridPos + " - " + character);

			if (findCharacterNeighbors (hexagon).Count > 0) { // verursacht falsch gespeicherte alte Nachbarfelder
				selectCharacter (hexagon, character);
				selectCharacterHexagon (ref hexagon);
			} else {
				// Kein Zug möglich --> nächster Spieler
				// ein Character geht nicht, aber beide Möglichkeiten nicht zugfähig???
			}

		}

		public void focusNextCharacterNeighbor() {
			HexagonModel hexagon = findNextCharacterNeighbor(_selectedHexagon);
			focusHexagon (hexagon);
		}
		
		public void focusPrevCharacterNeighbor() {
			HexagonModel hexagon = findPrevCharacterNeighbor(_selectedHexagon);
			focusHexagon (hexagon);
		}

		public void moveCharacter() {

		}

		public void moveSingleCharacter() {
			HexagonModel oldHexagon = _selectedHexagon;
			HexagonModel newHexagon = _focusedHexagon;

			Debug.Log ("oldHexagon.selectedCharacter: " + selectedCharacter);
			if (selectedCharacter != null) {

				if (newHexagon.hasEmptyCharacter1Slot()) {
					if (selectedCharacter.characterPosition == CharacterPosition.Position_1) {
						oldHexagon.character1 = null;
					} else if (selectedCharacter.characterPosition == CharacterPosition.Position_2) {
						oldHexagon.character2 = null;
					}
					selectedCharacter.gameObject.transform.position = World.Instance.grid.getPositionVector3(newHexagon.GridPos);
					selectedCharacter.isSelected = false;
					selectedCharacter.characterPosition = CharacterPosition.Position_1;
					newHexagon.character1 = selectedCharacter;

					Debug.Log ("Set to Position 1");
				} else if (newHexagon.hasEmptyCharacter2Slot()) {
					if (selectedCharacter.characterPosition == CharacterPosition.Position_1) {
						oldHexagon.character1 = null;
					} else if (selectedCharacter.characterPosition == CharacterPosition.Position_2) {
						oldHexagon.character2 = null;
					}
					selectedCharacter.gameObject.transform.position = World.Instance.grid.getPositionVector3(newHexagon.GridPos);
					selectedCharacter.isSelected = false;
					selectedCharacter.characterPosition = CharacterPosition.Position_2;
					newHexagon.character2 = selectedCharacter;
					Debug.Log ("Set to Position 2");
				}

				if (newHexagon.isTarget && newHexagon.teamColor == selectedCharacter.teamColor) {
					OnCharacterReachedTarget(newHexagon, selectedCharacter);
				}

				selectedCharacter = null;

			}
		}

		private void moveBothCharacters(HexagonModel oldHexagon, HexagonModel newHexagon) {
			if (oldHexagon.character1 != null) {
				oldHexagon.character1.gameObject.transform.position = World.Instance.grid.getPositionVector3(newHexagon.GridPos);

				oldHexagon.character1 = null;
				newHexagon.character1 = oldHexagon.character1;
			}
			if (oldHexagon.character2 != null) {
				oldHexagon.character2.gameObject.transform.position = World.Instance.grid.getPositionVector3(newHexagon.GridPos);

				oldHexagon.character2 = null;
				newHexagon.character2 = oldHexagon.character2;
			}
		}

		// Hexagon related --------------------------------------------------


		private HexagonModel findNextHexagon() {
			List<HexagonModel> usedHexagons = (List<HexagonModel>) this._usedHexagons;
			
			HexagonModel[] hexagonArray = usedHexagons.ToArray ();

			this._usedHexagonIndex++;
			if (_usedHexagonIndex >= hexagonArray.Length) {
				_usedHexagonIndex = 0;
			}

			HexagonModel hexagon = hexagonArray[this._usedHexagonIndex];
			
			return hexagon;
		}

		private HexagonModel findPrevHexagon() {
			List<HexagonModel> usedHexagons = (List<HexagonModel>) this._usedHexagons;
			
			HexagonModel[] hexagonArray = usedHexagons.ToArray ();

			this._usedHexagonIndex--;
			if (_usedHexagonIndex < 0) {
				_usedHexagonIndex = hexagonArray.Length - 1;
			}

			HexagonModel hexagon = hexagonArray[this._usedHexagonIndex];
			
			return hexagon;
		}

		public void selectHexagon(HexagonModel hexagon) {

			HexagonModel selectedHexagon = this._selectedHexagon;
			
			if (selectedHexagon != null) {
				selectedHexagon.IsSelected = false;
				resetNeighbors(selectedHexagon);
			}
			
			if (hexagon != null) {
				hexagon.IsSelected = true;
				_outdatedNeighbars = findHexagonNeighbors(hexagon);
				tintNeighbors(_outdatedNeighbars);
			}
			this._selectedHexagon = hexagon;
		}

		public void selectNextHexagon() {
			HexagonModel hexagon = null;
			do {
				hexagon = findNextHexagon ();
				Debug.Log (hexagon.GridPos);
			} while(findHexagonNeighbors (hexagon).Count <= 0);
			
			selectHexagon (hexagon);
		}

		public void selectPrevHexagon() {
			HexagonModel hexagon = null;
			do {
				hexagon = findPrevHexagon ();
				Debug.Log (hexagon.GridPos);
			} while(findHexagonNeighbors (hexagon).Count <= 0);
			
			selectHexagon (hexagon);
		}

		private List<HexagonModel> findHexagonNeighbors (HexagonModel centeredHexagon) {
			//List<Hexagon> neighbors = (List<Hexagon>) centeredHexagon.neighbors;
			List<HexagonModel> filteredNeighbors = new List<HexagonModel> ();
			/*
			foreach (HexagonModel hex in centeredHexagon.neighbors) {
				if (hex.canReceiveHexagon) {
					filteredNeighbors.Add(hex);
				}
			}
			*/
			// add condition for isolation
			/*

			
			return filteredNeighbors;
		}

		private HexagonModel findNextHexagonNeighbor(HexagonModel centeredHexagon) {
			HexagonModel[] neighbors = findHexagonNeighbors (centeredHexagon).ToArray();
			
			centeredHexagon.neighborIndex++;
			if (centeredHexagon.neighborIndex >= neighbors.Length) {
				centeredHexagon.neighborIndex = 0;
			}
			
			HexagonModel hex = neighbors[centeredHexagon.neighborIndex];
			
			return hex;
		}

		private HexagonModel findPrevHexagonNeighbor(HexagonModel centeredHexagon) {
			HexagonModel[] neighbors = findHexagonNeighbors (centeredHexagon).ToArray();
			
			centeredHexagon.neighborIndex--;
			if (centeredHexagon.neighborIndex < 0) {
				centeredHexagon.neighborIndex = neighbors.Length;
			}
			
			HexagonModel hex = neighbors[centeredHexagon.neighborIndex];
			
			return hex;
		}

		public void focusNextHexagonNeighbor() {
			HexagonModel hexagon = findNextHexagonNeighbor(_selectedHexagon);
			focusHexagon (hexagon);
		}
		
		public void focusPrevHexagonNeighbor() {
			HexagonModel hexagon = findPrevHexagonNeighbor(_selectedHexagon);
			focusHexagon (hexagon);
		}

		public void moveHexagon() {
			HexagonModel oldHexagon = _selectedHexagon;
			HexagonModel newHexagon = _focusedHexagon;

			moveBothCharacters (oldHexagon, newHexagon);

			newHexagon.IsField = true;
			oldHexagon.IsField = false;
			if (oldHexagon.isTarget) {
				newHexagon.teamColor = oldHexagon.teamColor;
			}
			this._usedHexagons.Remove (oldHexagon);
			this._usedHexagons.Add (newHexagon);
			List<HexagonModel> list = (List<HexagonModel>) this._usedHexagons;
			list.Sort (CompareUsedHexagonPositions);
			list.Reverse();
			this._usedHexagons = list;
		}

		private static int CompareUsedHexagonPositions(HexagonModel hex1, HexagonModel hex2) {
			if (hex1 == null) {
				if (hex2 == null) {
					return 0;
				} else {
					return -1;
				}
			} else {
				if (hex2 == null) {
					return 1;
				} else {
					if (hex1.GridPos.x > hex2.GridPos.x) {
						return -1;
					} else if (hex1.GridPos.x == hex2.GridPos.x) {
						if (hex1.GridPos.y > hex2.GridPos.y) {
							return -1;
						} else if (hex1.GridPos.y == hex2.GridPos.y) {
							return 0;
						} else {
							return 1;
						}
					} else {
						return 1;
					}
				}
			}
		}*/

	}

}