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
    public Material mat1, mat2, mat3;
    private Quaternion rotationCar;

    private GameObject teammate1;
    private GameObject teammate2;
    private GameObject enemy1;
    private GameObject enemy2;
    private GameObject enemy3;

    // Use this for initialization
    void Start() {
        //Inicialització cotxes
        teammate1 = GameObject.Find("Teammate_1");
        teammate2 = GameObject.Find("Teammate_2");
        enemy1 = GameObject.Find("Enemy_1");
        enemy2 = GameObject.Find("Enemy_2");
        enemy3 = GameObject.Find("Enemy_3");

        int model = PlayerPrefs.GetInt("carElection");
        int level = PlayerPrefs.GetInt("level");
        if (model == 1) {
            car.GetComponentInChildren<MeshRenderer>().material = mat1;
            teammate1.GetComponentInChildren<MeshRenderer>().material = mat1;
            teammate2.GetComponentInChildren<MeshRenderer>().material = mat1;
        }
        else if (model == 2) {
            car.GetComponentInChildren<MeshRenderer>().material = mat2;
            teammate1.GetComponentInChildren<MeshRenderer>().material = mat2;
            teammate2.GetComponentInChildren<MeshRenderer>().material = mat2;
        }
        else if (model == 3) {
            car.GetComponentInChildren<MeshRenderer>().material = mat3;
            teammate1.GetComponentInChildren<MeshRenderer>().material = mat3;
            teammate2.GetComponentInChildren<MeshRenderer>().material = mat3;
        }
        int model_rivals = Random.Range(1, 3);
        while (model_rivals == model)
            model_rivals = Random.Range(1, 3);
        SetModelRivals(model_rivals);
        //Inicialització cotxes
        teammate1 = GameObject.Find("Teammate_1");
        teammate2 = GameObject.Find("Teammate_2");
        enemy1 = GameObject.Find("Enemy_1");
        enemy2 = GameObject.Find("Enemy_2");
        enemy3 = GameObject.Find("Enemy_3");

        teammate1.SetActive(2 <= level);
        teammate2.SetActive(3 <= level);
        enemy1.SetActive(1 <= level);
        enemy2.SetActive(2 <= level);
        enemy3.SetActive(3 <= level);

        rotationCar = car.GetComponent<Rigidbody>().transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
        if (home.GetComponent<Explosion>().GetGoal()) {
            if (Mathf.Abs(car.GetComponent<Transform>().position.x - home.GetComponent<Transform>().position.x) < 150)
                car.GetComponent<Car>().Jump();
            bool result = scoreboard.GetComponent<Scoreboard>().SetScore("home");
            if (result)
                home.GetComponent<Explosion>().SetGoal(false);
            else
                print("Error. Score is not updated");

            ball.SetActive(false);
        }
        if (guest.GetComponent<Explosion>().GetGoal()) {
            if (Mathf.Abs(car.GetComponent<Transform>().position.x - guest.GetComponent<Transform>().position.x) < 150)
                car.GetComponent<Car>().Jump();
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

        if (scoreboard.GetComponent<Scoreboard>().GetCarStoped()) {
            car.GetComponent<Car>().enabled = false;
            ball.GetComponent<Rigidbody>().useGravity = false;
        }
        else {
            car.GetComponent<Car>().enabled = true;
            ball.GetComponent<Rigidbody>().useGravity = true;
        }

    }

    private void RestartPositions() { 
        Vector3 positionBall = new Vector3 ( -6.59f, 7.8f, -1.9f );
        Vector3 positionCar = new Vector3(-245.5f, 0.5f, -1.9f);
        ball.GetComponent<Rigidbody>().transform.SetPositionAndRotation(positionBall, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
        car.GetComponent<Rigidbody>().transform.SetPositionAndRotation(positionCar, rotationCar);
        ball.SetActive(true);
        scoreboard.GetComponent<Scoreboard>().SetTimePaused(false);
        scoreboard.GetComponent<Scoreboard>().SetGoalMsg("");
        scoreboard.GetComponent<Scoreboard>().SetMoreTime(9);
    }

    private void SetModelRivals(int model) {
        if (model == 1) {
            enemy1.GetComponentInChildren<MeshRenderer>().material = mat1;
            enemy2.GetComponentInChildren<MeshRenderer>().material = mat1;
            enemy3.GetComponentInChildren<MeshRenderer>().material = mat1;
        }
        else if (model == 2) {
            enemy1.GetComponentInChildren<MeshRenderer>().material = mat2;
            enemy2.GetComponentInChildren<MeshRenderer>().material = mat2;
            enemy3.GetComponentInChildren<MeshRenderer>().material = mat2;
        }
        else if (model == 3) {
            enemy1.GetComponentInChildren<MeshRenderer>().material = mat3;
            enemy2.GetComponentInChildren<MeshRenderer>().material = mat3;
            enemy3.GetComponentInChildren<MeshRenderer>().material = mat3;
        }
    }
}