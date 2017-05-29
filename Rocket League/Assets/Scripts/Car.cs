using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	public float maxTorque;
	public float jumpHeight;
	public float maxBrake;
	public float maxPitch;
	public Transform centerOfMass;
	public AudioSource ground;
	public AudioSource jump;

	public WheelCollider[] wheelColliders = new WheelCollider[4];
	public Transform[] tireMeshes = new Transform[4];

	private Movement m;
	private Rigidbody rigidBody;
	private string state;
	private float t1;
	private float t2;
	private AudioSource audioSource;
	private bool jumpCar;
	// Use this for initialization
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.centerOfMass = centerOfMass.localPosition;
		m = GameObject.FindGameObjectWithTag("Movement").GetComponent<Movement> ();
		state = "static";
		audioSource = GetComponent<AudioSource> ();
		jumpCar = false;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMeshesPositions ();
		if (Input.GetKeyDown (KeyCode.Space) && m.Grounded (wheelColliders)) {
			t1 = Time.time;
			m.Jump (jumpHeight, rigidBody);
			jump.Play ();
			jumpCar = true;
		} else if (m.Grounded (wheelColliders) && jumpCar) {
			if (Time.time - t1 > 1) {
				jump.Stop ();
				t2 = Time.time; 
				ground.Play ();
				jumpCar = false;
			}
		} else { 
			if (Time.time - t2 > 1) {
				ground.Stop ();
			}
		}
		AccelerationAudio ();
	}



	void FixedUpdate () {
		float directionAngle = Input.GetAxis("Horizontal");
		float directionAcc = Input.GetAxis("Vertical");
		float steer;
		float rpm = Mathf.Abs(wheelColliders[0].rpm) / 60;
		if (rpm < 2000) steer = 8f; 
		else steer = 3f;
		m.Rotate (steer, wheelColliders, directionAngle);
		if (state == "static") {
			if (directionAcc == -1) {
				if (wheelColliders[0].rpm <= 0) {
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

	public bool Ground() {
		return m.Grounded (wheelColliders);
	}

	private void AccelerationAudio() {
		float rpm = Mathf.Abs(wheelColliders[0].rpm) / 60;
		if (rpm > 5000) audioSource.pitch = 1.60f;
		else audioSource.pitch = maxPitch + rpm/4000;
	}

	private void OnCollisionEnter(Collision collision) {
		if (!(wheelColliders [0].isGrounded || wheelColliders [1].isGrounded || wheelColliders [2].isGrounded || wheelColliders [3].isGrounded)) {
			rigidBody.AddForce (2,0,2);
		}
	}


}
