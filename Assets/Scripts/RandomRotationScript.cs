using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotationScript : MonoBehaviour {

    [SerializeField]
    Vector2 xRotationRange = new Vector2(-10, 10);
    [SerializeField]
    Vector2 yRotationRange = new Vector2(-10, 10);
    [SerializeField]
    Vector2 zRotationRange = new Vector2(-10, 10);

    [SerializeField]
    Vector3 rotationPerSecond;

	// Use this for initialization
	void Start () {
        float xSign = Random.Range(0, 2) * 2 - 1;
        float ySign = Random.Range(0, 2) * 2 - 1;
        float zSign = Random.Range(0, 2) * 2 - 1;

        float xRot = xSign * Random.Range(xRotationRange.x, xRotationRange.y);
        float yRot = ySign * Random.Range(yRotationRange.x, yRotationRange.y);
        float zRot = zSign * Random.Range(zRotationRange.x, zRotationRange.y);

        rotationPerSecond = new Vector3(xRot, yRot, zRot);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(rotationPerSecond.y * Time.deltaTime, rotationPerSecond.x * Time.deltaTime, rotationPerSecond.z * Time.deltaTime));
	}
}
