using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ModeSwitchManager : UI_Interface
{

    public GameObject Shop;

    public GameObject Canvas;

    /////////////////////// 카메라 위치

    private Vector3 camDownPos = new Vector3 (0, -8, -2.04f);
    private Vector3 camOriginPos = new Vector3 (0, 0, -2.04f);
    private Vector3 camUpPos = new Vector3(0, 7, -2.04f);

    private Vector3 camLeftPos = new Vector3(-8, 0, -2.04f);
    private Vector3 camRightPos = new Vector3(8, 0, -2.04f);

    ///////////////////////////////////////// state pattern

    private Home home;
    private SpecificMode starCanvas;
    private SpecificMode shop;
    private SpecificMode HomeLeft;
    private SpecificMode HomeRight;
    public Node currentNode;
    private Node destination;

    //////////////////////////////////////// 모션

    private bool isMotionFinished = true;
    public bool isMotionSwitchEnabled = true;

    //////////////////////////////////////////

    void Start() {
        home = new Home("home", camOriginPos);
        
        starCanvas = new SpecificMode("canvas", camUpPos, Canvas);
        shop = new SpecificMode("shop", camDownPos, Shop);
        HomeLeft = new SpecificMode("homeLeft", camLeftPos, null);
        HomeRight = new SpecificMode("homeRight", camRightPos, null);

        List<Node> nodes = new List<Node>();

        nodes.Add(starCanvas);
        nodes.Add(shop);
        nodes.Add(HomeLeft);
        nodes.Add(HomeRight);

        home.nodes = nodes;
        currentNode = home;

        Shop.SetActive(false);
        Canvas.SetActive(false);
    } 

    public void switchState () {
        // 기능 켜주고 카메라 움직이기

        if (isMotionFinished) {
            if (currentNode.FeatureObject != null) {
            currentNode.FeatureObject.SetActive(false);
            } else { }
            if (destination.FeatureObject != null ) {
            destination.FeatureObject.SetActive(true);
            } else { }
            currentNode = destination;
            StartCoroutine(MoveCamera(destination.cameraPos));
        }
        isMotionFinished = true;
    }

    public void onInteract (int typeofOperation) {
        if (isMotionSwitchEnabled) {
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
    }

    public void returnToHome () {
        destination = home;
        switchState();
    }
    
    public void onSlideUp() {
        if (currentNode.name == "home") {
            destination = starCanvas;
            // StartCoroutine(MoveObject(Stars, starCanvas.cameraPos));
        }
        else if (currentNode.name == "shop") {
            destination = home;
        }
        switchState();
    }

    public void onSlideDown () {
        if (currentNode.name == "home") {
            destination = shop;
        }
        else if (currentNode.name == "canvas") {
            destination = home;
        }
        switchState();
    }

    public void onSlideRight () {
        if (currentNode.name == "home") {
            destination = HomeRight;
        }
        else if (currentNode.name == "homeLeft") {
            destination = home;
        }
        switchState();
    }

    public void onSlideLeft () {
        if (currentNode.name == "home") {
            destination = HomeLeft;
        }
        else if (currentNode.name == "homeRight") {
            destination = home;
        }
        switchState();
    }


    ///////////////////////////////////////////////////////////////

    IEnumerator MoveCamera(Vector3 cameraPos) { // 카메라를 움직이기 위한 것

        Camera main = Camera.main;

            isMotionFinished = false;

            while ( Vector3.Distance(main.transform.position, cameraPos) >= 0.1 ) {
                main.transform.position = Vector3.Lerp(main.transform.position, cameraPos, 0.01f);
                yield return new WaitForSeconds(0.001f);
            }   

        isMotionFinished = true;
    }

}


public class Node  {
    public string name;
    public Vector3 cameraPos; // 자신의 카메라 위치
    public GameObject FeatureObject {get; set;} // 각자의 기능을 담은 컴포넌트. 나중에 밖에서 켜고 꺼줄거임

}

public class Home : Node  {
    
    public List<Node> nodes {get; set;}

    public Home (string name, Vector3 cameraPos) {
        this.name = name;
        this.cameraPos = cameraPos;
    }


}

public class SpecificMode : Node {

    public Node home { get; set; }

    public SpecificMode (string name, Vector3 cameraPos, GameObject gameObject) {
        this.name = name;
        this.cameraPos = cameraPos;
        this.FeatureObject = gameObject;
    }

}
