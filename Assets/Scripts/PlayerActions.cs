using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnUse() {
        var cast = Physics2D.CircleCast(transform.position, 0.5f, Vector2.one, 0, 1<<10);
        if (cast.collider) {
            cast.collider.gameObject.GetComponent<Lever>().OnFlip();
        }
    }
}
