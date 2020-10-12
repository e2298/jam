using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    Alert alertScript;
    Patrol patrolScript;
    // Start is called before the first frame update
    void Start()
    {
        alertScript = GetComponent<Alert>();
        patrolScript = GetComponent<Patrol>();
    }

    void OnPlayerSeen(Vector2 position) {
        alertScript.enabled = true;
        alertScript.lastTarget = position;
        patrolScript.enabled = false;
    }

    void OnChaseEnd() {
        alertScript.enabled = false;
        patrolScript.enabled = true;
        patrolScript.inRoute = false;
    }
}
