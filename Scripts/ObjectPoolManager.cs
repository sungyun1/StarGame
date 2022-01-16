using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    
    public GameObject ObjectPrefab;

    private Queue<GameObject> pool = new Queue<GameObject>();

    public void setObjectPrefab (GameObject pref) {
        ObjectPrefab = pref;
    }

    bool isQueueEmpty () {
        if (pool.Count == 0) return true;
        else return false;
    }

    public GameObject pullObjectFromPoolTo (GameObject parent) {
        if (isQueueEmpty()) {
            GameObject newObj = Instantiate(ObjectPrefab);
            newObj.transform.SetParent(parent.transform);
            newObj.SetActive(true);
            return newObj;
        }
        return pool.Dequeue();
    }

    public void returnObjectToPool (GameObject obj) {
        obj.SetActive(false);
        obj.transform.SetParent(null);
        pool.Enqueue(obj);
    }
}

