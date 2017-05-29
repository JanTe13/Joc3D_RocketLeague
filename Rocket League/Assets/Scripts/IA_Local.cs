using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Local : MonoBehaviour {

    public float maxTorque;
    public GameObject camMove, goal;
    private Vector3 posPilota, posGoalAtac;

    public float maxPitch;
    public Transform centerOfMass;
    public AudioSource ground;
    public AudioSource jump;

    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public Transform[] tireMeshes = new Transform[4];

    private Movement m;
    private Rigidbody rigidBody;
    private AudioSource audioSource;

    private void Start() {
        posGoalAtac = goal.GetComponent<Transform>().position;

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass = centerOfMass.localPosition;
        m = GameObject.FindGameObjectWithTag("Movement").GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        UpdateMeshesPositions();
    }

    void FixedUpdate() {
        GameObject pilota = GameObject.Find("ball");
        if (pilota != null)
            posPilota = pilota.GetComponent<Transform>().position;
        Vector3 posTeammate1 = GetComponent<Transform>().position;
        Vector3 puntImpacteXut = PuntImpacteXut(posPilota, posGoalAtac);
        float angleAPuntImpacte = AngleAPuntImpacte(puntImpacteXut, posTeammate1);
        if (!camMove.GetComponent<CamMove>().Pared())
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0.0f, angleAPuntImpacte, 0.0f);
            m.Accelerate(5000.0f, wheelColliders, 1);
        }
        else {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            m.Accelerate(1000.0f, wheelColliders, -1);
        }

    }

    private Vector3 PuntImpacteXut(Vector3 pilota, Vector3 porteria) {
        float primer, segon;
        float x = pilota.x - 5;
        Vector3 punt = Vector3.zero;
        if (pilota.x != porteria.x) {
            primer = (pilota.z - porteria.z) / (pilota.x - porteria.x);
            segon = (-(pilota.z - porteria.z) * porteria.x / (pilota.x - porteria.x) + porteria.z);
            punt.x = x; punt.y = pilota.y; punt.z = (primer * x + segon);
        }
        return punt;
    }

    private float AngleAPuntImpacte(Vector3 puntImpacteXut, Vector3 posTeammate1) {
        float a, b;
        if (puntImpacteXut.x >= posTeammate1.x)
            a = puntImpacteXut.x - posTeammate1.x;
        else
            a = posTeammate1.x - puntImpacteXut.x;
        if (puntImpacteXut.z >= posTeammate1.z)
            b = puntImpacteXut.z - posTeammate1.z;
        else
            b = posTeammate1.z - puntImpacteXut.z;
        if (puntImpacteXut.x > posTeammate1.x)
        {
            if (puntImpacteXut.z > posTeammate1.z)
                return 90.0f + (90.0f - (180.0f - (Mathf.Atan(a / b) * 180.0f / Mathf.PI)));
            else
                return 90.0f + (90.0f - (Mathf.Atan(a / b) * 180.0f / Mathf.PI));
        }
        else {
            if (puntImpacteXut.z > posTeammate1.z)
                return 90.0f + (90.0f - (270.0f - (Mathf.Atan(b / a) * 180.0f / Mathf.PI)));
            else
                return 90.0f + (90.0f - (270.0f + (Mathf.Atan(b / a) * 180.0f / Mathf.PI)));
        }
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