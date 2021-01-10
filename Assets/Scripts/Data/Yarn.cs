using UnityEngine;

namespace CasePlanner.Data.Notes {
	[System.Serializable]
	public class Yarn : MonoBehaviour, ISerializable<Yarn.Edge> {
		[System.Serializable]
		public class Edge {
			public int a, b;

			public static bool operator ==(Edge a, Edge b) {
				return !(a is null) ? a.Equals(b) : b is null;
			}

			public static bool operator !=(Edge a, Edge b) {
				return !(a is null) ? !a.Equals(b) : !(b is null);
			}

			public override bool Equals(object obj) {
				if (obj is Edge) {
					return a == (obj as Edge).a && b == (obj as Edge).b;
				} else {
					return false;
				}
			}

			public override int GetHashCode() {
				var hashCode = 2118541809;
				hashCode = hashCode * -1521134295 + a.GetHashCode();
				hashCode = hashCode * -1521134295 + b.GetHashCode();
				return hashCode;
			}
		}

		[SerializeField]
		private GameObject lineColliderBase = null;

		public StickyNote A { get => a; set => a = value; }
		public StickyNote B { get => b; set => b = value; }
		public Transform PointA { get => pointA; set => pointA = value; }
		public Transform PointB { get => pointB; set => pointB = value; }
		public Vector3 EndOverride { get; set; }

		[SerializeField, HideInInspector]
		private LineRenderer line;

		[SerializeField, HideInInspector]
		private StickyNote a, b;

		[SerializeField, HideInInspector]
		private Transform pointA, pointB;

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
			Vector3 aPos = PointA.position;
			Vector3 bPos = EndOverride == Vector3.zero ? PointB.position : EndOverride;
			aPos.z = 0;
			bPos.z = 0;
			UpdateCollider(aPos, bPos);
		}

		private void LateUpdate() {
			Vector3 aPos = PointA.position;
			Vector3 bPos = EndOverride == Vector3.zero ? PointB.position : EndOverride;
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

		public Edge Serialized() {
			Edge edge = new Edge() {
				a = A.ID,
				b = B.ID
			};

			return edge;
		}

		public void Deserialize(Edge obj) {

		}
	}
}