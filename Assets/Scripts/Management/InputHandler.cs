using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {
	private Rigidbody heldNote = null;
	private Vector3 holdOffset = Vector3.zero;

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) {
				if (hit.collider.tag == "Note") {
					heldNote = hit.collider.gameObject.GetComponent<Rigidbody>();
					holdOffset = heldNote.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
					holdOffset.z = 0;
				}
			}
		}

		if (Input.GetMouseButtonUp(0)) {
			if (heldNote != null) {
				heldNote.velocity = Vector3.zero;
			}

			heldNote = null;
		}
	}

	private void FixedUpdate() {
		if (heldNote != null) {
			Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition) + holdOffset;
			target.z = 0;

			heldNote.velocity = (target - heldNote.transform.position) * 20;
		}
	}
}
