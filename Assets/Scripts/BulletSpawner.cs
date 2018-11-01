using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletSpawner : MonoBehaviour{

    public GameObject bulletPrefab { get; set; }
    public Vector2 direction { get; set; }
    public float speed { get; set; }
    public int numBullets { get; set; }
    public float selfdestructTimer { get; set; }

    public BulletSpawner(GameObject go, Vector2 dir, float s, float time) {
        bulletPrefab = go;
        direction = dir;
        speed = s;
        numBullets = 1;
        selfdestructTimer = time;
    }

    public virtual Vector3[] BulletVelocities() {
        Vector3[] results = new Vector3[1];
        results[0] = Vector3.Normalize(direction) * speed;
        return results;
    }

    private void Start() {
        Destroy(this.gameObject, selfdestructTimer);
    }
}
