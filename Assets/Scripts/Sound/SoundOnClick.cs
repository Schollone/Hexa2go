using System;
using UnityEngine;

namespace Hexa2Go {
	public class SoundOnClick : MonoBehaviour {

		private AudioSource _audioSource;

		void Start () {
			_audioSource = GetComponent<AudioSource>();
		}

		public void PlaySound (bool playIfMuted = false) {
			if (playIfMuted) {
				if (SoundManager.Instance.Muted) {
					_audioSource.Play();
				}
			} else {
				if (!SoundManager.Instance.Muted) {
					_audioSource.Play();
				}
			}
		}
	}
}