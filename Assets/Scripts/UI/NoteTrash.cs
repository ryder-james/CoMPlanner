using Common.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteTrash : MonoBehaviour {
	[SerializeField] private Image img;
	[SerializeField] private NoteManager noteManager;

	private bool trashEnabled = false;

	public void Enter(BaseEventData eventData) {
		if (eventData.selectedObject == null) {
			return;
		}

		DraggableUI heldUI = eventData.selectedObject.GetComponent<DraggableUI>();
		if (heldUI != null && heldUI.IsHeld) {
			trashEnabled = true;
			img.color = new Color(img.color.r, img.color.g, img.color.b);
		}
	}

	public void Drop(BaseEventData eventData) {
		if (eventData.selectedObject == null) {
			return;
		}

		if (trashEnabled && eventData.selectedObject.TryGetComponent(out NoteView view)) {
			noteManager.DestroyNote(view);
		}
	}

	public void Exit() {
		trashEnabled = false;
		img.color = new Color(img.color.r, img.color.g, img.color.b, 0.53f);
	}
}
