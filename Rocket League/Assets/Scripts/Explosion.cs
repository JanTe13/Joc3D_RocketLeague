using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Explosion : MonoBehaviour {

	public ParticleSystem partsyst1 = null;
    private bool goal;
	
	public float force = 30000.0f;
	public float radius = 200.0f;
	public float upwardsModifier = 0.0f;
	public ForceMode forceMode;

	void Start() {
        goal = false;
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Explode();
            goal = true;
        }
    }

    void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Ball") {
            Explode ();
            goal = true;
		}		
	}

    void Explode()
    {
        partsyst1.Play();
        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(force, transform.position, radius, upwardsModifier, forceMode);
        }
    }

    public bool GetGoal() {
        return goal;
    }

    public void SetGoal(bool isGoal) {
        goal = isGoal;
    }

    
}