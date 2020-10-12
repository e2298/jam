using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysBodyActions : MonoBehaviour
{
    public GameObject ghost;
    private CameraFollow fl;
    // Start is called before the first frame update
    void Start()
    {
        fl = Camera.main.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTransform() {
        ghost.transform.position = transform.position;
        ghost.SetActive(true);
        ghost.GetComponent<GhostActions>().body = gameObject;
        gameObject.SetActive(false);
        fl.target = ghost;
    }
}
