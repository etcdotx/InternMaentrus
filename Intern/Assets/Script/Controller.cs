using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject empty;
    private Vector2 fp;
    private Vector2 lp;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Ended){
                if (GameManager.state == GameManager.states.notplay) { GameManager.changestate(); }
            }
        }
    }

    private void OnMouseDown()
    {
        fp = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        lp = Input.mousePosition;

        if (GameManager.state == GameManager.states.play) {
            if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
            {
                if (fp.x > lp.x)
                {
                    empty.transform.Translate(-7f * Time.deltaTime, 0f, 0f);
                }
                else if (fp.x < lp.x) {
                    empty.transform.Translate(7f * Time.deltaTime, 0f, 0f);
                }
            }
        }
    }
}
