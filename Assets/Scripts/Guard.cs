using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;

public class Guard : MonoBehaviour
{
    Alert alertScript;
    Patrol patrolScript;
    public float viewRange;
    public float viewAngle;
    private CharacterMove mover;
    private Transform lgth;
    public GameObject target;
    Vector2 dir = new Vector2(0,-1);
    // Start is called before the first frame update
    void Start()
    {
        alertScript = GetComponent<Alert>();
        patrolScript = GetComponent<Patrol>();
        mover = GetComponent<CharacterMove>();
        lgth = transform.Find("Point Light 2D");
    }

    private void FixedUpdate() {
        if ((transform.position - target.transform.position).magnitude < 0.5f) {
            SceneManager.LoadScene(2);
        }
        if(mover.dir != Vector2.zero) {
            dir = mover.dir;
        }
        lgth.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, dir));

        if (Vector2.Angle(dir, target.transform.position - transform.position) < viewAngle / 2f) {
            var cast = Physics2D.Raycast(transform.position, target.transform.position - transform.position, viewRange, Physics2D.DefaultRaycastLayers ^ 1<<11);
            if(cast.collider && cast.collider.gameObject == target) {
                var p = target.transform.position;
                OnPlayerSeen(p);
            }
        }
    }

    void OnPlayerSeen(Vector2 position) {
        alertScript.enabled = true;
        alertScript.lastTarget = position;
        patrolScript.enabled = false;
        patrolScript.inRoute = false;
    }

    void OnChaseEnd() {
        alertScript.enabled = false;
        patrolScript.enabled = true;
        patrolScript.inRoute = false;
    }
}
