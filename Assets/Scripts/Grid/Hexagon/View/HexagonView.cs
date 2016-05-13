using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hexa2Go {

	public class HexagonView : MonoBehaviour, IHexagonView, IPointerClickHandler {

		public event EventHandler<EventArgs> OnClicked = (sender, e) => {};
		public event EventHandler<EventArgs> OnCheckIsBlocked = (sender, e) => {};

		private Color _defaultAreaColor;
		private Color _defaultBorderColor;

		private Color _nextAreaColor;
		private Color _nextBorderColor;

		private IHexagonState _state;
		private bool _activate = false;
		private bool _deactivate = false;

		private float _animationTime = 0f;

		const float SPEED = 1.8f;

		public AudioClip BurningClip;
		public AudioClip SelectClip;
		public AudioClip FocusClip;
		public AudioClip ActivateClip;
		public AudioClip DeactivateClip;

		private AudioSource _audioSource;

		void Awake () {	
			//_defaultAreaColor = HexagonColors.WHITE;
			//_defaultBorderColor = HexagonColors.WHITE;
		}

		private void TintBorder (Color color) {
			transform.GetChild (1).GetChild (0).GetComponent<MeshRenderer> ().material.color = color;
		}
		
		private void TintArea (Color color) {
			transform.GetChild (0).GetChild (0).GetComponent<MeshRenderer> ().material.color = color;
		}

		private IEnumerator WaitForActivate () {
			yield return new WaitForSeconds (0.5f);
			_activate = true;
		}

		#region IHexagonView implementation

		public bool IsActivated { 
			get {
				return _state.IsActivated;
			}
		}

		public void Init (GridPos gridPos, IHexagonState state) {
			_audioSource = GetComponent<AudioSource>();
			SoundManager.Instance.RegisterClip(_audioSource);

			Vector3 tmp = GridHelper.HexagonPosition (gridPos);
			transform.position = tmp;
			UpdateState (state);
			if (IsActivated) {
				Activate (state);
			} else {
				Deactivate (state);
			}
		}

		public void UpdateState (IHexagonState state) {
			if (_state != null) {
				_defaultAreaColor = _state.AreaColor;
				_defaultBorderColor = _state.BorderColor;
			}
			_state = state;
		}

		public void Tint (IHexagonState state) {
			TintArea (state.AreaColor);
			TintBorder (state.BorderColor);
		}

		public void Activate (IHexagonState state, bool animated = false) {
			_nextAreaColor = state.AreaColor;
			_nextBorderColor = state.BorderColor;

			if (animated) {
				_animationTime = 0f;
				StartCoroutine (WaitForActivate ());
			} else {
				transform.position = new Vector3 (transform.position.x, GridHelper.ACTIVATED_Y_POS, transform.position.z);
				Tint (_state);
			}
		}

		public void Deactivate (IHexagonState state, bool animated = false) {
			_nextAreaColor = state.AreaColor;
			_nextBorderColor = state.BorderColor;

			if (animated) {
				_animationTime = 0f;
				_deactivate = true;
				IGameMode gm = GameManager.Instance.GetGameMode();
				if (gm.GetMatchStateName(gm.GetMatchState()) != MatchStates.NullState) {
					_audioSource.PlayOneShot(DeactivateClip);
				}
			} else {
				transform.position = new Vector3 (transform.position.x, GridHelper.DEACTIVATED_Y_POS, transform.position.z);
				Tint (_state);
			}
		}

		void FixedUpdate () {
			if (_activate) {

				if (_animationTime <= 0f) {
					_audioSource.PlayOneShot(ActivateClip);
				}
				
				_animationTime += Time.deltaTime * SPEED;
				
				if (_animationTime > 1f) {
					_animationTime = 1f;
					
					_activate = false;
					if (OnCheckIsBlocked != null) {
						OnCheckIsBlocked (this, new EventArgs ());
					}
					GameManager.Instance.GetGameMode ().SwitchToNextMatchState ();
				}
				
				Color colorArea = Color.Lerp (_defaultAreaColor, _nextAreaColor, _animationTime);
				Color colorBorder = Color.Lerp (_defaultBorderColor, _nextBorderColor, _animationTime);
				TintArea (colorArea);
				TintBorder (colorBorder);
				
				float yPos = Mathf.Lerp (GridHelper.DEACTIVATED_Y_POS, GridHelper.ACTIVATED_Y_POS, _animationTime);
				transform.position = new Vector3 (transform.position.x, yPos, transform.position.z);
			}
			
			if (_deactivate) {
				
				_animationTime += Time.deltaTime * SPEED;
				
				if (_animationTime > 1f) {
					_animationTime = 1f;
					
					_deactivate = false;
				}
				Color colorArea = Color.Lerp (_defaultAreaColor, _nextAreaColor, _animationTime);
				Color colorBorder = Color.Lerp (_defaultBorderColor, _nextBorderColor, _animationTime);
				TintArea (colorArea);
				TintBorder (colorBorder);
				
				float yPos = Mathf.Lerp (GridHelper.ACTIVATED_Y_POS, GridHelper.DEACTIVATED_Y_POS, _animationTime);
				transform.position = new Vector3 (transform.position.x, yPos, transform.position.z);
			}
		}

		public void PlayExplosion (bool playLoop = false) {
			StartCoroutine (WaitForExplosion (playLoop));
		}

		private IEnumerator WaitForExplosion (bool playLoop) {
			yield return new WaitForSeconds (0.6f);
			Color color = transform.GetChild (0).GetChild (0).GetComponent<MeshRenderer> ().material.color;
			Transform particles = transform.GetChild (2);
			for (int i=0; i < particles.childCount; i++) {
				ParticleSystem particle = particles.GetChild (i).GetComponent<ParticleSystem> ();
				particle.startColor = color;
				if (playLoop) {
					particle.loop = true;
				}
				particle.Play ();
			}
			_audioSource.PlayOneShot(BurningClip);
		}

		public void PlaySelectionClip () {
			_audioSource.PlayOneShot(SelectClip);
		}

		public void PlayFocusClip () {
			_audioSource.PlayOneShot(FocusClip);
		}

		void OnDestroy () {
			SoundManager.Instance.UnregisterClip(_audioSource);
		}

		#endregion

		#region IPointerClickHandler implementation
		public void OnPointerClick (PointerEventData eventData) {
			OnClicked (this, new EventArgs ());
		}
		#endregion
	}

}