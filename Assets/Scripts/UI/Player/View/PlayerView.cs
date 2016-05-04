using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Hexa2Go {

	public class PlayerView : MonoBehaviour, IPlayerView {

		Animator animator;
		Image background;
		Text text;

		void Awake () {
			animator = GameObject.Find ("Player_Change").GetComponent<Animator> ();
			background = GameObject.Find ("Background").GetComponent<Image> ();
			text = GetComponent<Text> ();
		}

		public void UpdatePlayer (Color color, string name) {
			text.text = name;
			text.color = color;
			background.color = color;

			animator.SetTrigger (Animator.StringToHash ("ChangePlayer"));
			//animator.Play ("player_change");
		}
	}

}