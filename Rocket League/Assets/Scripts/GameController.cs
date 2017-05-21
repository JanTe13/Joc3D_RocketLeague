using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject ball;
    public GameObject car;
    public GameObject home;
    public GameObject guest;
    public GameObject scoreboard;
    private Quaternion rotationCar;

    public float time_max;
    private float time;

    // Use this for initialization
    void Start () {
        rotationCar = car.GetComponent<Rigidbody>().transform.rotation;
        time = time_max;
    }
	
	// Update is called once per frame
	void Update () {
		if (home.GetComponent<Explosion>().GetGoal()) {
            time -= Time.deltaTime;
            bool result = scoreboard.GetComponent<Scoreboard>().SetScore("home");
            if (result)
                home.GetComponent<Explosion>().SetGoal(false);
            else
                print("Error. Score is not updated");

            ball.SetActive(false);
            //temps
            RestartPositions();
        }
        if (guest.GetComponent<Explosion>().GetGoal()) {
            time -= time/5;
            bool result = scoreboard.GetComponent<Scoreboard>().SetScore("guest");
            if (result)
                guest.GetComponent<Explosion>().SetGoal(false);
            else
                print("Error. Score is not updated");

            ball.SetActive(false);
            //temps
            RestartPositions();
        }

    }

    private void RestartPositions() {

        /////////////////////////////////////////////
        ////Afegir temps abans de començar de nou////
        /////////////////////////////////////////////

        Vector3 positionBall = new Vector3 ( -6.59f, 7.8f, -1.9f );
        Vector3 positionCar = new Vector3(-34.6f, 0.5f, -1.9f);
        ball.GetComponent<Rigidbody>().transform.SetPositionAndRotation(positionBall, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
        car.GetComponent<Rigidbody>().transform.SetPositionAndRotation(positionCar, rotationCar);
        ball.SetActive(true);
        scoreboard.GetComponent<Scoreboard>().SetTimePaused(false);
    }
}
