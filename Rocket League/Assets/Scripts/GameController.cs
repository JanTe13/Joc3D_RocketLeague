using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {

    public GameObject ball;
    public GameObject car;
    public GameObject home;
    public GameObject guest;
    public GameObject scoreboard;
    private Quaternion rotationCar;

    // Use this for initialization
    void Start () {
        rotationCar = car.GetComponent<Rigidbody>().transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
        if (home.GetComponent<Explosion>().GetGoal()) {
            bool result = scoreboard.GetComponent<Scoreboard>().SetScore("home");
            if (result)
                home.GetComponent<Explosion>().SetGoal(false);
            else
                print("Error. Score is not updated");

            ball.SetActive(false);
        }
        if (guest.GetComponent<Explosion>().GetGoal()) {
            bool result = scoreboard.GetComponent<Scoreboard>().SetScore("guest");
            if (result)
                guest.GetComponent<Explosion>().SetGoal(false);
            else
                print("Error. Score is not updated");

            ball.SetActive(false);
        }

        //temps
        if (scoreboard.GetComponent<Scoreboard>().GetNewStart())
            RestartPositions();

        if (scoreboard.GetComponent<Scoreboard>().GetEndGame()) {
            ball.SetActive(false);
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
        scoreboard.GetComponent<Scoreboard>().SetGoalMsg("");
        scoreboard.GetComponent<Scoreboard>().SetMoreTime(6);
    }
}