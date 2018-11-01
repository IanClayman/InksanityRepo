using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RingSpawner : BulletSpawner{

    public float spreadSpeed { get; set; }

    /// <summary>
    /// Creates a ring of bullets fired simultaneously
    /// </summary>
    /// <param name="go">bullet gameobject</param>
    /// <param name="dir">direction ring will be fired</param>
    /// <param name="speed">speed of ring's movement</param>
    /// <param name="ss">spread speed of individual bullets</param>
    /// <param name="n">number of bullets</param>
    /// <param name="time">time before spawner self-destructs</param>
    public RingSpawner(GameObject go, Vector2 dir, float speed, float ss, int n, float time) : base (go, dir, speed, time) {
        spreadSpeed = ss;
        base.numBullets = n;

        if (speed == spreadSpeed) {
            spreadSpeed *= 1.25f;
        }
    }

    public void SetAll(GameObject obj, Vector2 dir, float s, float ss, int n, float time) {
        bulletPrefab = obj;
        direction = dir;
        speed = s;
        spreadSpeed = ss;
        numBullets = n;
        selfdestructTimer = time;
    }

    public override Vector3[] BulletVelocities() {
        Vector3[] results = new Vector3[numBullets];
        Vector3 normDirection = Vector3.Normalize(direction);

        float angleBtw = (numBullets > 1) ? Mathf.Deg2Rad * 360f / (numBullets) : 0;

        for (int i = 0; i < numBullets; i++) {
            float thisAngle = (-1 * Mathf.Deg2Rad * 360f / 2) + (i * angleBtw);
            //Debug.Log("i: " + i + " | angle: " + Mathf.Rad2Deg * thisAngle);
            float thisX = (normDirection.x * Mathf.Cos(thisAngle)) - (normDirection.y * Mathf.Sin(thisAngle));
            float thisY = (normDirection.x * Mathf.Sin(thisAngle)) + (normDirection.y * Mathf.Cos(thisAngle));

            if (speed != spreadSpeed) {
                results[i] = Vector3.Normalize(new Vector3(thisX, thisY)) * spreadSpeed + normDirection * speed;
            } else {
                results[i] = Vector3.Normalize(new Vector3(thisX, thisY)) * (spreadSpeed * 1.25f) + normDirection * speed;
            }
        }

        return results;
    }

    public void FireBullets() {
        Vector3[] velocities = new Vector3[numBullets];
        velocities = BulletVelocities();

        foreach (Vector3 v in velocities) {
            GameObject newBullet = Instantiate(bulletPrefab, this.transform.position + 0.5f * Vector3.forward, Quaternion.Euler(new Vector3(0, 270, 90)));
            newBullet.GetComponent<Rigidbody>().velocity = v;
        }
    }

}
