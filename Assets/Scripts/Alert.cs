using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public Transform target;
    public float speed;
    public Vector2 lastTarget;
    public float reacquireRange = 2f;

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
        if(Mathf.Abs(transform.position.x - Mathf.Round(transform.position.x)) < 0.2f) {
            return;
        }
        if(Mathf.Abs(transform.position.y - Mathf.Round(transform.position.y)) < 0.2f) {
            return;
        }
        mover.dir = map.ShortestPathDirection(transform.position, lastTarget);
        mover.speed = speed;
        if(mover.dir == Vector2.zero) {
            gameObject.SendMessage("OnChaseEnd");
            return;
        }
        var cast = Physics2D.Raycast(transform.position, target.position - transform.position, reacquireRange, Physics2D.DefaultRaycastLayers ^ 1<<11);
        if(cast.collider && cast.collider.gameObject.transform == target) {
            lastTarget = cast.collider.gameObject.transform.position;
        } else if((transform.position - target.position).magnitude < 0.01f) {
            gameObject.SendMessage("OnChaseEnd");
        }
    }


}
