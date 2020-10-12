using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite closed;
    public Sprite opened;
    public int switches = 1;
    SpriteRenderer sr;
    CapsuleCollider2D cl;
    private MapController mc;
    Vector2 offset = new Vector2(0.1f, -1);
    void Start()
    {
        mc = GameObject.Find("Map").GetComponent<MapController>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closed;
        cl = GetComponent<CapsuleCollider2D>();
        cl.enabled = true;
    }


    private void FixedUpdate() {
        if(switches == 0) {
            open();
        }
        else {
            close();
        }
    }

    public void close() {
        int x, y;
        x = (int)transform.position.x;
        y = (int)transform.position.y;
        mc.tiles[x, y].down = false;
        mc.tiles[x, y - 1].up = false;

        sr.sprite = closed;
        cl.enabled = true;
    }
        
    public void open() {
        int x, y;
        x = (int)transform.position.x;
        y = (int)transform.position.y;
        mc.tiles[x, y].down = true;
        mc.tiles[x, y - 1].up = true;

        sr.sprite = opened;
        cl.enabled = false;
    }
}
