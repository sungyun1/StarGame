using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gameObjectManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Queue<GameObject> linePool = new Queue<GameObject>();

    private Queue<GameObject> lineImagePool = new Queue<GameObject>();

    public Queue<GameObject> whiteStarPool = new Queue<GameObject>();
    private Queue<GameObject> blueStarPool = new Queue<GameObject>();
    private Queue<GameObject> yellowStarPool = new Queue<GameObject>();

    private Queue<GameObject> currentPool;

    ///////////////////////////////////////////////

    public GameObject linePrefab;
    public GameObject lineImagePrefab;
    public GameObject whiteStarPrefab;
    public GameObject yellowStarPrefab;
    public GameObject blueStarPrefab;

    private GameObject prefab;

    ///////////////////////////////////////////////////////

    public gameObjectManager chooseTypeOfPool (string type) {

        switch (type) {
            case "line":
                currentPool = linePool;
                prefab = linePrefab;
                break;
            case "white":
                currentPool = whiteStarPool;
                prefab = whiteStarPrefab;
                break;
            case "yellow":
                currentPool = yellowStarPool;
                prefab = yellowStarPrefab;
                break;
            case "blue":
                currentPool = blueStarPool;
                prefab = blueStarPrefab;
                break;
            case "lineImage":
                currentPool = lineImagePool;
                prefab = lineImagePrefab;
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
        GameObject newObj;
        if (isQueueEmpty()) {
            newObj = Instantiate(prefab);
        }
        else newObj = currentPool.Dequeue();
        
        newObj.transform.SetParent(parent.transform);
        newObj.SetActive(true);
        return newObj;
    }

    public void returnObjectToPool (GameObject obj) {
        obj.SetActive(false);
        obj.transform.SetParent(null);
        currentPool.Enqueue(obj);
    }

    public void returnGroupOfObjectToPool (GameObject folder) {
        int num = folder.transform.childCount;

        for (int i = 0; i < num; i++) {
            GameObject target = folder.transform.GetChild(0).gameObject;
            returnObjectToPool(target);
        }
    }

}
