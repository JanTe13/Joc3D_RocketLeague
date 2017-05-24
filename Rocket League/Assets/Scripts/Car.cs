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

	private Rigidbody rigidBody;
	private string state;
	private float t;

	// Use this for initialization
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.centerOfMass = centerOfMass.localPosition;
		state = "static";
		t = Time.time - 2.5f;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMeshesPositions();
        if (Input.GetKeyDown(KeyCode.Space) && Grounded())
            Jump();
	}

	void FixedUpdate () {
		float steer = Input.GetAxis("Horizontal");
		float accelerate = Input.GetAxis("Vertical");

		float finalAngle = steer * 5f;
		wheelColliders[0].steerAngle = finalAngle;
		wheelColliders[1].steerAngle = finalAngle;

		if (state == "static") {
			if (accelerate == -1) {
				if (Time.time - 2.5 >= t) {
					maxBrake = 0;
					maxTorque /= 2;
					state = "backward";
				}
			} else if (accelerate == 1) {
				maxBrake = 0;
				maxTorque *= 2;
				state = "forward";
			}
		} else if (state == "backward") {
			 if (accelerate != -1 && accelerate != 1) {
				maxBrake = 10000;
				maxTorque *= 2;
				state = "static";
			}
		} else {
			if (accelerate != -1 && accelerate != 1) {
				maxBrake = 80000;
				maxTorque /= 2;
				state = "static";
				t = Time.time;
			}
		}
		for (int i = 0; i < 4; ++i) {
			wheelColliders [i].motorTorque = accelerate * maxTorque;
			wheelColliders [i].brakeTorque = maxBrake;
		}

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
		rigidBody.AddForce (Vector3.up * jumpHeight,ForceMode.Acceleration);
	}

	private bool Grounded() {
		return (wheelColliders [0].isGrounded && wheelColliders [1].isGrounded && wheelColliders [2].isGrounded && wheelColliders [3].isGrounded);
	}


}
