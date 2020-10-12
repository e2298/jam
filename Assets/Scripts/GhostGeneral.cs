using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.U2D;

public class GhostGeneral : MonoBehaviour
{
    SpriteRenderer rd;
    Light2D lght;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<SpriteRenderer>();
        rd.enabled = false;
        lght = GetComponent<Light2D>();
        GhostMode(false);
    }

    void GhostMode(bool on) {
        rd.enabled = on;
        lght.enabled = on;
    }
}
