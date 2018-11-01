using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesScript : MonoBehaviour {

    public TextMeshProUGUI livesCounter;

    [SerializeField]
    [Range(1, 5)]
    public int setLives = 3;

    public static int lives;

    // Use this for initialization
    void Start () {
        lives = setLives;
	}
	
	// Update is called once per frame
	void Update () {
        livesCounter.SetText("Lives: " + lives);
	}
}
