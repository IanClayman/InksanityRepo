using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour {

    public TextMeshProUGUI scoreCounter;
    
    [SerializeField]
    [Range(0, 10)]
    public int scorePerSec = 5;

    [SerializeField]
    [Range(0, 10)]
    public int setScoreForBullet;

    [SerializeField]
    [Range(0, 100)]
    public int setScoreForEnemy;

    public static float score = 0;
    public static int scoreForBullet;
    public static int scoreForEnemy;

    // Use this for initialization
    void Start () {
        scoreForBullet = setScoreForBullet;
        scoreForEnemy = setScoreForEnemy;
	}
	
	// Update is called once per frame
	void Update () {
        score += scorePerSec * Time.deltaTime;
        
        scoreCounter.SetText("Score: " + Mathf.RoundToInt(score).ToString("000000"));
	}
}
