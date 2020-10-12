using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGaem : MonoBehaviour
{
    public int nextScene;

    void OnStart() {
        SceneManager.LoadScene(nextScene); 
    }
}
