using UnityEngine;
using CasePlanner.Data.Notes;

namespace CasePlanner.UI {
	public class PinConnector : MonoBehaviour {
		[SerializeField] private GameObject stringBase = null;

		public Pin A { get; set; }
		public Pin B { get; set; }

		private Yarn yarn;

		private void Update() {
			if (A != null && B == null) {
				if (!Input.GetMouseButton(0)) {
					Cancel();
					return;
				}
				if (yarn == null) {
					yarn = Instantiate(stringBase, transform.parent).GetComponent<Yarn>();
				}

				yarn.PointA = A.transform;
				yarn.EndOverride = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
		}

		public void Connect() {
			if (yarn == null) {
				yarn = Instantiate(stringBase, transform.parent).GetComponent<Yarn>();
			}

			yarn.PointA = A.transform;
			yarn.PointB = B.transform;
			yarn.EndOverride = Vector3.zero;

			yarn.A = A.Note;
			yarn.B = B.Note;
			A.Note.Connect(yarn);
			B.Note.Connect(yarn);

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