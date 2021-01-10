using UnityEngine;
using UnityEngine.UI;

using Common.UI;
using UnityEngine.EventSystems;

namespace CasePlanner.UI {
	public class Trash_Legacy : MonoBehaviour {
		private Image img;
		private bool trashEnabled = false;

		private void Start() {
			img = GetComponent<Image>();
		}

		public void Enter(BaseEventData eventData) {
			DraggableUI heldUI = eventData.selectedObject.GetComponent<DraggableUI>();
			if (heldUI != null && heldUI.IsHeld) {
				trashEnabled = true;
				img.color = new Color(img.color.r, img.color.g, img.color.b);
			}
		}

		public void Drop(BaseEventData eventData) {
			if (trashEnabled) {
				Destroy(eventData.selectedObject);
			}
		}

		public void Exit() {
			trashEnabled = false;
			img.color = new Color(img.color.r, img.color.g, img.color.b, 0.53f);
		}
	}
}
