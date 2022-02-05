using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StartGame();
        }
    }

    void StartGame () {
        SceneManager.LoadScene("Story");
    }
    
}
