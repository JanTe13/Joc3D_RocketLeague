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
    public Text Restart_msg;

    private bool end_game;
    private bool time_paused;
    private int time;

    void Start() {
		StartCoroutine ("LoseTime");
        Guest.text = "0";
        Home.text = "0";
        Goal_msg.text = "";
        Restart_msg.text = "";
        end_game = false;
        time_paused = false;
        time = timeLeft + 5;
    }

    public bool SetScore(string team) {
        bool updated = false;
        time_paused = true;
        time = timeLeft;
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
        }
        if (timeLeft <= 0 && !time_paused) {
                StopCoroutine("LoseTime");
                end_game = time_paused = true;
                Goal_msg.text = "Times Up! ";
                if (int.Parse(Home.text) > int.Parse(Guest.text))
                    Goal_msg.text += "You've won!";
                else if (int.Parse(Home.text) < int.Parse(Guest.text))
                    Goal_msg.text += "You've lost...";
                else
                    Goal_msg.text += "It was a tie";
                Restart_msg.text = "Press 'R' to go to the Menu";
            }
	}

    public bool GetEndGame() {
        return end_game;
    }

    public void SetTimePaused(bool pause) {
        time_paused = pause;
    }

    public void SetGoalMsg (string msg) {
        Goal_msg.text = msg;
    }

    public bool GetNewStart() {
        return (time - timeLeft >= 6 && time_paused);
    }

    public void SetMoreTime(int sec) {
        timeLeft += sec;
    }

	IEnumerator LoseTime() {
		while (true) {
			yield return new WaitForSeconds (1);
                timeLeft--;
		}
	}
}
