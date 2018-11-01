using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InkwellScript : MonoBehaviour {

    public Image inkBar;

    public static float inkLevel = 1;

    [SerializeField]
    [Range(0, 1)]
    public float setBulletInk = .0625f;

    [SerializeField]
    [Range(0, 1)]
    public float setMovementInk = .125f;

    [SerializeField]
    [Range(0, 1)]
    public float setInkRefillRate = .125f;

    public static float bulletInk;
    public static float movementInk;
    public static float inkRefillRate;

    float vel = 0;

    // Use this for initialization
    void Start () {
        bulletInk = setBulletInk;
        movementInk = setMovementInk;
        inkRefillRate = setInkRefillRate;
	}
	
	// Update is called once per frame
	void Update () {
        inkLevel += inkRefillRate * Time.deltaTime;
        inkLevel = Mathf.Clamp(inkLevel, 0f, 1f);

        inkBar.fillAmount = inkLevel;
	}
}
