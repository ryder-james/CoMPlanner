using UnityEngine;
using UnityEngine.UI;
using Common.UI;
using CasePlanner.Data.Notes;

namespace CasePlanner.UI {
	public class Pin : MonoBehaviour {
		[SerializeField] private Color[] colors = null;

		private int colorIndex = 0;
		private Image img;
		private PinConnector connector;
		private StickyNote note;
		private DraggableUI draggable;

		public StickyNote Note { get => note; set => note = value; }

		private void Start() {
			img = GetComponent<Image>();
			connector = GameObject.FindGameObjectWithTag("Connector").GetComponent<PinConnector>();
			Note = GetComponentInParent<StickyNote>();
			draggable = GetComponentInParent<DraggableUI>();
		}

		public void StartConnection() {
			connector.A = this;
			draggable.IsHeld = false;
		}

		public void EndConnection() {
			if (connector.A != null) {
				connector.B = this;
				connector.Connect();
			}
		}

		public void NextColor() {
			SetColor((colorIndex + 1) % colors.Length);
		}

		public void SetColor(int index) {
			if (colors != null) {
				colorIndex = index;
				img.color = colors[colorIndex];
			}
		}
	}
}