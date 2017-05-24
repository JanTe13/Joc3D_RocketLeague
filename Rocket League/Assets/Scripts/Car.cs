using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	public float maxTorque;
	public float jumpHeight;
	public float maxBrake;
	public Transform centerOfMass;

	public WheelCollider[] wheelColliders = new WheelCollider[4];
	public Transform[] tireMeshes = new Transform[4];

	private Movement m;
	private Rigidbody rigidBody;
	private string state;
	private float t;

	// Use this for initialization
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.centerOfMass = centerOfMass.localPosition;
		m = GameObject.FindGameObjectWithTag("Movement").GetComponent<Movement> ();
		state = "static";
		t = Time.time - 2.5f;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMeshesPositions();
		if (Input.GetKeyDown (KeyCode.Space) && m.Grounded(wheelColliders))
  			m.Jump (jumpHeight,rigidBody);
	}

	void FixedUpdate () {
		float directionAngle = Input.GetAxis("Horizontal");
		float directionAcc = Input.GetAxis("Vertical");
		float steer = 5f;
		m.Rotate (steer, wheelColliders, directionAngle);
		if (state == "static") {
			if (directionAcc == -1) {
				if (Time.time - 2.5 >= t) {
					maxBrake = 0;
					maxTorque /= 2;
					state = "backward";
				}
			} else if (directionAcc == 1) {
				maxBrake = 0;
				maxTorque *= 2;
				state = "forward";
			}
		} else if (state == "backward") {
			if (directionAcc != -1 && directionAcc != 1) {
				maxBrake = 10000;
				maxTorque *= 2;
				state = "static";
			}
		} else {
			if (directionAcc != -1 && directionAcc != 1) {
				maxBrake = 80000;
				maxTorque /= 2;
				state = "static";
				t = Time.time;
			}
		}
		m.Accelerate (maxTorque,wheelColliders,directionAcc);
		m.Brake (maxBrake,wheelColliders);

	}
		
	private void UpdateMeshesPositions() {
		for (int i = 0; i < 4; ++i) {
			Quaternion quat;
			Vector3 pos;
			wheelColliders [i].GetWorldPose (out pos, out quat);
			tireMeshes [i].position = pos;
			tireMeshes [i].rotation = quat;
		}
	}

	public void Jump() {
		m.Jump (jumpHeight,rigidBody);
	}


}
