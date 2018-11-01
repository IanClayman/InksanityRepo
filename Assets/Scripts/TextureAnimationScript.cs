using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimationScript : MonoBehaviour {

    [SerializeField]
    int index = 0;
    [SerializeField]
    bool isPlaying = true;

    [SerializeField]
    [Range(0.3125f, 100f)]
    float animationTime = 1f;
    [SerializeField]
    [Range(1, 60)]
    int framesPerSec = 30;
    [SerializeField]
    [Range(1, 256)]
    int numFrames = 64;
    [SerializeField]
    [Range(1, 64)]
    int rows = 8;
    [SerializeField]
    [Range(1, 64)]
    int columns = 8;

    float timePerFrame;
    Material material;
    // Use this for initialization
    void Start () {
        timePerFrame = (float)(1f/framesPerSec);
        material = GetComponent<Renderer>().material;

        StartCoroutine(AnimateTextureRoutine());
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator AnimateTextureRoutine() {
        while(isPlaying) {
            float yOffset = (float) ((rows - (Mathf.Floor(index/rows) + 1)) / rows);
            float xOffset = (float)(index % columns) / columns;

            material.SetTextureOffset("_MainTex", new Vector2(xOffset, yOffset));
            material.SetTextureOffset("_BumpMap", new Vector2(xOffset, yOffset));

            //Debug.Log("step is done | index = " + index + " | Time: " + Time.timeSinceLevelLoad);

            if (index < numFrames - 1) {
                index++;
            } else {
                index = 0;
            }

            yield return new WaitForSeconds(timePerFrame);
            //yield return new WaitForSeconds(0.5f);
        }
    }
}
