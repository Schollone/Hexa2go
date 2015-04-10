using UnityEngine;
using System.Collections;

public class Hexagon {

	public static Color LIGHT_GRAY = new Color(0.8f, 0.8f, 0.8f);
	public static Color WHITE = Color.white;
	public static Color RED = Color.red;
	public static Color BLUE = Color.blue;
	public static Color GREEN = Color.green;
	public static Color ORANGE = new Color (1f, 0.68f, 0f);

	public enum TeamColor { NONE, RED, BLUE };

	// -- Basic object related
	private GameObject _gameObject;
	private GridPos _gridPos;
	private bool _isField = false;
	private bool _canReceiveHexagon = false;

	// -- Color related --
	private Color _defaultAreaColor = Hexagon.WHITE;
	private Color _defaultBorderColor = Hexagon.WHITE;
	private bool _isSelected = false;
	private bool _isFocused = false;

	// -- Character related --
	private Character _character1;
	private Character _character2;
	private bool _hasMoveableCharacter = false;
	private bool _canReceiveCharacter = false;
	private bool _isBlocked = false;
	private bool _isTarget = false;
	private TeamColor _teamColor = TeamColor.NONE;

	public Hexagon(GridPos newGridPos, GameObject newGameObject, bool newIsField = false, TeamColor newTeamColor = TeamColor.NONE) {
		Debug.Log ("Hexagon created");

		this._gameObject = newGameObject;
		this._gridPos = newGridPos;
		this._isField = newIsField;
		this._canReceiveHexagon = false;

		this._defaultAreaColor = Hexagon.WHITE;
		this._defaultBorderColor = Hexagon.WHITE;
		isField = this._isField;
		this._isSelected = false;
		this._isFocused = false;

		this._character1 = null;
		this._character2 = null;
		this._hasMoveableCharacter = false;
		this._canReceiveCharacter = false;
		this._isBlocked = false;
		this._isTarget = false;

		this._teamColor = newTeamColor;
		if (this._teamColor == TeamColor.BLUE) {
			this._gameObject.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material.color = Hexagon.BLUE;
			this._defaultBorderColor = Hexagon.BLUE;
		}
		if (this._teamColor == TeamColor.RED) {
			this._gameObject.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material.color = Hexagon.RED;
			this._defaultBorderColor = Hexagon.RED;
		}
	}

	public GameObject gameObject {
		get {
			return _gameObject;
		}
	}
	
	public GridPos gridPos {
		get {
			return _gridPos;
		}
	}

	public bool isField {
		get {
			return _isField;
		}
		set {
			this._isField = value;
			if (this._isField) {
				this._defaultAreaColor = Hexagon.LIGHT_GRAY;
				this._defaultBorderColor = Hexagon.LIGHT_GRAY;
				this._gameObject.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = this._defaultAreaColor;
				this._gameObject.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material.color = this._defaultBorderColor;
			} else {
				this._defaultAreaColor = Hexagon.WHITE;
				this._defaultBorderColor = Hexagon.WHITE;
				this._gameObject.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = this._defaultAreaColor;
				this._gameObject.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material.color = this._defaultBorderColor;
			}
		}
	}

	public bool canReceiveHexagon {
		get {
			return _canReceiveHexagon;
		}
		set {
			_canReceiveHexagon = value;
		}
	}

	// --------------------------------------------------------------------------------------

	public Color defaultAreaColor {
		get {
			return _defaultAreaColor;
		}
	}

	public Color defaultBorderColor {
		get {
			return _defaultBorderColor;
		}
	}

	public bool isSelected {
		get {
			return this._isSelected;
		}
		set {
			this._isSelected = value;
			if (!value) {
				this._gameObject.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = this._defaultAreaColor;
				this._gameObject.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material.color = this._defaultBorderColor;
			}
			if (value) {
				this._gameObject.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = Hexagon.ORANGE;
				this._gameObject.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material.color = Hexagon.ORANGE;
			}
		}
	}

	public bool isFocused {
		get {
			return _isFocused;
		}
		set {
			_isFocused = value;
		}
	}

	// --------------------------------------------------------------------------------------

	public Character character1 {
		get {
			return _character1;
		}
		set {
			_character1 = value;
		}
	}

	public Character character2 {
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

	public bool canReceiveCharacter {
		get {
			this._canReceiveCharacter = false;
			if (isField && !isBlocked) {
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
			if (this._isField && this._teamColor != TeamColor.NONE) {
				return true;
			}
			return _isTarget;
		}
	}

	public TeamColor teamColor {
		get {
			return _teamColor;
		}
	}

}
