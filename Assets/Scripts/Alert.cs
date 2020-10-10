using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public Vector2 target;
    public int targetLayer;

    MapController map;
    CharacterMove mover;

    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map").GetComponent<MapController>();
        mover = GetComponent<CharacterMove>();
    }

    // Update is called once per frame
    void Update()
    {
        mover.dir = map.ShortestPathDirection(transform.position, target);
        var cast = Physics2D.Raycast(transform.position, target - (Vector2)transform.position);
        if(cast.collider.gameObject.layer == targetLayer) {
            target = cast.collider.gameObject.transform.position;
        } else if(((Vector2)transform.position - target).magnitude < 0.01f) {
            gameObject.SendMessage("OnChaseEnd");
        }
    }


}
