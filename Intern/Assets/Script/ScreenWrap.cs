using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {
    public float leftCons= Screen.width;
    public float rightCons = Screen.width;
    private Camera cam;
    private float distanceZ;
    private float buffer = 1.018f;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);
        leftCons = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x ;
        rightCons = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
    }


    // Update is called once per frame
    private void Update () {
        if (transform.position.x < leftCons - buffer) {
            GameManager.FindSelf(this.gameObject,0);
        }
        if (transform.position.x > rightCons +buffer)
        {
            GameManager.FindSelf(this.gameObject, 1);
        }

    }


}
