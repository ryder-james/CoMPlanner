using UnityEngine;
using CasePlanner.Data.Notes;

namespace CasePlanner.UI {
	public class PinConnector : MonoBehaviour {
		[SerializeField] private GameObject edgeViewPrefab = null;
		[SerializeField] private Transform stringParent = null;

		public Pin A { get; set; }
		public Pin B { get; set; }

		private EdgeView yarn;

		private void Update() {
			if (A != null && B == null) {
				if (!Input.GetMouseButton(0)) {
					Cancel();
					return;
				}
				if (yarn == null) {
					yarn = Instantiate(edgeViewPrefab, stringParent).GetComponent<EdgeView>();
				}

				yarn.PointA = A.transform;
				yarn.EndOverride = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				yarn.gameObject.name = $"{A.NoteView.Note.ID}:";
			}
		}

		public void Connect() {
			if (yarn == null) {
				yarn = Instantiate(edgeViewPrefab, stringParent).GetComponent<EdgeView>();
			}

			yarn.PointA = A.transform;
			yarn.PointB = B.transform;
			yarn.EndOverride = Vector3.zero;

			A.NoteView.Note.Connect(B.NoteView.Note.ID);
			B.NoteView.Note.Connect(A.NoteView.Note.ID);

			yarn.gameObject.name = $"{A.NoteView.Note.ID}:{B.NoteView.Note.ID}";

			A = null;
			B = null;
			yarn = null;
		}

		public void Cancel() {
			if (yarn != null) {
				Destroy(yarn.gameObject);
			}
			A = null;
			B = null;
		}
	}
}