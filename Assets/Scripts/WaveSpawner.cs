using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSpawner: BulletSpawner {

    public float angle { get; set; }

    /// <summary>
    /// Creates a wave of bullets fired simultaneously
    /// </summary>
    /// <param name="go">bullet gameobject</param>
    /// <param name="dir">initial direction of bullets</param>
    /// <param name="s">bullet speed</param>
    /// <param name="a">angle of arc from initial direction (pos & neg angle)</param>
    /// <param name="n">number of bullets</param>
    /// <param name="time">time before spawner self-destructs</param>
    public WaveSpawner(GameObject go, Vector2 dir, float s, float a, int n, float time) : base (go, dir, s, time) {
        angle = a;
        base.numBullets = n;

        angle = (numBullets == 1) ? 0 : angle;
    }

    public void SetAll(GameObject obj, Vector2 dir, float s, float a, int n, float time) {
        bulletPrefab = obj;
        direction = dir;
        speed = s;
        angle = a;
        numBullets = n;
        selfdestructTimer = time;
    }

    public override Vector3[] BulletVelocities() {
        Vector3[] results = new Vector3[numBullets];
        Vector3 normDirection = Vector3.Normalize(direction);

        float angleBtw = (numBullets > 1) ? Mathf.Deg2Rad * angle / (numBullets - 1) : 0;

        for (int i = 0; i < numBullets; i++) {
            float thisAngle = (-1 * Mathf.Deg2Rad * angle / 2) + (i * angleBtw);
            //Debug.Log("i: " + i + " | angle: " + Mathf.Rad2Deg * thisAngle);
            float thisX = (normDirection.x * Mathf.Cos(thisAngle)) - (normDirection.y * Mathf.Sin(thisAngle));
            float thisY = (normDirection.x * Mathf.Sin(thisAngle)) + (normDirection.y * Mathf.Cos(thisAngle));

            results[i] = Vector3.Normalize(new Vector3(thisX, thisY)) * speed;
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
