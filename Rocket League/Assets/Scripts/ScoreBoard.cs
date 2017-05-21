using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreBoard : MonoBehaviour {

	/*public Text Scoreboard;
	private GameObject ball;
	private int Bat_1_Score = 0;
	private int Bat_2_Score = 0;

	// Use this for initialization
	void Start () {
		ball = GameObject.Find ("ball");
		//Scoreboard.text = Bat_1_Score.ToString() + " - " + Bat_2_Score.ToString();
		Scoreboard.text = "Timer";
	}
	
	// Update is called once per frame
	void Update () {
		if (ball.transform.position.x >= -260.0f && (ball.transform.position.z <= -20.0f && ball.transform.position.z >= 20.0f))
			Bat_1_Score++;
		else if (ball.transform.position.x >= 240.0f && (ball.transform.position.z <= -20.0f && ball.transform.position.z >= 20.0f))
			Bat_2_Score++;

		Scoreboard.text = Bat_1_Score.ToString() + " - " + Bat_2_Score.ToString();
	}*/

	public int timeLeft = 300;
	public Text countdownText;

	void Start() {
		StartCoroutine ("LoseTime");
	}

	void Update() {
		if (timeLeft%60 < 10)
			countdownText.text = ((timeLeft/60) + ":0" + (timeLeft%60));
		else
			countdownText.text = ((timeLeft/60) + ":" + (timeLeft%60));
	
		if (timeLeft <= 0) {
			StopCoroutine ("LoseTime");
			countdownText.text = "Times Up!";
		}
	}

	IEnumerator LoseTime() {
		while (true) {
			yield return new WaitForSeconds (1);
			timeLeft--;
		}
	}
}
