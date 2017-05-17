using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	public float maxTorque;
	public Transform centerOfMass;

	public WheelCollider[] wheelColliders = new WheelCollider[4];
	public Transform[] tireMeshes = new Transform[4];

	private Rigidbody rigidBody;

	// Use this for initialization
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.centerOfMass = centerOfMass.localPosition;

	}
	
	// Update is called once per frame
	void Update () {
		UpdateMeshesPositions();
	}

	void FixedUpdate () {
		float steer = Input.GetAxis("Horizontal");
		float accelerate = Input.GetAxis("Vertical");

		float finalAngle = steer * 5f;
		wheelColliders[0].steerAngle = finalAngle;
		wheelColliders[1].steerAngle = finalAngle;

		for (int i = 0; i < 4; ++i) {
			wheelColliders[i].motorTorque = accelerate * maxTorque;
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


}
