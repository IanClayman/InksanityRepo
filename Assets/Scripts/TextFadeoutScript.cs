using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class TextFadeoutScript : MonoBehaviour {

    TextMeshPro tm;

    public float fadeoutTime = 1;

    private float t;
    private float startAlpha;

	// Use this for initialization
	void Start () {
        tm = GetComponent<TextMeshPro>();
        startAlpha = tm.color.a;
	}
	
	// Update is called once per frame
	void Update () {
		if (t <= 1) {
            float newAlpha = tm.color.a - ((fadeoutTime / startAlpha) * Time.deltaTime);
            newAlpha = Mathf.Clamp01(newAlpha);
            tm.color = new Color32((byte)(tm.color.r * 255), (byte)(tm.color.g * 255), (byte)(tm.color.b * 255), (byte)(newAlpha * 255));
        } else {
            Destroy(this.gameObject);
        }
        t += Time.deltaTime;
    }
}
