using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ModeSwitchManager : MonoBehaviour
{

    private Dictionary<string, List<IEnumerator>> transition; // 동작 이름, 실제로 움직일 방법

    public GameObject Shop;

    public GameObject Canvas;

    public GameObject Stars;

    public Camera cam;

    private bool isCameraOnPosition = true;
    private Vector3 openPos = new Vector3 (1080 / 2, 1920 / 2, 0);

    private Vector3 closePos = new Vector3 (1080 / 2 , - 1920 / 2, 0);

    private Vector3 camDownPos = new Vector3 (0, -8, -2.04f);
    private Vector3 camOriginPos = new Vector3 (0, 0, -2.04f);
    private Vector3 camUpPos = new Vector3(0, 7, -2.04f);

    private Vector3 camLeftPos = new Vector3(-8, 0, -2.04f);
    private Vector3 camRightPos = new Vector3(8, 0, -2.04f);

    public enum StateName  {
        Home,
        Canvas,
        Shop,
        Diary,
        HomeLeft,
        HomeRight
    }
    public StateName currentState;

    void Start() {
        currentState = StateName.Home;
        Shop.SetActive(false);
        Canvas.SetActive(false);
    }  

    public void onInteract (int typeofOperation) {
        if (typeofOperation == 1) { // slide up
            onSlideUp();
        } else if (typeofOperation == 2) { // slide down
            onSlideDown();
        }
        else if (typeofOperation == 3) { // slide right
            onSlideRight();
        }   
        else if (typeofOperation == 4) { // slide left 
            onSlideLeft();
        }
    }
    
    void onSlideUp() {

        if (currentState == StateName.Canvas) {
            currentState = StateName.Home;
            Canvas.SetActive(false);
            CanvasToHome();
        }

        else if (currentState == StateName.Home) {
            currentState = StateName.Shop;
            Shop.SetActive(true);
            HomeToShop();
        }

    }

    void onSlideDown () {
        if (isCameraOnPosition) {
            if (currentState == StateName.Home) {
                currentState = StateName.Canvas;
                Canvas.SetActive(true);
                HomeToCanvas();
            }
            else if (currentState == StateName.Shop) {
                currentState = StateName.Home;
                Shop.SetActive(false);
                ShopToHome();
            }
        }
    }

    void onSlideRight () {
        if (currentState == StateName.Home) {
            HomeToHomeLeft();
        }
        else if (currentState == StateName.HomeRight) {
            StartCoroutine(MoveCamera(cam, camOriginPos));
        }
        
    }

    void onSlideLeft () {
        if (currentState == StateName.Home) {
            HomeToHomeRight();
        }
        else if (currentState == StateName.HomeLeft) {
            StartCoroutine(MoveCamera(cam, camOriginPos));
        }
    }

    public void addMotion (string motionName, IEnumerator ie) {
        transition[motionName].Add(ie);
    }

    public void createMotion (string motionName) {
        transition.Add(motionName, new List<IEnumerator>());
    }

    // 대리자를 통해서 수행하는 걸로 바꾸자 !!

    void HomeToShop () {
        StartCoroutine(MoveCamera(cam, camDownPos));
    }

    void ShopToHome () {
        StartCoroutine(MoveCamera(cam, camOriginPos));
    }

    void HomeToCanvas () {
        StartCoroutine(MoveCamera(cam, camUpPos));
        StartCoroutine(MoveObject(Stars, camUpPos));
    }

    void CanvasToHome () {
        StartCoroutine(MoveCamera(cam, camOriginPos));
        StartCoroutine(MoveObject(Stars, camOriginPos));
    }

    void HomeToHomeLeft () {
        StartCoroutine(MoveCamera(cam, camLeftPos));
    }

    void HomeToHomeRight () {
        StartCoroutine(MoveCamera(cam, camRightPos));
    }


    ///////////////////////////////////////////////////////////////

    IEnumerator MoveObject (GameObject Page, Vector3 Destination) {

        while (Vector3.Distance(Page.transform.position, Destination) >= 0.1) {
            Page.transform.position = Vector3.Lerp(Page.transform.position, Destination, 0.01f);
            yield return new WaitForSeconds(0.001f);
        }
    }

    IEnumerator MoveCamera (Camera cam, Vector3 Destination) {

        while (Vector3.Distance(cam.transform.position, Destination) >= 0.1) {
            cam.transform.position = Vector3.Lerp(cam.transform.position, Destination, 0.01f);
            yield return new WaitForSeconds(0.001f);
        }
    }

}



