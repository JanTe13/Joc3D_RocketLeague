using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Explosion : MonoBehaviour {

	public ParticleSystem partsyst1 = null;
	public Text Guest;
	public Text Home;
	public float force = 30000.0f;
	public float radius = 200.0f;
	public float upwardsModifier = 0.0f;
	public ForceMode forceMode;

	void Start() {
		Guest.text = "0";
		Home.text = "0";
	}

	void Explode() {
		partsyst1.Play ();
		foreach (Collider col in Physics.OverlapSphere(transform.position, radius)) {
			Rigidbody rb = col.GetComponent<Rigidbody>();
			if (rb != null)
				rb.AddExplosionForce(force, transform.position, radius, upwardsModifier, forceMode);
		}
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Ball") {
			Explode ();
			if (partsyst1.name == "fire_goal2") {
				int score = int.Parse(Guest.text);
				Guest.text = (score + 1).ToString();
			}
			else if (partsyst1.name == "fire_goal1") {
				int score = int.Parse(Home.text);
				Home.text = (score + 1).ToString();
			}
		}		
	}
}