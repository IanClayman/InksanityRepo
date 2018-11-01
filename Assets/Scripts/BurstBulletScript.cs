using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstBulletScript : MonoBehaviour {

    public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        foreach(ContactPoint cp in collision) {
            if (cp.otherCollider.tag == "Player Bullet") {
                RingSpawner ring = new RingSpawner(bulletPrefab, Vector2.down, 0, 1, 18, 10);

                Vector3[] velocities = new Vector3[ring.numBullets];
                velocities = ring.BulletVelocities();

                foreach (Vector3 v in velocities) {
                    GameObject newBullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(new Vector3(0, 270, 90)));
                    newBullet.GetComponent<Rigidbody>().velocity = v;
                }
            }
        }
    }

}
