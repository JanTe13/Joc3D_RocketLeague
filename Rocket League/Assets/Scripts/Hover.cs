using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {
	
	public float fMag = 50.0f;
	public float impulseMag = 3000.0f;
	public float turn = 7000.0f;
	public float forceMagnitude = 10000.0f;


	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().centerOfMass = new Vector3 (0.0f, -0.3f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 leftRear = transform.TransformPoint(new Vector3(-0.5f, -0.5f, -0.5f));
		Vector3 rightRear = transform.TransformPoint(new Vector3(0.5f, -0.5f, -0.5f));
		Vector3 leftFront = transform.TransformPoint(new Vector3(-0.5f, -0.5f, 0.5f));
		Vector3 rightFront = transform.TransformPoint(new Vector3(0.5f, -0.5f, 0.5f));
		
		RaycastHit hLeftRear, hRightRear, hLeftFront, hRightFront;

		Physics.Raycast(leftRear + 0.1f * transform.up, -transform.up, out hLeftRear);
		Physics.Raycast(rightRear + 0.1f * transform.up, -transform.up, out hRightRear);
		Physics.Raycast(leftFront + 0.1f * transform.up, -transform.up, out hLeftFront);
		Physics.Raycast(rightFront + 0.1f * transform.up, -transform.up, out hRightFront);
		
		Debug.DrawRay(leftRear, -transform.up, (leftRear.y < 1.1f)?Color.red:Color.black);
		Debug.DrawRay(rightRear, -transform.up,(rightRear.y < 1.1f)?Color.red:Color.black);
		Debug.DrawRay(leftFront, -transform.up, (leftFront.y < 1.1f)?Color.red:Color.black);
		Debug.DrawRay(rightFront, -transform.up, (rightFront.y < 1.1f)?Color.red:Color.black);
		
		// Suspension
		if(leftRear.y < 1.0f)
			GetComponent<Rigidbody>().AddForceAtPosition((1.0f - leftRear.y) * fMag * hLeftRear.normal, leftRear);
		if(rightRear.y < 1.0f)
			GetComponent<Rigidbody>().AddForceAtPosition((1.0f - rightRear.y) * fMag * hRightRear.normal, rightRear);
		if(leftFront.y < 1.0f)
			GetComponent<Rigidbody>().AddForceAtPosition((1.0f - leftFront.y) * fMag * hLeftFront.normal, leftFront);
		if(rightFront.y < 1.0f)
			GetComponent<Rigidbody>().AddForceAtPosition((1.0f - rightFront.y) * fMag * hRightFront.normal, rightFront);

		//Impulse
		if (leftRear.y < 3f && rightRear.y < 3f) {
			if (Input.GetAxis ("Vertical") == 1) {
				impulseMag = 5000.0f;
				GetComponent<Rigidbody> ().AddForceAtPosition (impulseMag * Input.GetAxis ("Vertical") * transform.forward, transform.position - 0.3f * transform.up);
			} else {
				impulseMag = 800;
				GetComponent<Rigidbody> ().AddForceAtPosition (impulseMag * Input.GetAxis ("Vertical") * transform.forward, transform.position - 0.3f * transform.up);
			}
		}
		else {
			impulseMag = 200;
			GetComponent<Rigidbody> ().AddForceAtPosition (impulseMag * Input.GetAxis ("Vertical") * transform.forward, transform.position - 0.3f * transform.up);
		}

		//Rotation
		GetComponent<Rigidbody>().AddTorque(transform.up * turn * Input.GetAxis("Horizontal"));

		//Traction
		GetComponent<Rigidbody>().AddForce(- Vector3.Dot(GetComponent<Rigidbody>().velocity, transform.right) * transform.right);

		//Jump
		if (leftRear.y < 1.1f) {
			if (Input.GetKey (KeyCode.Space))
				gameObject.GetComponent<Rigidbody> ().AddForceAtPosition (forceMagnitude * Vector3.up, transform.position);
		}
	}
}
