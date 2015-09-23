using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Hexa2Go {

	public abstract class AbstractButtonView : MonoBehaviour, IButtonView {

		public event EventHandler<ButtonClickedEventArgs> OnClicked = (sender, e) => {};

		protected GameObject _gameObject;
		protected Button _button;

		protected virtual void Awake () {
			_gameObject = gameObject;
			_button = GetComponent<Button> ();
			_button.onClick.AddListener (OnClick);
		}

		// Use this for initialization
		protected virtual void Start () {

		}
		
		// Update is called once per frame
		protected virtual void Update () {
		
		}

		protected virtual void OnClick () {
			ButtonClickedEventArgs eventArgs = new ButtonClickedEventArgs ();
			OnClicked (this, eventArgs);
		}

		public GameObject GameObject {
			get {
				return _gameObject;
			}
		}

		#region IButtonView implementation

		// changed to _gameObject instead of gameobject, because gameObject (DiceView) was acceced two times, so I had to unregister the handler-method in buttonhandler when changing the game state.
		public virtual void Show () {
			if (_gameObject != null) {
				_gameObject.SetActive (true);
			}
		}

		public virtual void Hide () {
			if (_gameObject != null) {
				_gameObject.SetActive (false);
			}
		}

		public virtual void Enable () {
			if (_button != null) {
				_button.interactable = true;
			}
		}

		public virtual void Disable () {
			if (_button != null) {
				_button.interactable = false;
			}
		}

		#endregion

		public virtual void SetViewParent () {
			GameObject gui = GameObject.Find ("GUI");
			Transform parent = gui.transform.GetChild (0).GetChild (0);
			transform.SetParent (parent);
		}
	}

}