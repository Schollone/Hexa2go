using UnityEngine;
using System.Collections;

namespace Hexa2Go {

	public class CameraView : MonoBehaviour {

		public float MouseSpeed = 2f;
		public float MouseWheelZoomSpeed = 400f;

		public float TouchSpeed = 1;
		public float PerspectiveZoomSpeed = 1f;


		private Camera cameraComponent;

		// Use this for initialization
		void Start () {
			cameraComponent = gameObject.GetComponent<Camera> ();
		}
	
		// Update is called once per frame
		void Update () {
			HandleKeyboardInputs ();

			HandleTouchInputs ();

			if (SystemInfo.deviceType == DeviceType.Desktop) {
				HandleMouseInputs ();
			}
		}

		private void HandleKeyboardInputs () {
			if (Input.GetKey (KeyCode.A)) {
				if (transform.position.x >= 0) {
					Vector3 newPosition = new Vector3 (transform.position.x - 1f, transform.position.y, transform.position.z);
					transform.position = newPosition;
				}
			}
			if (Input.GetKey (KeyCode.D)) {
				if (transform.position.x <= 75) {
					Vector3 newPosition = new Vector3 (transform.position.x + 1f, transform.position.y, transform.position.z);
					transform.position = newPosition;
				}
			}
			if (Input.GetKey (KeyCode.W)) {
				if (transform.position.z <= -20) {
					//-60
					Vector3 newPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1f);
					transform.position = newPosition;
				}
			}
			if (Input.GetKey (KeyCode.S)) {
				if (transform.position.z >= -70) {
					Vector3 newPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1f);
					transform.position = newPosition;
				}
			}
		}

		private void HandleTouchInputs () {
			if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved) {
				// Get movement of the finger since last frame
				Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
				// Move object across XY plane
				//transform.Translate (-touchDeltaPosition.x, 0, -touchDeltaPosition.y);
				transform.position += new Vector3 (-touchDeltaPosition.x * Time.deltaTime * TouchSpeed, 0.0f, -touchDeltaPosition.y * Time.deltaTime * TouchSpeed);
				//transform.Translate (-touchDeltaPosition.x * Time.deltaTime * TouchSpeed, 0.0f, -touchDeltaPosition.y * Time.deltaTime * TouchSpeed);
				if (transform.position.z <= -70) {
					transform.position = new Vector3 (transform.position.x, transform.position.y, -70);
				}
				if (transform.position.z >= -20) {
					transform.position = new Vector3 (transform.position.x, transform.position.y, -20);
				}
				if (transform.position.x <= 0) {
					transform.position = new Vector3 (0, transform.position.y, transform.position.z);
				}
				if (transform.position.x >= 75) {
					transform.position = new Vector3 (75, transform.position.y, transform.position.z);
				}
			}
			if (Input.touchCount == 2 && Input.GetTouch (0).phase == TouchPhase.Moved) {
				// Store both touches.
				Touch touchZero = Input.GetTouch (0);
				Touch touchOne = Input.GetTouch (1);
				// Find the position in the previous frame of each touch.
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
				// Find the magnitude of the vector (the distance) between the touches in each frame.
				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
				// Find the difference in the distances between each frame.
				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
				// Otherwise change the field of view based on the change in distance between the touches.
				cameraComponent.fieldOfView += deltaMagnitudeDiff * Time.deltaTime * PerspectiveZoomSpeed;
				// Clamp the field of view to make sure it's between 0 and 180.
				cameraComponent.fieldOfView = Mathf.Clamp (cameraComponent.fieldOfView, 20f, 75f);
			}
		}

		private void HandleMouseInputs () {
			if (Input.GetMouseButton (0)) {
				if (Input.GetAxis ("Mouse X") != 0 || Input.GetAxis ("Mouse X") != 0) {
					transform.position -= new Vector3 (Input.GetAxisRaw ("Mouse X") * MouseSpeed, 0.0f, Input.GetAxisRaw ("Mouse Y") * MouseSpeed);
				}
				if (transform.position.z <= -70) {
					transform.position = new Vector3 (transform.position.x, transform.position.y, -70);
				}
				if (transform.position.z >= -20) {
					transform.position = new Vector3 (transform.position.x, transform.position.y, -20);
				}
				if (transform.position.x <= 0) {
					transform.position = new Vector3 (0, transform.position.y, transform.position.z);
				}
				if (transform.position.x >= 75) {
					transform.position = new Vector3 (75, transform.position.y, transform.position.z);
				}
			}

			float delta = Input.GetAxis ("Mouse ScrollWheel");
			if (delta != 0f) {
				cameraComponent.fieldOfView -= delta * Time.deltaTime * MouseWheelZoomSpeed;
				cameraComponent.fieldOfView = Mathf.Clamp (cameraComponent.fieldOfView, 20f, 75f);
			}
		}
	}

}