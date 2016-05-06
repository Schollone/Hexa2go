using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Hexa2Go {

	public class PlayerView : MonoBehaviour, IPlayerView {

		Animator animator;
		Image background;
		Image bgAccept;
		Image bgHintBox;
		Text text;

		private AudioSource audioSource;

		void Awake () {
			animator = GameObject.Find ("Player_Change").GetComponent<Animator> ();
			background = GameObject.Find ("Background").GetComponent<Image> ();
			bgAccept = GameObject.Find ("BG_Accept").GetComponent<Image> ();
			bgHintBox = GameObject.Find ("BG_HintBox").GetComponent<Image> ();
			text = GetComponent<Text> ();

			audioSource = GetComponent<AudioSource>();
		}

		public void UpdatePlayer (Color color, string name) {
			text.text = name;
			text.color = color;
			background.color = color;
			bgAccept.color = color;
			bgHintBox.color = color;

			animator.SetTrigger (Animator.StringToHash ("ChangePlayer"));

			audioSource.Play();
		}
	}

}