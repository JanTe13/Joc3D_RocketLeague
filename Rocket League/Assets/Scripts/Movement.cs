using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {


	public void Accelerate (float acc, WheelCollider[] wc, float direction) {
		for (int i = 0; i < 4; ++i) {
			wc [i].motorTorque = direction * acc;
		}
	}

	public void Brake (float br, WheelCollider[] wc) {
		for (int i = 0; i < 4; ++i) {
			wc [i].brakeTorque = br;
		}
	}

	public void Jump (float jmp, Rigidbody rb) {
		rb.AddForce (Vector3.up * jmp,ForceMode.Acceleration);
	}

	public void Rotate (float rt, WheelCollider[] wc, float direction) {
		float finalAngle = direction * rt;
		wc[0].steerAngle = finalAngle;
		wc[1].steerAngle = finalAngle;
	}

	public bool Grounded(WheelCollider[] wc) {
		return (wc [0].isGrounded && wc [1].isGrounded && wc [2].isGrounded && wc [3].isGrounded);
	}
}
