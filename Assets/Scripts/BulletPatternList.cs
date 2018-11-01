using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BulletPatternList : MonoBehaviour {
    //public GameObject bulletPrefab;

    [ListDrawerSettings(NumberOfItemsPerPage = 3)]
    public BulletPattern[] patternList;

    GameObject player;
    //BulletSpawner[] spawnArr;

    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player");

        StartCoroutine(SpawnRoutine());
        
    }

    // Update is called once per frame
    void Update() {
    }


    IEnumerator SpawnRoutine() {
        for (int i = 0; i < patternList.Length; i++) {
            Vector2 direction;
            if (patternList[i].aimAtPlayer) {
                direction = Vector3.Normalize(-1 * transform.position + player.transform.position);
            } else {
                direction = patternList[i].direction;
            }

            GameObject spawnerObject = new GameObject("Spawner " + i);
            spawnerObject.transform.position = transform.position;

            if (patternList[i].patternType == PatternType.wave) {
                WaveSpawner ws = spawnerObject.AddComponent(typeof(WaveSpawner)) as WaveSpawner;
                ws.SetAll(patternList[i].bullet, direction, patternList[i].speed, patternList[i].angle, patternList[i].numBullets, 5);
                ws.FireBullets();
            } else if (patternList[i].patternType == PatternType.ring) {
                RingSpawner rs = spawnerObject.AddComponent(typeof(RingSpawner)) as RingSpawner;
                rs.SetAll(patternList[i].bullet, direction, patternList[i].speed, patternList[i].spreadSpeed, patternList[i].numBullets, 5);
                rs.FireBullets();
            } else if (patternList[i].patternType == PatternType.spiral) {
                SpiralSpawner ss = spawnerObject.AddComponent(typeof(SpiralSpawner)) as SpiralSpawner;
                ss.SetAll(patternList[i].bullet, direction, patternList[i].speed, patternList[i].duration, patternList[i].revolutions,
                    patternList[i].rotation, patternList[i].numBullets, patternList[i].duration * 2);
                StartCoroutine(ss.FireBullets());

            }

            yield return new WaitForSeconds(patternList[i].delay);
        }
    }

    IEnumerator DelayRoutine(float t) {
        yield return new WaitForSeconds(t);
    }
    
}
