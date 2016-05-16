using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Hexa2Go {

	public class PauseMatch : MonoBehaviour {
				
		void Start () {
			GetComponent<Button> ().onClick.AddListener (OnPause);
		}
				
		private void OnPause () {
			GameObject.Find ("PauseScreen").transform.GetChild (0).gameObject.SetActive (true);
			Time.timeScale = 0f;
			SoundManager.Instance.PauseClips();
		}

	}

}