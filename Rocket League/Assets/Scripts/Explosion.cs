using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	void Explode() {
		var exp = GetComponent<ParticleSystem>();
		exp.Play();
		GetComponent<MeshRenderer>().enabled = false;
		Destroy(gameObject,exp.duration);
	}

	// Grenade explodes on impact.
	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Goal") 
			Explode();
	}
}