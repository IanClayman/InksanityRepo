using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    public Image gameOverContainer;
    public TextMeshProUGUI finalScore;

	// Use this for initialization
	void Start () {
        gameOverContainer.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(LivesScript.lives == 0) {
            gameOverContainer.gameObject.SetActive(true);

            finalScore.SetText("Final Score: " + ScoreScript.score.ToString("000000"));
        }
	}

    public void ResetGame() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void QuitGame() {
        Application.Quit();

        Debug.Log("Break point hit!  Line reached after Application.Quit() called.");
        Debug.Break();
    }
}
