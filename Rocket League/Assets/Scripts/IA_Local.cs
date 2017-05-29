using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Local : MonoBehaviour {

    public GameObject teammate1, teammate2;
    private Vector3 posPilota, posGoalAtac, posGoalDefensa;

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
    private AudioSource audioSource;
    private bool jumpCar;

    private void Start() {
        posGoalAtac = GameObject.Find("goal2").GetComponent<Transform>().position;
        posGoalDefensa = GameObject.Find("goal1").GetComponent<Transform>().position;

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass = centerOfMass.localPosition;
        m = GameObject.FindGameObjectWithTag("Movement").GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
        jumpCar = false;
    }

    void Update() {
        UpdateMeshesPositions();
    }

    void FixedUpdate() {
        //posPilota = GameObject.Find("ball").GetComponent<Transform>().position;
        Vector3 posTeammate1 = teammate1.GetComponent<Transform>().position;
        float carRotY = teammate1.GetComponent<Transform>().rotation.eulerAngles.y;
        
        Vector3 puntImpacte = PuntImpacte(posPilota, posGoalAtac);
    }

    private Vector3 PuntImpacte(Vector3 pilota, Vector3 porteria) {
        float primer, segon;
        float x = porteria.z + 5;
        Vector3 punt = Vector3.zero;
        if (pilota.x != porteria.x) {
            primer = (pilota.z - porteria.z) / (pilota.x - porteria.x);
            segon = (-(pilota.z - porteria.z) * porteria.x / (pilota.x - porteria.x) + porteria.z);
            punt.x = x; punt.y = pilota.y; punt.z = (primer * x + segon);
        }
        return punt;
    }

    private void UpdateMeshesPositions()
    {
        for (int i = 0; i < 4; ++i)
        {
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);
            tireMeshes[i].position = pos;
            tireMeshes[i].rotation = quat;
        }
    }

    private void AccelerationAudio()
    {
        float rpm = Mathf.Abs(wheelColliders[0].rpm) / 60;

        if (rpm > 5000) audioSource.pitch = 1.65f;
        else audioSource.pitch = maxPitch + rpm / 4000;
    }
}
