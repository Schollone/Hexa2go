using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Hexa2Go {

	public class DiceView : AbstractButtonView, IDiceView {

		public Sprite symbol_Circle;
		public Sprite symbol_Triangle;
		public Sprite symbol_Square;

		private Transform _symbol;
			
		private Dictionary<CharacterType, Sprite> _characterTypes;
		private Dictionary<TeamColor, Color> _teamColors;

		private float[] noiseValues;
		
		// Use this for initialization
		protected override void Awake () {
			base.Awake();

			_characterTypes = new Dictionary<CharacterType, Sprite> ();
			_characterTypes.Add (CharacterType.CIRCLE, symbol_Circle);
			_characterTypes.Add (CharacterType.TRIANGLE, symbol_Triangle);
			_characterTypes.Add (CharacterType.SQUARE, symbol_Square);
			
			_teamColors = new Dictionary<TeamColor, Color> ();
			_teamColors.Add (TeamColor.BLUE, HexagonColors.BLUE);
			_teamColors.Add (TeamColor.RED, HexagonColors.RED);

			_symbol = transform.FindChild("Symbol");

			UpdateViewByIndex(-1, -1);
		}

		// Use this for initialization
		protected override void Start () {
			base.Start();
		}
		
		// Update is called once per frame
		protected override void Update () {
			base.Update();
		}
		
		#region IDiceView implementation
		public void UpdateView (CharacterType characterType, TeamColor teamColor) {
			Sprite sprite = null;
			_characterTypes.TryGetValue (characterType, out sprite);
			_symbol.GetComponent<Image> ().overrideSprite = sprite;
			Color color;
			_teamColors.TryGetValue (teamColor, out color);
			_symbol.GetComponent<Image> ().color = color;
			//Debug.Log(characterType + " ; " + teamColor + " !!!!!!!!!");
		}

		public DiceObject UpdateViewByIndex (int characterTypeIndex, int teamColorIndex) {
			CharacterType characterType = getCharacterType(characterTypeIndex);
			TeamColor teamColor = getTeamColor(teamColorIndex);

			UpdateView (characterType, teamColor);

			DiceObject diceObject = new DiceObject(characterType, teamColor);
			return diceObject;
		}

		public MonoBehaviour This {
			get {
				return this;
			}
		}

		public event EventHandler<DiceThrowedEventArgs> OnThrowed = (sender, e) => {};

		public void StartThrow() {
			StartCoroutine("Throw");
			//Throw();
		}
		#endregion

		public IEnumerator Throw() {

			DiceObject diceObject = new DiceObject();
			float elapsedTime = UnityEngine.Random.value;
			float interval = 0f;
			
			while (elapsedTime <= 3f) {
				
				elapsedTime += Time.deltaTime;
				interval += Time.deltaTime;
				//Debug.LogWarning(Time.time);
				
				if (interval >= Mathf.Exp(elapsedTime - 4)) {
					//Debug.LogWarning(Time.time);
					diceObject = UpdateViewByIndex(-1, -1);
					interval = 0;
				}
				
				yield return null;
			}

			DiceThrowedEventArgs eventArgs = new DiceThrowedEventArgs(diceObject);
			Debug.Log ("Throw: " + diceObject.CharacterType + " _ " + diceObject.TeamColor);
			OnThrowed(this, eventArgs);
			//Model.SetDiceValue(diceObject);
		}

		private CharacterType getCharacterType(int index) {
			CharacterType[] typeArray = new CharacterType[_characterTypes.Count];
			_characterTypes.Keys.CopyTo(typeArray, 0);
			if (index < 0 || index >= typeArray.Length) { // use random value
				//Debug.Log("1: " + Time.time);
				//System.Random random = new System.Random();
				//index = random.Next(0, typeArray.Length);
				//UnityEngine.Random.seed = 42;
				index = UnityEngine.Random.Range(0, 3);
			}
			return typeArray[index];
		}

		private TeamColor getTeamColor(int index) {
			TeamColor[] colorArray = new TeamColor[_teamColors.Count];
			_teamColors.Keys.CopyTo(colorArray, 0);
			if (index < 0 || index >= colorArray.Length) { // use random value
				//Debug.Log("2: " + Time.time);
				System.Random random = new System.Random();
				index = random.Next(0, colorArray.Length);
			}
			return colorArray[index];
		}
	}

}