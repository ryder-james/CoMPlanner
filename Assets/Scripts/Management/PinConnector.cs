using UnityEngine;
using CasePlanner.Data.Notes;

namespace CasePlanner.UI {
	public class PinConnector : MonoBehaviour {
		[SerializeField] private GameObject edgeViewPrefab = null;
		[SerializeField] private Transform stringParent = null;

		public Pin A { get; set; }
		public Pin B { get; set; }

		private EdgeView edgeView;

		private void Update() {
			if (A != null && B == null) {
				if (!Input.GetMouseButton(0)) {
					Cancel();
					return;
				}
				if (edgeView == null) {
					edgeView = Instantiate(edgeViewPrefab, stringParent).GetComponent<EdgeView>();
				}

				edgeView.A = A.NoteView;
				edgeView.EndOverride = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				edgeView.gameObject.name = $"{A.NoteView.Note.ID}:";
			}
		}

		public void Connect() {
			if (edgeView == null) {
				edgeView = Instantiate(edgeViewPrefab, stringParent).GetComponent<EdgeView>();
			}

			edgeView.A = A.NoteView;
			edgeView.B = B.NoteView;
			edgeView.EndOverride = Vector3.zero;

			A.NoteView.Note.Connect(B.NoteView.Note.ID);
			B.NoteView.Note.Connect(A.NoteView.Note.ID);
			A.NoteView.ConnectEdge(ref edgeView);
			B.NoteView.ConnectEdge(ref edgeView);

			edgeView.gameObject.name = $"{A.NoteView.Note.ID}:{B.NoteView.Note.ID}";

			A = null;
			B = null;
			edgeView = null;
		}

		public void Cancel() {
			if (edgeView != null) {
				Destroy(edgeView.gameObject);
			}
			A = null;
			B = null;
		}
	}
}