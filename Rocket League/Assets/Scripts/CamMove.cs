// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden

using UnityEngine;
using System.Collections;

// Place the script in the Camera-Control group in the component menu
[AddComponentMenu("Camera-Control/Smooth Follow CSharp")]

public class CamMove : MonoBehaviour
{
    /*
    This camera smoothes out rotation around the y-axis and height.
    Horizontal Distance to the target is always fixed.

    There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.

    For every of those smoothed values we calculate the wanted value and the current value.
    Then we smooth it using the Lerp function.
    Then we apply the smoothed values to the transform's position.
    */

    // The target we are following
    public Transform target;
	public Transform sphere;
    // The distance in the x-z plane to the target
    public float distance;
    // the height we want the camera to be above the target
    public float height;
    // How much we 
    public float heightDamping;
    public float rotationDamping;
    public GameObject pointLight;
	public GameObject car;

	private bool CameraBall;
	private Vector3 offset;

	void Start() {
		CameraBall = false;
        pointLight.GetComponent<Light>().enabled = false;
		car = GameObject.Find("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!CameraBall)
            {
                CameraBall = true;
                pointLight.GetComponent<Light>().enabled = true;
            }
            else
            {
                CameraBall = false;
                pointLight.GetComponent<Light>().enabled = false;
            }
        }
    }

    void LateUpdate()
    {
		if (!Pared ()) {
			if (!CameraBall) {

				// Early out if we don't have a target
				if (!target)
					return;

				// Calculate the current rotation angles

				float wantedRotationAngle = target.eulerAngles.y;
				float wantedHeight = target.position.y + height;
				float currentRotationAngle = transform.eulerAngles.y;
				float currentHeight = transform.position.y;

				// Damp the rotation around the y-axis
				currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

				// Damp the height
				currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

				// Convert the angle into a rotation
				Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);


				// Set the position of the camera on the x-z plane to:
				// distance meters behind the target

				var v3T = sphere.position - target.position;
				//transform.position = sphere.position + v3T.normalized * distance;
				transform.position = target.position;
				transform.position -= currentRotation * Vector3.forward * distance + v3T.normalized * (distance - 14);

				// Set the height of the camera

				transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);

				// Always look at the target
				transform.LookAt (target);

			} else {

				/*
				var v3T = sphere.position - target.position;
				transform.position = target.position + v3T.normalized * (distance);
				transform.LookAt (sphere);*/

				transform.position = new Vector3 (-337, 110, -146);
				transform.rotation = Quaternion.Euler (30, 65, 0);

			}
		} else {
			if (!CameraBall) {
				transform.LookAt (target);
				if (target.position.x > 260) {
					offset = new Vector3 (-50, 20, 0);
				} else if (target.position.z < 0)
					offset = new Vector3 (50, 20, 20);
				else if (target.position.z > 0)
					offset = new Vector3 (50, 20, -20);
				transform.position = target.position + offset;
			} else {
				transform.position = new Vector3 (-337, 110, -146);
				transform.rotation = Quaternion.Euler (30, 65, 0);
			}
		}
	}

	private bool Pared() {
		if (target.position.y > 19)
			return true;
		else if (car.GetComponent<Car> ().Ground () && target.position.y > 2)
			return true;
		else
			return false;
	}

}
