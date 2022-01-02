using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    // 일기장 관련 제어 diaryManager

    public PlayerInput playerInput;

    public ModeSwitchManager gameMode;

    public GameObjectManager gameObjectManager;


    // 인터랙션이 일어났을 경우

    void Start()
    {
        playerInput.onInteract += whenInteract;
    }
    
    void whenInteract () {
        int currentOperation = playerInput.typeofInput;

        if (currentOperation == 0) {
            Vector3 position = playerInput.endPos;
            gameObjectManager.onInteract(position);
        }
        else {
            gameMode.onInteract(currentOperation);
        }
    }  
}
