using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public Transform mainMenu, choiseMenu, levelMenu;
	public AudioSource music;

	void Start() {
		music.Play ();
	}

    public void LoadSceneGame (string name) {
		SceneManager.LoadScene (name);
		music.Stop ();
	}

	public void QuitGame () {
		Application.Quit ();
	}

    public void ChoiseMenu (bool clicked) {
        choiseMenu.gameObject.SetActive(clicked);
        levelMenu.gameObject.SetActive(!clicked);
        mainMenu.gameObject.SetActive(!clicked);
		if (!music.isPlaying) music.Play ();
    }

    public void ChoseCar1() {
        PlayerPrefs.SetInt("carElection", 1);
        GoToLevelMenu(true);
    }

    public void ChoseCar2() {
        PlayerPrefs.SetInt("carElection", 2);
        GoToLevelMenu(true);
    }

    public void ChoseCar3() {
        PlayerPrefs.SetInt("carElection", 3);
        GoToLevelMenu(true);
    }

    public void GoBackMainMenu(bool clicked) {
        choiseMenu.gameObject.SetActive(false);
        levelMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
		if (!music.isPlaying) music.Play ();
    }

    public void GoBackChoiseMenu(bool clicked) {
        choiseMenu.gameObject.SetActive(true);
        levelMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
		if (!music.isPlaying) music.Play ();
    }

    private void GoToLevelMenu(bool clicked) {
        choiseMenu.gameObject.SetActive(!clicked);
        levelMenu.gameObject.SetActive(clicked);
        mainMenu.gameObject.SetActive(!clicked);
		if (!music.isPlaying) music.Play ();
    }

    public void ChoseLevel1() {
        PlayerPrefs.SetInt("level", 1);
        LoadSceneGame("scene1");
    }

    public void ChoseLevel2() {
        PlayerPrefs.SetInt("level", 2);
        LoadSceneGame("scene1");
    }

    public void ChoseLevel3() {
        PlayerPrefs.SetInt("level", 3);
        LoadSceneGame("scene1");
    }
}
