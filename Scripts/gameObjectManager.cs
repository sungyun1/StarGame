using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameObjectManager : MonoBehaviour
{
    // Start is called before the first frame update
    public ObjectPoolManager lineObjectPool;

    public ObjectPoolManager whitestarPool;
    

    public enum ObjectType {
        line = 0,
        whitestar,
        yellostar,
        bluestar
    }


}
