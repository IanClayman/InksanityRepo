using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour {

    public TextMeshProUGUI countdownTimer;

    [SerializeField]
    [Range(1, 5)]
    public int setTimer = 3;

    public static int timer;

	// Use this for initialization
	void Start () {
        timer = setTimer;
        countdownTimer.gameObject.SetActive(true);

        StartCoroutine(CountdownRoutine());
	}
	
	// Update is called once per frame
	void Update () {
        countdownTimer.SetText(timer.ToString());
	}

    IEnumerator CountdownRoutine() {
        while (timer > 0) {
            yield return new WaitForSeconds(1);
            timer--;
        }

        countdownTimer.gameObject.SetActive(false);
    }
}
