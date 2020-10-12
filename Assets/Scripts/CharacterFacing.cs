using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CharacterFacing : MonoBehaviour
{
    private CharacterMove mv;
    private SpriteRenderer sr;
    public Sprite up;
    public Sprite down;
    public Sprite left;


    // Start is called before the first frame update
    void Start()
    {
        mv = GetComponent<CharacterMove>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 d = mv.dir;
        if(d == Vector2.zero) {
            return;
        }
        if(Mathf.Abs(d.y) > Mathf.Abs(d.x)) {
            sr.flipX = false;
            if(d.y > 0) {
                sr.sprite = up;
            }
            else {
                sr.sprite = down;
            }
        }
        else {
            sr.sprite = left;
            if(d.x > 0) {
                sr.flipX = true;
            }
            else {
                sr.flipX = false;
            }
        }
    }
}
