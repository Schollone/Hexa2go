using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Hexa2Go {

	public class PlayerView : MonoBehaviour, IPlayerView {

		Animator animator;
		Image gameplayButtons;
		Image acceptBg;
		Image hintBoxBg;
		Text currentPlayerText;

		private AudioSource _audioSource;

		void Awake () {
			animator = GameObject.Find ("CurrentPlayer").GetComponent<Animator> ();
			gameplayButtons = GameObject.Find ("GameplayButtons").GetComponent<Image> ();
			acceptBg = GameObject.Find ("Accept_Bg").GetComponent<Image> ();
			hintBoxBg = GameObject.Find ("HintBox_Bg").GetComponent<Image> ();
			currentPlayerText = GetComponent<Text> ();

			_audioSource = GetComponent<AudioSource>();
			SoundManager.Instance.RegisterClip(_audioSource);
		}

		public void UpdatePlayer (Color color, string name) {
			currentPlayerText.text = name;
			currentPlayerText.color = color;
			gameplayButtons.color = color;
			acceptBg.color = color;
			hintBoxBg.color = color;

			animator.SetTrigger (Animator.StringToHash ("ChangePlayer"));

			_audioSource.Play();
		}

		void OnDestroy () {
			SoundManager.Instance.UnregisterClip(_audioSource);
		}
	}

}