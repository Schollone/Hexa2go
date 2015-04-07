using UnityEngine;
using System.Collections;

public class Character {

	private Vector3 gridPos;
	private TeamColor color;
	private CharacterType type;
	private bool isSelected = false;

}

public enum TeamColor {BLUE, RED};

public enum CharacterType {KNIGHT, BISHOP, PAWN};

