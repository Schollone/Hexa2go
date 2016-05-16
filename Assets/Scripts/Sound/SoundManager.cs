using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexa2Go {

	public class SoundManager {

		private static SoundManager _soundManager = null;

		private IList<AudioSource> _clips;
		private IList<AudioSource> _music;

		private bool _muted = false;

		private SoundManager () {
			_clips = new List<AudioSource>();
			_music = new List<AudioSource>();
		}

		public static SoundManager Instance {
			get {
				if (_soundManager == null) {
					_soundManager = new SoundManager();
				}
				return _soundManager;
			}
		}

		public bool Muted {
			get {
				return _muted;
			}
		}

		public void RegisterClip (AudioSource clip) {
			if (!_clips.Contains(clip)) {
				clip.mute = _muted;
				_clips.Add(clip);
			}
		}

		public void UnregisterClip (AudioSource clip) {
			if (_clips.Contains(clip)) {
				_clips.Remove(clip);
			}
		}

		public void RegisterMusic (AudioSource music) {
			if (!_music.Contains(music)) {
				music.mute = _muted;
				_music.Add(music);
			}
		}

		public void MuteClips () {
			AudioSource[] array = new AudioSource[_clips.Count];
			_clips.CopyTo (array, 0);
			foreach (AudioSource source in array) {
				source.mute = true;
			}
			_muted = true;
		}

		public void UnmuteClips () {
			AudioSource[] array = new AudioSource[_clips.Count];
			_clips.CopyTo (array, 0);
			foreach (AudioSource source in array) {
				source.mute = false;
			}
			_muted = false;
		}

		public void PauseClips () {
			AudioSource[] array = new AudioSource[_clips.Count];
			_clips.CopyTo (array, 0);
			foreach (AudioSource source in array) {
				if (source.isPlaying) {
					source.Pause();
				}
			}
		}

		public void ResumeClips () {
			AudioSource[] array = new AudioSource[_clips.Count];
			_clips.CopyTo (array, 0);
			foreach (AudioSource source in array) {
				source.UnPause();
			}
		}
	}
}

