using CasePlanner.Data.Notes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEdgeBreaker : MonoBehaviour {
	private void OnParticleCollision(GameObject other) {
		EdgeView edge = other.GetComponentInParent<EdgeView>();
		if (edge != null) {
			edge.A.DisconnectEdge(edge);
			edge.B.DisconnectEdge(edge);
			Destroy(edge.gameObject);
		}
	}
}
