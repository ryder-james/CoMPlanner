using UnityEngine;
using CasePlanner.Data.Notes;

namespace CasePlanner.UI {
	public class PinConnector_Legacy : MonoBehaviour {
		[SerializeField] private GameObject stringBase = null;
		[SerializeField] private RectTransform stringParent = null;

		public Pin_Legacy A { get; set; }
		public Pin_Legacy B { get; set; }

		private Yarn_Legacy yarn;

		private void Update() {
			if (A != null && B == null) {
				if (!Input.GetMouseButton(0)) {
					Cancel();
					return;
				}
				if (yarn == null) {
					yarn = Instantiate(stringBase, stringParent).GetComponent<Yarn_Legacy>();
				}

				yarn.PointA = A.transform;
				yarn.EndOverride = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				yarn.gameObject.name = $"{A.Note.ID}:";
			}
		}

		public void Connect() {
			if (yarn == null) {
				yarn = Instantiate(stringBase, stringParent).GetComponent<Yarn_Legacy>();
			}

			yarn.PointA = A.transform;
			yarn.PointB = B.transform;
			yarn.EndOverride = Vector3.zero;

			yarn.A = A.Note;
			yarn.B = B.Note;
			A.Note.Connect(yarn);
			B.Note.Connect(yarn);

			yarn.gameObject.name = $"{A.Note.ID}:{B.Note.ID}";

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