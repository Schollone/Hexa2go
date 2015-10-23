using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class CharacterView : MonoBehaviour, ICharacterView {

		private Vector3 target = new Vector3 ();
		private Vector3 start = new Vector3 ();

		private bool move = false;
		private float _animationTime = 0f;
		const float ANIMATION_SPEED = 1.8f;

		private bool _place = false;
		private float _placingTime = 0f;
		const float PLACING_SPEED = 2f;

		void FixedUpdate () {
			if (move) {
				Vector3 bezier = (target + start) / 2;
				bezier.y = 5f;

				_animationTime += Time.deltaTime * ANIMATION_SPEED;

				if (_animationTime > 1) {
					_animationTime = 1f;
				}

				transform.position = GridHelper.Bezier (start, bezier, target, _animationTime);

				if (transform.position == target) {
					move = false;
					_animationTime = 0f;
					_place = true;
				}
			}

			if (_place) {
				_placingTime += Time.deltaTime * PLACING_SPEED;

				if (_placingTime >= 1f) {
					_placingTime = 1f;
					_place = false;
				}
				float yPos = Mathf.Lerp (-GridHelper.ACTIVATED_Y_POS, GridHelper.DEACTIVATED_Y_POS, _placingTime);
				transform.position = new Vector3 (transform.position.x, yPos, transform.position.z);
			}
		}

		public void Init (GridPos gridPos, GridHelper.OffsetPosition offsetPosition) {
			Vector3 tmp = GridHelper.HexagonPosition (gridPos);
			tmp += GridHelper.CharacterOffset (offsetPosition);
			transform.position = tmp;
		}

		public void Tint (Color color) {
			transform.GetChild (0).GetChild (0).GetComponent<MeshRenderer> ().material.color = color;
		}

		public void Select () {
			transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
		}

		public void Deselect () {
			transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
		}

		public void Move (GridPos gridPos, GridHelper.OffsetPosition offsetPosition, bool jump = true) {
			Vector3 tmp = GridHelper.HexagonPosition (gridPos);
			tmp += GridHelper.CharacterOffset (offsetPosition);
			_placingTime = 0f;
			if (GameManager.Instance.GridHandler.HexagonHandler.Get (gridPos).View.DefaultAreaColor != HexagonColors.WHITE) {
				_placingTime = 1f;
			}

			if (jump) {
				tmp.y -= GridHelper.ACTIVATED_Y_POS;
				target = tmp;
				start = transform.position;
				move = true;
			} else {
				transform.position = tmp;
			}

		}

		public void Remove () {
			StartCoroutine (WaitForRemove ());
		}

		private IEnumerator WaitForRemove () {
			yield return new WaitForSeconds (0.7f);
			gameObject.SetActive (false);
		}

		public void Rotate () {
			transform.Rotate (0f, 180f, 0f);
		}
	}

}