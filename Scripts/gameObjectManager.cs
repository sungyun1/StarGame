using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType {
        line = 0,
        whiteStar,
        yellowStar,
        blueStar
    }

public class gameObjectManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Queue<GameObject> linePool = new Queue<GameObject>();

    public Queue<GameObject> whiteStarPool = new Queue<GameObject>();
    private Queue<GameObject> blueStarPool = new Queue<GameObject>();
    private Queue<GameObject> yellowStarPool = new Queue<GameObject>();

    private Queue<GameObject> currentPool;

    ///////////////////////////////////////////////

    public GameObject linePrefab;
    public GameObject whiteStarPrefab;
    public GameObject yellowStarPrefab;
    public GameObject blueStarPrefab;

    private GameObject prefab;

    
    // 메소드 체이닝

    public gameObjectManager chooseTypeOfPool (ObjectType type) {

        switch (type) {
            case ObjectType.line:
                currentPool = linePool;
                prefab = linePrefab;
                break;
            case ObjectType.whiteStar:
                currentPool = whiteStarPool;
                prefab = whiteStarPrefab;
                break;
            case ObjectType.yellowStar:
                currentPool = yellowStarPool;
                prefab = yellowStarPrefab;
                break;
            case ObjectType.blueStar:
                currentPool = blueStarPool;
                prefab = blueStarPrefab;
                break;
            default:
                break;
        }

        return this;
    }

    bool isQueueEmpty () {
        if (currentPool.Count == 0) return true;
        else return false;
    }

    public GameObject pullObjectFromPoolTo (GameObject parent) {
        if (isQueueEmpty()) {
            GameObject newObj = Instantiate(prefab);
            newObj.transform.SetParent(parent.transform);
            newObj.SetActive(true);
            return newObj;
        }
        return currentPool.Dequeue();
    }

    public void returnObjectToPool (GameObject obj) {
        obj.SetActive(false);
        obj.transform.SetParent(null);
        currentPool.Enqueue(obj);
    }

}
