using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

    private CharacterMove mover;

    Animator an;
    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<CharacterMove>();
        mover.speed = walkSpeed;
        an = GetComponent<Animator>();
    }


    private void OnWalk(InputValue value) {
        var inputDir = value.Get<Vector2>();
        inputDir.Normalize();
        mover.dir = inputDir;
        an.SetFloat("leftVal", Mathf.Abs(Mathf.Min(inputDir.x, 0)));
        an.SetFloat("rightVal", Mathf.Abs(Mathf.Max(inputDir.x, 0)));
        an.SetFloat("frontVal", Mathf.Abs(Mathf.Min(inputDir.y, 0)));
        an.SetFloat("backVal", Mathf.Abs(Mathf.Max(inputDir.y, 0)));
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
