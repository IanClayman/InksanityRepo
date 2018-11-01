using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragScript : MonoBehaviour {

    public GameObject dragLineObj;
    public Vector2 xDimensions = new Vector2(-1, 1);
    public Vector2 yDimensions = new Vector2(-1, 1);

    GameObject player;
    public bool canDrag = true;

    Vector3 dragPoint;
    LineRenderer dragLine;

    private Vector3 vel = Vector3.zero;

	// Use this for initialization
	void Start () {
        dragLine = dragLineObj.GetComponent<LineRenderer>();
        if(dragLine ==null) {
            Debug.LogError("No line renderer assigned!  Please check Drag Line Obj and its components!");
            Debug.Break();
        }
        dragLine.positionCount = 2;
        dragLineObj.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit)) {

                if(hit.collider.tag == "Player") {
                    player = hit.collider.gameObject;
                }
            }
        } else if (Input.GetMouseButton(0) && player && canDrag) {
            dragPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragPoint.z = player.transform.position.z;

            dragLineObj.SetActive(true);
            dragLine.SetPosition(0, player.transform.position);
            dragLine.SetPosition(1, dragPoint);

            Debug.DrawLine(player.transform.position, dragPoint, Color.green);
            //Debug.Log(dragPoint);
        } else if (Input.GetMouseButtonUp(0) && player && canDrag) {
            dragPoint.x = Mathf.Clamp(dragPoint.x, xDimensions.x, xDimensions.y);
            dragPoint.y = Mathf.Clamp(dragPoint.y, yDimensions.x, yDimensions.y);

            if (InkwellScript.inkLevel >= InkwellScript.movementInk) {
                StartCoroutine(movePlayer(player, dragPoint, 0.125f));
                InkwellScript.inkLevel -= InkwellScript.movementInk;
            }

            player = null;
            dragLineObj.SetActive(false);

        }
	}

    IEnumerator movePlayer(GameObject player, Vector3 targetPos, float moveTime) {
        canDrag = false;

        while (Vector3.Distance(player.transform.position, targetPos) > 0.01f) {
            player.transform.position = Vector3.SmoothDamp(player.transform.position, targetPos, ref vel, moveTime);
            yield return new WaitForEndOfFrame();

            
        }

        canDrag = true;
    }



}
