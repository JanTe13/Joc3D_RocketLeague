using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public ParticleSystem partsyst1 = null;
	public float force = 30000.0f;
	public float radius = 200.0f;
	public float upwardsModifier = 0.0f;
	public ForceMode forceMode;

	void Explode() {
		partsyst1.Play ();
		foreach (Collider col in Physics.OverlapSphere(transform.position, radius)) {
			Rigidbody rb = col.GetComponent<Rigidbody>();
			if (rb != null)
				rb.AddExplosionForce(force, transform.position, radius, upwardsModifier, forceMode);
		}
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Ball")
			Explode();
	}
}