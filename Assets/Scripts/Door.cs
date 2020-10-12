using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite closed;
    public Sprite opened;
    SpriteRenderer sr;

    private MapController mc;
    Vector2 offset = new Vector2(0.1f, -1);
    void Start()
    {
        mc = GameObject.Find("Map").GetComponent<MapController>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closed;
    }

    public void close() {
        transform.SetPositionAndRotation(transform.position - (Vector3)offset, Quaternion.identity);
        int x, y;
        x = (int)transform.position.x;
        y = (int)transform.position.y;
        mc.tiles[x, y].down = false;
        mc.tiles[x, y - 1].up = false;
        mc.tiles[x, y - 1].right = true;
        mc.tiles[x + 1, y - 1].left = true;

        sr.sprite = closed;

    }
        
    public void open() {
        int x, y;
        x = (int)transform.position.x;
        y = (int)transform.position.y;
        mc.tiles[x, y].down = true;
        mc.tiles[x, y - 1].up = true;
        mc.tiles[x, y - 1].right = false;
        mc.tiles[x + 1, y - 1].left = false;

        sr.sprite = opened;

        transform.SetPositionAndRotation(transform.position + (Vector3)offset, Quaternion.Euler(0, 0, 90));
    }
}
