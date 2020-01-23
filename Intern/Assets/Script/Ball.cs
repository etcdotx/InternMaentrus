using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private bool touch = false;
    private Rigidbody rb;

    private void Start()
    {
        touch = false;
       rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.AddForce(Vector3.down * GameManager.force *Time.deltaTime, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != gameObject.tag)
        {
            if (!touch)
            {
                touch = true;
                GameManager.Down();
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else{
            if (!touch)
            {
                touch = true;
                GameManager.addScore();
                Destroy(gameObject);
            }
        }
    }
}
