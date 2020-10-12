using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttackerGhost : MonoBehaviour
{
    public Vector2 spawn;
    public float range;
    public float AttackRange;
    public GameObject target;
    public float speed;
    public float angerySpeed;
    private CharacterMove mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<CharacterMove>();
    }

    void FixedUpdate()
    {
        if(target.activeSelf && ((Vector2)target.transform.position - (Vector2) transform.position).magnitude < AttackRange) {
            target.SendMessage("OnTransform");
            return;
        }


        if(target.activeSelf && ((Vector2)target.transform.position - spawn).magnitude < range) {
            mover.dir = (target.transform.position - transform.position).normalized;
            mover.speed = angerySpeed;
        }
        else if (((Vector2)transform.position - spawn).magnitude > 0.1f){
            mover.dir = (spawn - (Vector2)transform.position).normalized;
            mover.speed = speed;
        }
        else {
            mover.speed = 0;
        }
    }
}
