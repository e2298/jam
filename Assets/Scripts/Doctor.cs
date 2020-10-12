using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : MonoBehaviour
{
    public GameObject guards;
    public GameObject target;
    public Vector2 direction;
    public float visionrange;
    public float visionCone;

    public Sprite down;
    public Sprite up;
    public Sprite left;
    private SpriteRenderer sr;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        if(direction == Vector2.zero) {
            return;
        }
        if(Mathf.Abs(direction.y) > Mathf.Abs(direction.x)) {
            sr.flipX = false;
            if(direction.y > 0) {
                sr.sprite = up;
            }
            else {
                sr.sprite = down;
            }
        }
        else {
            sr.sprite = left;
            if(direction.x > 0) {
                sr.flipX = true;
            }
            else {
                sr.flipX = false;
            }
        }
    }

    private void FixedUpdate() {
        if (Vector2.Angle(direction, target.transform.position - transform.position) < visionCone / 2f) {
            var cast = Physics2D.Raycast(transform.position, target.transform.position - transform.position, visionrange);
            if(cast.collider && cast.collider.gameObject == target) {
                var p = target.transform.position;
                guards.BroadcastMessage("OnPlayerSeen", new Vector2(p.x, p.y));
            }
        }
    }

}
