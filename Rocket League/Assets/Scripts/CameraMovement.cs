using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform lookAt;
    public Transform camTransform;

    private float distance = 0.0f;
    private float x = 15.0f;
    private float y = 7.0f;
    private float currentX = 0.0f;
    private float currentY = 90.0f;

    [SerializeField]
    private Transform target;

    private void Start() {
        camTransform = transform;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            if (y == 7.0f && currentY == 90.0f) {
                y = 40.0f;
                x = 50.0f;
                currentX = 20.0f;
            }
            else {
                y = 7.0f;
                x = 15.0f;
                currentX = 0.0f;
            }
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            if (currentY == 90.0f && y == 7.0f)
                currentY = -90.0f;
            else
                currentY = 90.0f;
        }
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(-x, y, -distance);
        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.rotation = Quaternion.Euler(currentX, currentY, 0.0f);
        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}