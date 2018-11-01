using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBulletCollisionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        ContactPoint cp = collision.contacts[0];
        if (cp.otherCollider.tag == "Player Hitbox") {
            GameObject scorePopup = new GameObject("Score Popup");
            scorePopup.transform.position = GameObject.Find("Player").transform.position;
            scorePopup.transform.position += new Vector3(-0.0625f, 0.25f, -1);

            Rigidbody rb = scorePopup.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.velocity = new Vector3(0, 0.5f, 0);
            rb.useGravity = false;

            TextMeshPro tm = scorePopup.AddComponent(typeof(TextMeshPro)) as TextMeshPro;
            tm.SetText("-1 Life");
            tm.alignment = TextAlignmentOptions.Center;
            tm.fontSize = 3;
            tm.color = new Color32(32, 32, 32, 255);

            TextFadeoutScript fs = scorePopup.AddComponent(typeof(TextFadeoutScript)) as TextFadeoutScript;
            fs.fadeoutTime = 1;

            LivesScript.lives--;

            Destroy(this.gameObject);
        }
    }
}
