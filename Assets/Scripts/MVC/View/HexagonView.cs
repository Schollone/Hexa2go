using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;

namespace Hexa2Go {

	public class HexagonView : MonoBehaviour, IHexagonView, IPointerClickHandler {

		public event EventHandler<EventArgs> OnClicked = (sender, e) => {};

		private Color _defaultAreaColor;
		private Color _defaultBorderColor;

		private Color _nextAreaColor;
		private Color _nextBorderColor;

		private TeamColor _teamColor;




		private IHexagonState _state;
		private bool _activate = false;
		private bool _deactivate = false;

		private float _animationTime = 0f;

		const float SPEED = 1.8f;

		void Awake () {	
			_defaultAreaColor = HexagonColors.WHITE;
			_defaultBorderColor = HexagonColors.WHITE;
		}

		public Color DefaultAreaColor {
			get {
				return _defaultAreaColor;
			}
			set {
				_defaultAreaColor = value;
			}
		}

		public Color DefaultBorderColor {
			get {
				return _defaultBorderColor;
			}
			set {
				_defaultBorderColor = value;
			}
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

		/*void OnHexagonClicked () {
			Debug.Log ("Hexagon Clicked !!!!!!!!!!!!!!!!!! ");
			OnClicked (this, new EventArgs ());
		}*/

		#region IHexagonView implementation

		public bool IsActivated { 
			get {
				return _state.IsActivated;
			}
		}

		public void Init (GridPos gridPos, IHexagonState state) {
			Vector3 tmp = GridHelper.HexagonPosition (gridPos);
			transform.position = tmp;
			UpdateState (state);
		}

		public void UpdateState (IHexagonState state) {
			_state = state;
		}

		public void Tint (IHexagonState state) {
			TintArea (state.AreaColor);
			TintBorder (state.BorderColor);
		}

		public void Activate (IHexagonState state, bool animated) {
			_nextAreaColor = state.AreaColor;
			_nextBorderColor = state.AreaColor;

			if (animated) {
				_animationTime = 0f;
				StartCoroutine (WaitForActivate ());
			} else {
				transform.position = new Vector3 (transform.position.x, GridHelper.ACTIVATED_Y_POS, transform.position.z);
				Tint (_state);
			}
		}

		public void Deactivate (bool animated) {
			if (animated) {
				_animationTime = 0f;
				_deactivate = true;
			} else {
				transform.position = new Vector3 (transform.position.x, GridHelper.DEACTIVATED_Y_POS, transform.position.z);
				Tint (_state);
			}
		}

		void FixedUpdate () {
			if (_activate) {
				
				_animationTime += Time.deltaTime * SPEED;
				
				if (_animationTime > 1) {
					_animationTime = 1f;
					
					_activate = false;
					_defaultAreaColor = _state.AreaColor;
					_defaultBorderColor = _state.BorderColor;
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
				
				if (_animationTime > 1) {
					_animationTime = 1f;
					
					_deactivate = false;
					_defaultAreaColor = _state.AreaColor;
					_defaultBorderColor = _state.BorderColor;
				}
				
				Color colorArea = Color.Lerp (_defaultAreaColor, _nextAreaColor, _animationTime);
				Color colorBorder = Color.Lerp (_defaultBorderColor, _nextBorderColor, _animationTime);
				TintArea (colorArea);
				TintBorder (colorBorder);
				
				float yPos = Mathf.Lerp (GridHelper.ACTIVATED_Y_POS, GridHelper.DEACTIVATED_Y_POS, _animationTime);
				transform.position = new Vector3 (transform.position.x, yPos, transform.position.z);
			}
		}













		public void Select () {
			TintArea (HexagonColors.ORANGE);
		}

		public void Deselect () {
			TintArea (_defaultAreaColor);
			TintBorder (_defaultBorderColor);
		}

		public void Focus () {
			TintArea (HexagonColors.GREEN);
			TintBorder (HexagonColors.GREEN);
		}

		public void Focusable () {
			TintBorder (HexagonColors.GREEN);
		}

		public void ResetTint () {
			TintArea (_defaultAreaColor);
			TintBorder (_defaultBorderColor);
		}


		public void Activate (Color? color = null, bool animate = false) {
			_nextAreaColor = (color != null) ? (Color)color : HexagonColors.LIGHT_GRAY;
			_nextBorderColor = (color != null) ? (Color)color : HexagonColors.LIGHT_GRAY;

			_animationTime = 0f;

			if (animate) {
				StartCoroutine (WaitForActivate ());
			} else {
				_defaultAreaColor = (color != null) ? (Color)color : HexagonColors.LIGHT_GRAY;
				_defaultBorderColor = (color != null) ? (Color)color : HexagonColors.LIGHT_GRAY;
				transform.position = new Vector3 (transform.position.x, GridHelper.ACTIVATED_Y_POS, transform.position.z);
				TintArea (_defaultAreaColor);
				TintBorder (_defaultBorderColor);
			}

		}



		/*public void Deactivate (bool animate = false) {
			_animationTime = 0f;

			if (animate) {
				_deactivate = true;
			} else {
				_defaultAreaColor = HexagonColors.WHITE;
				_defaultBorderColor = HexagonColors.WHITE;
				transform.position = new Vector3 (transform.position.x, GridHelper.DEACTIVATED_Y_POS, transform.position.z);
				TintArea (_defaultAreaColor);
				TintBorder (_defaultBorderColor);
			}
		}*/

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


		}

		public TeamColor TeamColor {
			get {
				return this._teamColor;
			}
			set {
				_teamColor = value;
			}
		}

		#endregion

		#region IPointerClickHandler implementation

		public void OnPointerClick (PointerEventData eventData) {
			OnClicked (this, new EventArgs ());
		}

		#endregion
	}

}