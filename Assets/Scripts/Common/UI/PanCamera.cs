using UnityEngine;

namespace JCommon.UI {
	[System.Serializable, RequireComponent(typeof(Camera), typeof(Rigidbody2D), typeof(BoxCollider2D))]
	public class PanCamera : MonoBehaviour {
		[SerializeField]
		private Vector2 aspectRatio = new Vector2(16, 9);

		[SerializeField, Range(30, 150)]
		private float panSpeed = 200;

		[SerializeField, Range(20, 200)]
		private float scrollSpeed = 200;

		public float Size { 
			get => cam.orthographicSize;
			set => cam.orthographicSize = value;
		}

		public float BaseSize { get; private set; }

		public bool CamEnabled { get; set; } = true;

		private Camera cam;
		private Rigidbody2D rb;
		private BoxCollider2D frustrumCollider;

		private Vector3 lastMousePosition;

		private void Awake() {
			cam = GetComponent<Camera>();
			rb = GetComponent<Rigidbody2D>();
			frustrumCollider = GetComponent<BoxCollider2D>();

			lastMousePosition = Vector3.zero;
			BaseSize = Size;
		}

		private void Update() {
			if (!CamEnabled)
				return;

			cam.orthographicSize -= Input.mouseScrollDelta.y 
				* scrollSpeed 
				* Mathf.Max(PlayerPrefs.GetFloat("ScrollSpeed", 0.5f), 0.05f) 
				* 100 
				* Time.deltaTime;

			cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 300, 800);
			float size = cam.orthographicSize * 2;

			frustrumCollider.size = new Vector2(size * (aspectRatio.x / aspectRatio.y), size);
		}

		private void FixedUpdate() {
			if (!CamEnabled)
				return;

			if (Input.GetMouseButton(2)) {
				Vector3 cursorDelta = Input.mousePosition - lastMousePosition;

				rb.drag = 0;
				rb.velocity = -cursorDelta 
					* panSpeed 
					* Mathf.Max(PlayerPrefs.GetFloat("PanSpeed", 0.5f), 0.05f);
			} else {
				rb.drag = 10;
			}

			lastMousePosition = Input.mousePosition;
		}
	}
}
