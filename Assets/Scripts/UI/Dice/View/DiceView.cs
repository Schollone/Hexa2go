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

		private Image _background;

		private float[] noiseValues;

		public AudioClip DiceRollClip;
		public AudioClip DoubleDiceClip;

		private AudioSource _audioSource;
		
		// Use this for initialization
		protected override void Awake () {
			base.Awake ();

			_characterTypes = new Dictionary<CharacterType, Sprite> ();
			_characterTypes.Add (CharacterType.CIRCLE, symbol_Circle);
			_characterTypes.Add (CharacterType.TRIANGLE, symbol_Triangle);
			_characterTypes.Add (CharacterType.SQUARE, symbol_Square);
			
			_teamColors = new Dictionary<TeamColor, Color> ();
			_teamColors.Add (TeamColor.BLUE, HexagonColors.DICE_BLUE);
			_teamColors.Add (TeamColor.RED, HexagonColors.DICE_RED);

			_symbol = transform.FindChild ("Symbol");

			_background = transform.GetChild(0).GetComponent<Image>();

			UpdateViewByIndex (-1, -1);
		}

		// Use this for initialization
		protected override void Start () {
			base.Start ();
			_audioSource = GetComponent<AudioSource>();
			SoundManager.Instance.RegisterClip(_audioSource);
		}
		
		// Update is called once per frame
		protected override void Update () {
			base.Update ();
		}
		
		#region IDiceView implementation
		public event EventHandler<DiceThrowedEventArgs> OnThrowed = (sender, e) => {};

		public void UpdateBackground (bool select = false) {
			if (select) {
				_background.color = HexagonColors.ORANGE;
			} else {
				_background.color = HexagonColors.WHITE;
			}
		}

		public void UpdateView (CharacterType characterType, TeamColor teamColor) {
			Sprite sprite = null;
			_characterTypes.TryGetValue (characterType, out sprite);
			_symbol.GetComponent<Image> ().overrideSprite = sprite;
			Color color;
			_teamColors.TryGetValue (teamColor, out color);
			_symbol.GetComponent<Image> ().color = color;
		}

		public DiceObject UpdateViewByIndex (int characterTypeIndex, int teamColorIndex) {
			CharacterType characterType = getCharacterType (characterTypeIndex);
			TeamColor teamColor = getTeamColor (teamColorIndex);

			UpdateView (characterType, teamColor);

			DiceObject diceObject = new DiceObject (characterType, teamColor);
			return diceObject;
		}

		public MonoBehaviour This {
			get {
				return this;
			}
		}

		public void StartThrow () {
			StartCoroutine ("Throw");
		}

		public void PlayDiceRoll () {
			_audioSource.PlayOneShot (DiceRollClip);
		}

		public void PlayDoubleDice () {
			_audioSource.PlayOneShot (DoubleDiceClip);
		}
		#endregion

		public IEnumerator Throw () {

			DiceObject diceObject = new DiceObject ();
			float elapsedTime = UnityEngine.Random.value;
			float interval = 0f;
			
			while (elapsedTime <= 3f) {
				
				elapsedTime += Time.deltaTime;
				interval += Time.deltaTime;

				if (interval >= Mathf.Exp (elapsedTime - 4)) {
					diceObject = UpdateViewByIndex (-1, -1);
					interval = 0;
				}
				
				yield return null;
			}

			DiceThrowedEventArgs eventArgs = new DiceThrowedEventArgs (diceObject);
			OnThrowed (this, eventArgs);
		}

		private CharacterType getCharacterType (int index) {
			CharacterType[] typeArray = new CharacterType[_characterTypes.Count];
			_characterTypes.Keys.CopyTo (typeArray, 0);
			if (index < 0 || index >= typeArray.Length) { // use random value
				int seed = Guid.NewGuid ().GetHashCode ();
				System.Random rnd = new System.Random (seed);
				index = rnd.Next (0, 3);
			}
			return typeArray [index];
		}

		private TeamColor getTeamColor (int index) {
			TeamColor[] colorArray = new TeamColor[_teamColors.Count];
			_teamColors.Keys.CopyTo (colorArray, 0);
			if (index < 0 || index >= colorArray.Length) { // use random value
				int seed = Guid.NewGuid ().GetHashCode ();
				System.Random rnd = new System.Random (seed);
				index = rnd.Next (0, colorArray.Length);
			}
			return colorArray [index];
		}

		void OnDestroy () {
			SoundManager.Instance.UnregisterClip(_audioSource);
		}
	}

}