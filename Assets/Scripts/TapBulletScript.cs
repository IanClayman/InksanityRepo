using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapBulletScript : MonoBehaviour {

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    [Range(1, 50)]
    float bulletVelocity = 10;
    [SerializeField]
    [Range(0, 5)]
    float fireDelay = 0.5f;

    GameObject player;
    TrailRenderer bulletTrail;

    float t = -1;
    bool canFire = false;
    Vector3 touchPoint;
    GameObject newBullet;



	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag != "Player") {
                canFire = true;
            }
        } else if (Input.GetMouseButton(0) && canFire && Physics.Raycast(ray, out hit)) {
            if(hit.collider.tag != "Player") {
                touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                touchPoint.z = player.transform.position.z;

                Vector3 direction = Vector3.Normalize(touchPoint - player.transform.position);

                if (t == -1 && InkwellScript.inkLevel >= InkwellScript.bulletInk) {
                    newBullet = Instantiate(bulletPrefab, player.transform.position, Quaternion.Euler(new Vector3(0, 270, 90)));
                    newBullet.GetComponent<Rigidbody>().velocity = direction * bulletVelocity;

                    InkwellScript.inkLevel -= InkwellScript.bulletInk;
                    t = 0;
                }
            }
        } else if (Input.GetMouseButtonUp(0) && canFire) {
            canFire = false;
        }

        if(t >= 0) { t += Time.deltaTime; }

        if(t > fireDelay) { t = -1; }
    }

}
