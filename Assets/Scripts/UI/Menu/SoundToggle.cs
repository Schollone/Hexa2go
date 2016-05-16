using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hexa2Go {

	public class SoundToggle : MonoBehaviour {

		public Sprite SoundOn;
		public Sprite SoundOff;

		private Image _ImageSound;

		void Start () {
			GetComponent<Button>().onClick.AddListener (ToggleSound);
			_ImageSound = GetComponent<Image>();
		}

		void ToggleSound () {
			if (SoundManager.Instance.Muted) {
				SoundManager.Instance.UnmuteClips ();
				_ImageSound.sprite = SoundOn;
			} else {
				SoundManager.Instance.MuteClips ();
				_ImageSound.sprite = SoundOff;
			}
		}
	}
}

