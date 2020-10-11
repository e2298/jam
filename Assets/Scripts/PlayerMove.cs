using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

    private CharacterMove mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<CharacterMove>();
        mover.speed = walkSpeed;
    }


    private void OnWalk(InputValue value) {
        Debug.Log(value);
        mover.dir = value.Get<Vector2>();
    }

    private void OnRun(InputValue value) {
        if(value.Get<float>() == 1f) {
            mover.speed = runSpeed;
        }
        else {
            mover.speed = walkSpeed;
        }
    }
}
