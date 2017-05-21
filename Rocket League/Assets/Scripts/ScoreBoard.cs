using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scoreboard : MonoBehaviour {

	public int timeLeft = 300;
	public Text countdownText;
    public Text Guest;
    public Text Home;
    public Text Goal_msg;

    private bool end_game;
    private bool time_paused;

    void Start() {
		StartCoroutine ("LoseTime");
        Guest.text = "0";
        Home.text = "0";
        Goal_msg.text = "";
        end_game = false;
        time_paused = false;
    }

    public bool SetScore(string team) {
        bool updated = false;
        time_paused = true;
        if (team.Equals("guest"))
        {
            int score = int.Parse(Guest.text);
            Guest.text = (score + 1).ToString();
            Goal_msg.text = "Ouch! It was GOAL...";
            updated = true;
        }
        else if (team.Equals("home"))
        {
            int score = int.Parse(Home.text);
            Home.text = (score + 1).ToString();
            Goal_msg.text = "GOOOOAAAAL!";
            updated = true;
        }
        return updated;
    }

	void Update() {
        if (!time_paused) {
            if (timeLeft % 60 < 10)
                countdownText.text = ((timeLeft / 60) + ":0" + (timeLeft % 60));
            else
                countdownText.text = ((timeLeft / 60) + ":" + (timeLeft % 60));

            if (timeLeft <= 0)
            {
                StopCoroutine("LoseTime");
                countdownText.text = "Times Up!";
                end_game = true;
            }
        }
	}

    public bool GetEndGame() {
        return end_game;
    }

    public void SetTimePaused(bool pause) {
        time_paused = pause;
    }

	IEnumerator LoseTime() {
		while (true) {
			yield return new WaitForSeconds (1);
			timeLeft--;
		}
	}
}
