using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject dragLineObj;
    [SerializeField]
    [Range(0.1f, 2f)]
    float minDragDist = 0.5f;
    [SerializeField]
    [Range(1, 5)]
    float maxDragDist = 3f;
    [SerializeField]
    [Range(1, 50)]
    float bulletVelocity = 10;

    GameObject player;
    LineRenderer dragLine;
    TrailRenderer bulletTrail;

    Vector3 touchPoint;
    GameObject newBullet;
    Color bulletColor;
    Vector3 dragPoint;
    Vector3 direction;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");

        dragLine = dragLineObj.GetComponent<LineRenderer>();
        if (dragLine == null)
        {
            Debug.LogError("No line renderer assigned!  Please check Drag Line Obj and its components!");
            Debug.Break();
        }
        dragLine.positionCount = 2;
        dragLineObj.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Background") {
                    touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    touchPoint.z = player.transform.position.z;

                    newBullet = Instantiate(bulletPrefab, touchPoint, Quaternion.Euler(new Vector3(0, 270, 90)));

                    bulletColor = newBullet.GetComponent<Renderer>().material.color;
                    Color halfAlpha = new Color(bulletColor.r, bulletColor.g, bulletColor.b, bulletColor.a / 2);

                    newBullet.GetComponent<Renderer>().material.color = halfAlpha;
                    newBullet.GetComponent<TrailRenderer>().enabled = false;
                }
            }
        } else if (Input.GetMouseButton(0) && newBullet) {
            dragPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragPoint.z = player.transform.position.z;

            if (Vector3.Distance(dragPoint, touchPoint) >= minDragDist) {
                direction = (dragPoint - touchPoint).normalized;
                float dist = (2 * Vector3.Distance(dragPoint, touchPoint) < Vector3.Distance(dragPoint, touchPoint) + 1) ?
                    2 * Vector3.Distance(dragPoint, touchPoint) : Vector3.Distance(dragPoint, touchPoint) + 1;

                dragLineObj.SetActive(true);
                dragLine.SetPosition(0, (touchPoint + new Vector3(0, 0, 0.125f)));
                dragLine.SetPosition(1, touchPoint + dist * direction);
            } else {
                dragLineObj.SetActive(false);
            }
        } else if (Input.GetMouseButtonUp(0) && newBullet) {
            if (Vector3.Distance(dragPoint, touchPoint) >= minDragDist) {
                newBullet.GetComponent<Renderer>().material.color = bulletColor;

                float maxDistPercentage = Vector3.Distance(dragPoint, touchPoint);
                maxDistPercentage = Mathf.Clamp(maxDistPercentage, maxDragDist/2, maxDragDist) / maxDragDist;
                newBullet.GetComponent<Rigidbody>().velocity = bulletVelocity * maxDistPercentage * direction;
                newBullet.GetComponent<TrailRenderer>().enabled = true;
            } else {
                Destroy(newBullet);
            }
                newBullet = null;
                dragLineObj.SetActive(false);
        }
    }

}
