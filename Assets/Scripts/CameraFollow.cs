using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float minDistance = 1f;

    private Rigidbody2D rb;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 dir = target.transform.position - transform.position;
        if (dir.magnitude > minDistance) {
            rb.AddForce(dir);
        }
        else {
            rb.velocity = Vector2.zero;
        }
    }
}
