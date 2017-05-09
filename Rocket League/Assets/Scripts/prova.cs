using UnityEngine;
using System.Collections;

public class prova : MonoBehaviour {

	public float speed = 10f;
	public float jumpForce = 8f;
	public float gravity = 30f;
	private Vector3 moveDir = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = gameObject.GetComponent<CharacterController> ();

		if (controller.isGrounded) {
			moveDir = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			moveDir = transform.TransformDirection (moveDir);
			moveDir *= speed;

			if (Input.GetKey (KeyCode.Space)) {
				moveDir.y = jumpForce;
			}
		}

		moveDir.y -= gravity * Time.deltaTime;

		controller.Move (moveDir * Time.deltaTime);
	}
}
