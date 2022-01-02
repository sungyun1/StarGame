using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{

    public int typeofInput;
    public Vector3 startPos;
    public Vector3 endPos;

    private Vector3 currentPos;

    public Vector3 result;

    public Camera cam;


    public event Action onInteract;
    
    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            currentPos = cam.ScreenToWorldPoint(Input.mousePosition);
            startPos = currentPos;
        }
        if (Input.GetMouseButton(0)) {
            currentPos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0)) {
            endPos = currentPos;
            result = endPos - startPos;
            typeofInput = DetermineInputType();
            onInteract();
        }
    }

    int DetermineInputType () {

        Vector3 res = result;

        if (res.x < 0.5 && res.y < 0.5 && res.y > - 0.5) return 0;
        if (res.x < 0.5 && res.y > 1) return 1;
        if (res.x < 0.5 && res.y < -1) return 2;
        else return 99;
    }


}
