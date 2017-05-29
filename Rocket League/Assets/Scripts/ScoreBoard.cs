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
	public AudioSource win;
	public AudioSource lose;
	public AudioSource explosion;

    private bool end_game;
    private bool time_paused;
    private int time;
    private bool car_stoped;

    void Start() {
		StartCoroutine ("LoseTime");
        Guest.text = "0";
        Home.text = "0";
        Goal_msg.text = "";
        Restart_msg.text = "";
        end_game = false;
        time_paused = false;
        time = timeLeft + 5;
        car_stoped = true;
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
			lose.Play ();
			explosion.Play ();
            updated = true;
        }
        else if (team.Equals("home"))
        {
            int score = int.Parse(Home.text);
            Home.text = (score + 1).ToString();
            Goal_msg.text = "GOAL!";
			win.Play ();
			explosion.Play ();
            updated = true;
        }
        return updated;
    }

	void Update() {
        if (car_stoped) {
            Goal_msg.text = (3 - ((time - 5) - timeLeft)).ToString();
            if (Goal_msg.text == "0") {
                car_stoped = false;
                Goal_msg.text = "";
                timeLeft += 3;
            }
        }
        if (!time_paused && !car_stoped) {
            if (timeLeft % 60 < 10)
                countdownText.text = ((timeLeft / 60) + ":0" + (timeLeft % 60));
            else
                countdownText.text = ((timeLeft / 60) + ":" + (timeLeft % 60));
        }
        if (timeLeft <= 0 && !time_paused) {
            StopCoroutine("LoseTime");
            end_game = true;
            Goal_msg.text = "Times Up! ";
            if (int.Parse(Home.text) > int.Parse(Guest.text))
            {
                win.Play();
                Goal_msg.text += "You've won!";
            }
            else if (int.Parse(Home.text) < int.Parse(Guest.text))
            {
                lose.Play();
                Goal_msg.text += "You've lost...";
            }
            else {
                win.Play();
                Goal_msg.text += "It was a tie";
            }
            Restart_msg.text = "Press 'R' to go to the Menu";
        }

        if (GiveStart())
            Goal_msg.text = (9 - (time - timeLeft)).ToString();

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
        return (time - timeLeft >= 9 && time_paused);
    }

    public bool GiveStart() {
        return (time - timeLeft >= 6 && time - timeLeft < 9 && time_paused);
    }

    public void SetMoreTime(int sec) {
        timeLeft += sec;
    }

    public bool GetCarStoped() {
        return car_stoped;
    }

	IEnumerator LoseTime() {
		while (true) {
			yield return new WaitForSeconds (1);
                timeLeft--;
		}
	}
}
