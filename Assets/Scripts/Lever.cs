using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    private SpriteRenderer sr;
    private bool state = false;

    private Door doorscript;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = up;

        doorscript = transform.parent.Find("Door").GetComponent<Door>();
    }

    public void OnFlip() {
        state = !state;
        if (state) {
            sr.sprite = down;
            doorscript.switches--;
        }
        else {
            sr.sprite = up;
            doorscript.switches++;
        }
    }
}
