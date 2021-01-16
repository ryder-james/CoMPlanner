using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class EdgeCutter : MonoBehaviour {
	[SerializeField] private ParticleSystem cutterParticles = null;

	private bool isCutting = false;
	private Rigidbody2D rb;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		if (isCutting) {
			Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			target.z = 0;
			rb.velocity = (target - rb.transform.position) * 10;
			cutterParticles.transform.position = rb.transform.position;
		} else {
			rb.velocity = Vector3.zero;
		}
	}

	public void StartBreak(BaseEventData baseEventData) {
		var eventData = baseEventData as PointerEventData;
		if (eventData.button == PointerEventData.InputButton.Right) {
			isCutting = true;
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			rb.transform.position = new Vector3(pos.x, pos.y);
			cutterParticles.Clear();
			cutterParticles.Play();
		}
	}

	public void StopBreak(BaseEventData baseEventData) {
		var eventData = baseEventData as PointerEventData;
		if (eventData.button == PointerEventData.InputButton.Right) {
			isCutting = false;
			cutterParticles.Stop();
		}
	}
}
