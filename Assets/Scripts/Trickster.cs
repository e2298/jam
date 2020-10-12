using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trickster : MonoBehaviour
{
    public bool isOnLever = false;
    public float leverRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var cast = Physics2D.CircleCast(transform.position, leverRange, Vector2.one, 0, 1<<10);
        if (cast.collider) {
            if(!isOnLever)
                cast.collider.gameObject.GetComponent<Lever>().OnFlip();
            isOnLever = true;
        }
        else {
            isOnLever = false;
        }
    }
}
