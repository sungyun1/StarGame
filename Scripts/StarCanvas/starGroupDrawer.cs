using UnityEngine;
using System;

public class starGroupDrawer : MonoBehaviour {
    
    public gameObjectManager pool;
    
    public GameObject LineFolder;

    private Star buffer;
    LineRenderer lr;

    private Vector3 coefficient = new Vector3(0.21f, 0.25f, 0);

    public void drawLine(Vector3 p1, Vector3 p2) 
    {
        Vector3 fixed1 = p1 + coefficient;
        Vector3 fixed2 = p2 + coefficient;

        GameObject line = pool.chooseTypeOfPool("line").pullObjectFromPoolTo(LineFolder);
        lr = line.GetComponent<LineRenderer>();
        lr.positionCount += 1;
        lr.SetPosition(0, fixed1);
        lr.positionCount += 1;
        lr.SetPosition(1, fixed2);
    }
}