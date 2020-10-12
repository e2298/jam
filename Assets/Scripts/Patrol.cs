using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Patrol : MonoBehaviour
{
    public Vector2[] points;
    private int pointIdx = 0;
    public bool inRoute = true;
    public float speed;

    MapController map;
    CharacterMove mover;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map").GetComponent<MapController>();
        mover = GetComponent<CharacterMove>();
    }

    void FixedUpdate()
    {
        if(((Vector2)transform.position - points[pointIdx]).magnitude < 0.5f) {
            pointIdx++;
            pointIdx %= points.Length;
            inRoute = true;
        }

        mover.speed = speed;

        if (inRoute) {
            mover.dir = (points[pointIdx] - (Vector2)transform.position).normalized;
        }
        else {
            mover.dir = map.ShortestPathDirection(transform.position, points[pointIdx]);
        }
    }
}
