using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public Transform lookAt;



	public Vector3 offset1;
	public Vector3 offset2;
	public Vector3 offset3;

	private Vector3 offsetActual;
	private float currentY;

	private void Start() {
		offsetActual = offset1;
		currentY = 90.0f;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
  /*          if (y == 7.0f && currentY == 90.0f) {
                y = 40.0f;
                x = 50.0f;
                currentX = 20.0f;
            }
            else {
                y = 7.0f;
                x = 15.0f;
                currentX = 0.0f;
            }*/
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            /*if (currentY == 90.0f && y == 7.0f)
                currentY = -90.0f;
            else
                currentY = 90.0f;*/
			if (offsetActual == offset3) offsetActual = offset1;
			else offsetActual = offset3;
        } 
    }

    private void LateUpdate()
    {
		//Debug.Log (lookAt.rotation.y);
		if (offsetActual == offset1) {
			if (lookAt.rotation.y < 1 && lookAt.rotation.y >= 0) {
				if (lookAt.rotation.y == 0.99) {
				}
				else if (currentY == 90.0f) {
					Quaternion rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
					transform.position = lookAt.position + rotation * offsetActual;
				} else if (currentY == 0.0f) {
					Quaternion rotation2 = Quaternion.Euler (0.0f, 0.0f, 0.0f);
					transform.position = lookAt.position + rotation2 * offsetActual;  
					transform.rotation = Quaternion.Euler (lookAt.rotation.x, lookAt.rotation.y + 90, lookAt.rotation.z);
				}
			} else if (lookAt.rotation.y < 0 && lookAt.rotation.y > -1) {
				if (lookAt.rotation.y == -0.99) {}
			//	if (currentY == 90.0f) {
					Quaternion rotation2 = Quaternion.Euler (0.0f, 180.0f, 0.0f);
					transform.position = lookAt.position + rotation2 * offsetActual;  
					transform.rotation = Quaternion.Euler (lookAt.rotation.x, lookAt.rotation.y - 90, lookAt.rotation.z);
					currentY = 0.0f;
			//	} else if (currentY == 0.0f) {
					
			//	}
			} else if (lookAt.rotation.y == 0.99){
				
			
			} else if (lookAt.rotation.y == 0.99){


			}
		} /*else if (offsetActual == offset3) {
			Quaternion rotation = Quaternion.Euler (0.0f, 180.0f, 0.0f);
			transform.position = lookAt.position + rotation * offsetActual;
			transform.rotation = Quaternion.Euler (lookAt.rotation.x, lookAt.rotation.y - 90, lookAt.rotation.z);
			//transform.forward = lookAt.position - transform.position; 
		}*/

    } 
}