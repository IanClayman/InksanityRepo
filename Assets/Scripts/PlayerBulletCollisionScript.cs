using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBulletCollisionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {   
        ContactPoint cp = collision.contacts[0];
        if (cp.otherCollider.tag == "Enemy Bullet") {
                
            if (ScoreScript.scoreForBullet > 0) {
                GameObject scorePopup = new GameObject("Score Popup");
                scorePopup.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

                Rigidbody rb = scorePopup.AddComponent(typeof(Rigidbody)) as Rigidbody;
                rb.velocity = new Vector3(0, 0.5f, 0);
                rb.useGravity = false;

                TextMeshPro tm = scorePopup.AddComponent(typeof(TextMeshPro)) as TextMeshPro;
                tm.SetText("+" + ScoreScript.scoreForBullet);
                tm.alignment = TextAlignmentOptions.Center;
                tm.fontSize = 3;
                tm.color = (cp.otherCollider.name == "BurstBulletPrefab(Clone)") ? new Color32(208, 16, 0, 255) : new Color32 (52, 56, 64, 255);

                TextFadeoutScript fs = scorePopup.AddComponent(typeof(TextFadeoutScript)) as TextFadeoutScript;
                fs.fadeoutTime = 1;
            }

            ScoreScript.score += ScoreScript.scoreForBullet;

            Destroy(cp.otherCollider.gameObject);
            Destroy(this.gameObject);
        } else if (cp.otherCollider.tag == "Enemy") {
            if (ScoreScript.scoreForEnemy > 0)
            {
                GameObject scorePopup = new GameObject("Score Popup");
                scorePopup.transform.position = GameObject.Find("Enemy").transform.position;
                scorePopup.transform.position += new Vector3(-0.0625f, 0.25f, -1);

                Rigidbody rb = scorePopup.AddComponent(typeof(Rigidbody)) as Rigidbody;
                rb.velocity = new Vector3(0, 0.5f, 0);
                rb.useGravity = false;

                TextMeshPro tm = scorePopup.AddComponent(typeof(TextMeshPro)) as TextMeshPro;
                tm.SetText("+" + ScoreScript.scoreForEnemy);
                tm.alignment = TextAlignmentOptions.Center;
                tm.fontSize = 3;
                tm.color = new Color32(52, 56, 64, 255);

                TextFadeoutScript fs = scorePopup.AddComponent(typeof(TextFadeoutScript)) as TextFadeoutScript;
                fs.fadeoutTime = 1;
            }

            ScoreScript.score += ScoreScript.scoreForEnemy;

            Destroy(this.gameObject);
        }
    }

}
