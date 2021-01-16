using UnityEngine;

using JCommon.UI;

using CasePlanner.Management;

namespace CasePlanner.Data.Views {
	public class Pin : MonoBehaviour {
		private NoteView noteView;
		private DraggableUI draggable;

		public PinConnector PinConnector { get; set; }
		public NoteView NoteView {
			get => noteView;
			set {
				noteView = value;
				draggable = noteView.GetComponent<DraggableUI>();
			}
		}

		private void Awake() {
			PinConnector = FindObjectOfType<PinConnector>();
			NoteView = GetComponentInParent<NoteView>();
		}

		public void BeginConnection() {
			PinConnector.A = this;
			draggable.IsHeld = false;
		}

		public void EndConnection() {
			if (PinConnector.A != null) {
				PinConnector.B = this;
				PinConnector.Connect();
			}
		}
	}
}