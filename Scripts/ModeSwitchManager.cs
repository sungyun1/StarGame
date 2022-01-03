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

    ///////////////////////

    private Vector3 camDownPos = new Vector3 (0, -8, -2.04f);
    private Vector3 camOriginPos = new Vector3 (0, 0, -2.04f);
    private Vector3 camUpPos = new Vector3(0, 7, -2.04f);

    private Vector3 camLeftPos = new Vector3(-8, 0, -2.04f);
    private Vector3 camRightPos = new Vector3(8, 0, -2.04f);

    /////////////////////////////////////////

    private Home home;
    private SpecificMode starCanvas;
    private SpecificMode shop;
    private SpecificMode HomeLeft;
    private SpecificMode HomeRight;
    private Node currentNode;

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

        // 초기 설정

        home = new Home("home", camOriginPos);
        
        starCanvas = new SpecificMode("starCanvas", camUpPos, Canvas);
        shop = new SpecificMode("shop", camDownPos, Shop);
        HomeLeft = new SpecificMode("homeLeft", camLeftPos, null);
        HomeRight = new SpecificMode("homeRight", camRightPos, null);

        List<Node> nodes = new List<Node>();

        nodes.Add(starCanvas);
        nodes.Add(shop);
        nodes.Add(HomeLeft);
        nodes.Add(HomeRight);

        print(nodes[0].GetType());

        home.nodes = nodes;
        currentNode = home;

        currentState = StateName.Home;
        Shop.SetActive(false);
        Canvas.SetActive(false);
    } 

    void switchState (Node fromNode, Node toNode) {
        // 기능 켜주고 카메라 움직이기
        if (fromNode.FeatureObject == null ) { }
        else {
            fromNode.FeatureObject.SetActive(false);
        }

        if (toNode.FeatureObject == null ) { }
        else {
            toNode.FeatureObject.SetActive(true);
        }

        StartCoroutine(MoveCamera(toNode.cameraPos));

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

        if (currentNode.name == "home") {
            switchState(currentNode, starCanvas);
        }
        else if (currentNode.name == "shop") {
            switchState(currentNode, home);
        }

        /*
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
        */

    }

    void onSlideDown () {
        
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

    void onSlideRight () {
        if (currentState == StateName.Home) {
            HomeToHomeLeft();
        }
        else if (currentState == StateName.HomeRight) {
            StartCoroutine(MoveCamera(camOriginPos));
        }
        
    }

    void onSlideLeft () {
        if (currentState == StateName.Home) {
            HomeToHomeRight();
        }
        else if (currentState == StateName.HomeLeft) {
            StartCoroutine(MoveCamera(camOriginPos));
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
        StartCoroutine(MoveCamera(camDownPos));
    }

    void ShopToHome () {
        StartCoroutine(MoveCamera(camOriginPos));
    }

    void HomeToCanvas () {
        StartCoroutine(MoveCamera(camUpPos));
        StartCoroutine(MoveObject(Stars, camUpPos));
    }

    void CanvasToHome () {
        StartCoroutine(MoveCamera(camOriginPos));
        StartCoroutine(MoveObject(Stars, camOriginPos));
    }

    void HomeToHomeLeft () {
        StartCoroutine(MoveCamera(camLeftPos));
    }

    void HomeToHomeRight () {
        StartCoroutine(MoveCamera(camRightPos));
    }


    ///////////////////////////////////////////////////////////////

    IEnumerator MoveObject (GameObject Page, Vector3 Destination) {

        while (Vector3.Distance(Page.transform.position, Destination) >= 0.1) {
            Page.transform.position = Vector3.Lerp(Page.transform.position, Destination, 0.01f);
            yield return new WaitForSeconds(0.001f);
        }
    }

    public IEnumerator MoveCamera(Vector3 cameraPos) { // 카메라를 움직이기 위한 것

        Vector3 mainPos = Camera.main.transform.position;

        while ( Vector3.Distance(mainPos, cameraPos) >= 0.1 ) {
            mainPos = Vector3.Lerp(mainPos, cameraPos, 0.01f);
            yield return new WaitForSeconds(0.001f);
        }
    }

}

public interface NodeInterface {
    IEnumerator MoveCamera();
}

public interface HomeInterface {
    Node GetNode(string name);
}

public interface ModeInterface {
    Home returnToHome();
}

public class Node : NodeInterface {
    public string name;
    public Vector3 cameraPos; // 자신의 카메라 위치
    public GameObject FeatureObject {get; set;} // 각자의 기능을 담은 컴포넌트. 나중에 밖에서 켜고 꺼줄거임

    public IEnumerator MoveCamera() { // 카메라를 움직이기 위한 것

        Vector3 mainPos = Camera.main.transform.position;

        while ( Vector3.Distance(mainPos, cameraPos) >= 0.1 ) {
            mainPos = Vector3.Lerp(mainPos, cameraPos, 0.01f);
            yield return new WaitForSeconds(0.001f);
        }
    }
}

public class Home : Node, HomeInterface  {
    
    public List<Node> nodes {get; set;}

    public Home (string name, Vector3 cameraPos) {
        this.name = name;
        this.cameraPos = cameraPos;
    }

    public Node GetNode(string name) {
        foreach ( Node node in nodes ) {
            if (node.name == name) {
                return node;
            }
        }
        return null;
    }

}

public class SpecificMode : Node, ModeInterface {

    public Home home { get; set; }

    public SpecificMode (string name, Vector3 cameraPos, GameObject gameObject) {
        this.name = name;
        this.cameraPos = cameraPos;
        this.FeatureObject = gameObject;
    }

    public Home returnToHome() {
        return home;
    }
}
