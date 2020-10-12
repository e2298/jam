using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GhostActions : MonoBehaviour
{
    public GameObject body;
    public float range;
    private CameraFollow fl;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        fl = Camera.main.GetComponent<CameraFollow>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector2 tetherPoint = body.transform.position;
        Vector2 rel = (Vector2)transform.position - tetherPoint;
        rel = Vector2.ClampMagnitude(rel, range);
        transform.position = tetherPoint + rel;
    }

    void OnTransform() {
        gameObject.SetActive(false);
        body.SetActive(true);
        fl.target = body;
    }
}
