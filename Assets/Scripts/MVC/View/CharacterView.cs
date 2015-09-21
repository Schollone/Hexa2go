using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class CharacterView : MonoBehaviour, ICharacterView {

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void Init (GridPos gridPos) {
			Vector3 tmp = GridHelper.HexagonPosition(gridPos);
			transform.position = tmp;
		}

		public void Tint (Color color) {
			transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;
		}

		public void Select () {
			transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
		}

		public void Deselect () {
			transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		}

		public void Move (GridPos gridPos) {
			Vector3 tmp = GridHelper.HexagonPosition(gridPos);
			transform.position = tmp;
		}

		public void Remove () {
			gameObject.SetActive(false);
		}
	}

}