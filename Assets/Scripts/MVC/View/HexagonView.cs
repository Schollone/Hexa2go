using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class HexagonView : MonoBehaviour, IHexagonView {

		private Color _defaultAreaColor;
		private Color _defaultBorderColor;

		private Color _nextAreaColor;
		private Color _nextBorderColor;

		private TeamColor _teamColor;
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

		#region IHexagonView implementation

		public void Init (GridPos gridPos) {
			Vector3 tmp = GridHelper.HexagonPosition (gridPos);
			transform.position = tmp;
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

		void FixedUpdate () {
			if (_activate) {

				_animationTime += Time.deltaTime * SPEED;

				if (_animationTime > 1) {
					_animationTime = 1f;

					_activate = false;
					_defaultAreaColor = _nextAreaColor;
					_defaultBorderColor = _nextBorderColor;
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
					_defaultAreaColor = HexagonColors.WHITE;
					_defaultBorderColor = HexagonColors.WHITE;
				}

				Color colorArea = Color.Lerp (_defaultAreaColor, HexagonColors.WHITE, _animationTime);
				Color colorBorder = Color.Lerp (_defaultBorderColor, HexagonColors.WHITE, _animationTime);
				TintArea (colorArea);
				TintBorder (colorBorder);

				float yPos = Mathf.Lerp (GridHelper.ACTIVATED_Y_POS, GridHelper.DEACTIVATED_Y_POS, _animationTime);
				transform.position = new Vector3 (transform.position.x, yPos, transform.position.z);
			}
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

		private IEnumerator WaitForActivate () {
			yield return new WaitForSeconds (0.5f);
			_activate = true;
		}

		public void Deactivate (bool animate = false) {
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
		}

		public void PlayExplosion () {
			StartCoroutine (WaitForExplosion ());
		}

		private IEnumerator WaitForExplosion () {
			yield return new WaitForSeconds (0.6f);
			Color color = transform.GetChild (0).GetChild (0).GetComponent<MeshRenderer> ().material.color;
			Transform particles = transform.GetChild (2);
			for (int i=0; i < particles.childCount; i++) {
				ParticleSystem particle = particles.GetChild (i).GetComponent<ParticleSystem> ();
				particle.startColor = color;
				particle.Play ();
			}


		}

		public Vector3 SlotPosition1 {
			get {
				throw new System.NotImplementedException ();
			}
		}

		public Vector3 SlotPosition2 {
			get {
				throw new System.NotImplementedException ();
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
	}

}