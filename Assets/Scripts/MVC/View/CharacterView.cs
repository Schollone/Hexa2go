using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class CharacterView : MonoBehaviour, ICharacterView {

		public void Init (GridPos gridPos, GridHelper.OffsetPosition offsetPosition) {
			Vector3 tmp = GridHelper.HexagonPosition (gridPos);
			tmp += GridHelper.CharacterOffset (offsetPosition);
			transform.position = tmp;
		}

		public void Tint (Color color) {
			transform.GetChild (0).GetComponent<MeshRenderer> ().material.color = color;
		}

		public void Select () {
			transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
		}

		public void Deselect () {
			transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
		}

		public void Move (GridPos gridPos, GridHelper.OffsetPosition offsetPosition) {
			Vector3 tmp = GridHelper.HexagonPosition (gridPos);
			tmp += GridHelper.CharacterOffset (offsetPosition);
			transform.position = tmp;
		}

		public void Remove () {
			gameObject.SetActive (false);
		}

		public void Rotate () {
			transform.Rotate (0f, 180f, 0f);
		}
	}

}