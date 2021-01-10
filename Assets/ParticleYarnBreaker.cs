using CasePlanner.Data.Notes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleYarnBreaker : MonoBehaviour {
	private void OnParticleCollision(GameObject other) {
		Yarn yarn = other.GetComponentInParent<Yarn>();
		if (yarn != null) {
			yarn.A.Remove(yarn);
			yarn.B.Remove(yarn);
			Destroy(yarn.gameObject);
		}
	}
}
