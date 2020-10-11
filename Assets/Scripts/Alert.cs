using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public Transform target;
    public float speed;
    public Vector2 lastTarget;

    MapController map;
    CharacterMove mover;

    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map").GetComponent<MapController>();
        mover = GetComponent<CharacterMove>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mover.dir = map.ShortestPathDirection(transform.position, lastTarget);
        mover.speed = speed;
        var cast = Physics2D.Raycast(transform.position, target.position - transform.position);
        if(cast.collider && cast.collider.gameObject.transform == target) {
            lastTarget = cast.collider.gameObject.transform.position;
        } else if((transform.position - target.position).magnitude < 0.01f) {
            gameObject.SendMessage("OnChaseEnd");
        }
    }


}
