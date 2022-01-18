using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{

    void ShowStartScreen() {
        // 제목과 함께 시작 화면
    }

    void StartGame () {
        
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene("MainScene");
        }
    }
}
