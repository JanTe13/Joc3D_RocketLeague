using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public ParticleSystem partsyst1 = null;

	void Explode() {
		partsyst1.Play();
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Ball") 
			Explode();
	}
}