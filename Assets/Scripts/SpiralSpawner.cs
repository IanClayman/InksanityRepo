using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Rotation { CW, CCW };


[System.Serializable]
public class SpiralSpawner : BulletSpawner {

    public float spawnDuration { get; set; }
    public float revolutions { get; set; }
    public Rotation rotation { get; set; }

    /// <summary>
    /// Creates  spiral pattern of bullets (requires the BulletDelay method)
    /// </summary>
    /// <param name="go">bullet gameobject</param>
    /// <param name="dir">direction of first bullet fired</param>
    /// <param name="speed">speed of bullets</param>
    /// <param name="dur">duration of bullet spawning</param>
    /// <param name="revs">number of revolutions to make (1 rev = 360 degrees)</param>
    /// <param name="n">number of bullets to fire</param>
    /// <param name="time">time before spawner self-destructs</param>
    public SpiralSpawner(GameObject go, Vector2 dir, float speed, float dur, float revs, Rotation rot, int n, float time) : base(go, dir, speed, time) {
        spawnDuration = dur;
        revolutions = revs;
        rotation = rot;
        base.numBullets = n;
    }

    public void SetAll(GameObject obj, Vector2 dir, float s, float dur, float revs, Rotation rot, int n, float time) {
        bulletPrefab = obj;
        direction = dir;
        speed = s;
        spawnDuration = dur;
        revolutions = revs;
        rotation = rot;
        numBullets = n;
        selfdestructTimer = time;
    }

    public override Vector3[] BulletVelocities()
    {
        Vector3[] results = new Vector3[numBullets];
        Vector3 normDirection = Vector3.Normalize(direction);

        float angleBtw = (numBullets > 1) ? Mathf.Deg2Rad * 360f * revolutions / (numBullets) : 0;

        for (int i = 0; i < numBullets; i++)
        {
            float thisAngle;
            thisAngle = (rotation == Rotation.CCW) ? (i * angleBtw) : (-1 * i * angleBtw);
            //Debug.Log("i: " + i + " | angle: " + Mathf.Rad2Deg * thisAngle);
            float thisX = (normDirection.x * Mathf.Cos(thisAngle)) - (normDirection.y * Mathf.Sin(thisAngle));
            float thisY = (normDirection.x * Mathf.Sin(thisAngle)) + (normDirection.y * Mathf.Cos(thisAngle));

            results[i] = Vector3.Normalize(new Vector3(thisX, thisY)) * speed;
        }

        return results;
    }

    public float BulletDelay() {
        return spawnDuration / numBullets;
    }

    public IEnumerator FireBullets() {
        Vector3[] velocities = new Vector3[numBullets];
        velocities = BulletVelocities();

        foreach (Vector3 v in velocities) {
            GameObject newBullet = Instantiate(bulletPrefab, this.transform.position + 0.5f * Vector3.forward, Quaternion.Euler(new Vector3(0, 270, 90)));
            newBullet.GetComponent<Rigidbody>().velocity = v;

            yield return new WaitForSeconds(this.BulletDelay());
        }
    }
    
}
