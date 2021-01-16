using UnityEngine;

namespace CasePlanner.Data.Notes {
	public class EdgeView : MonoBehaviour {
		[SerializeField] private GameObject lineColliderBase = null;

		public NoteView A { get; set; }
		public NoteView B { get; set; }

		public Vector3 EndOverride { get; set; }

		private LineRenderer line;

		private BoxCollider2D boxCollider;

		private void Start() {
			line = GetComponent<LineRenderer>();
			CreateCollider();
		}

		private void CreateCollider() {
			boxCollider = Instantiate(lineColliderBase, transform).GetComponent<BoxCollider2D>();
		}

		private void UpdateCollider(Vector3 start, Vector3 end) {
			Vector3 mid = (start + end) * 0.5f;
			float angle = CalculateAngle(start, end);

			boxCollider.size = new Vector3(Vector3.Distance(start, end), line.startWidth, line.endWidth);

			boxCollider.transform.rotation = Quaternion.identity;
			boxCollider.transform.Rotate(0, 0, angle);

			boxCollider.transform.position = new Vector3(mid.x, mid.y);
		}

		private void Update() {
			Vector3 aPos = A.Pin.transform.position;
			Vector3 bPos = EndOverride == Vector3.zero ? B.Pin.transform.position : EndOverride;
			aPos.z = 0;
			bPos.z = 0;
			UpdateCollider(aPos, bPos);
		}

		private void LateUpdate() {
			Vector3 aPos = A.Pin.transform.position;
			Vector3 bPos = EndOverride == Vector3.zero ? B.Pin.transform.position : EndOverride;
			aPos.z = 0;
			bPos.z = 0;
			line.SetPositions(new Vector3[] { aPos, bPos });
		}

		private static float CalculateAngle(Vector3 startPos, Vector3 endPos) {
			float angle = Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x);

			if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x)) {
				angle = -angle;
			}

			angle = Mathf.Rad2Deg * Mathf.Atan(angle);
			return angle;
		}
	}
}