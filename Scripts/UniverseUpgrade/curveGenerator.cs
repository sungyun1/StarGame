using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curveGenerator : MonoBehaviour
{

    public LineImageDrawer lineImageDrawer;

    // number of points between start & end = 2 * (numOfLineSegment - 1)
    private int numOfLineSegment = 7;
    private int numOfPoints;
    private float timeInterval;
    private float speedOfLine = 0.05f;

    ////////////////////////////////////////////////////

    public int currentStage = 4;
    
    public List<GameObject> planets = new List<GameObject>();
    public List<GameObject> controlPoints = new List<GameObject>();
    public Vector2[] controlObjects = new Vector2 [2];

    void Awake() {
        numOfPoints = 2 * numOfLineSegment;
        timeInterval = 1f / ( numOfPoints + 1);
    }

    public IEnumerator drawCurrentSpaceMapProgress () { // 움직이고 난 후에 control pt를 잡아야 함.

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < currentStage; i++) {
            controlObjects[0] = controlPoints[2 * i].transform.position;
            controlObjects[1] = controlPoints[2 * i + 1].transform.position;
            StartCoroutine(drawBezierCurve(3, i));
            yield return new WaitForSeconds(1.5f);
        }
        
    }

    public IEnumerator drawBezierCurve (int degree, int stageNum) {

        Vector2 startPt = planets[stageNum].transform.position;
        Vector2 endPt = planets[stageNum + 1].transform.position;
        Vector2[] controlPts = controlObjects;

        Vector2 middlePoint = startPt;
        Vector2 previous = new Vector2(0, 0);
        float time = 0;
        float factor = 0;
        int currentIndex = 0;

        while (time < 1) {
            time += timeInterval;
            factor = 1 - time;

            if (degree == 2) {
                middlePoint = secondBezierEquation(time, startPt, endPt, controlPts[0]);
            }
            else if (degree == 3) {
                middlePoint = thridBezierEquation(time, startPt, endPt, controlPts);
            }

            currentIndex += 1;

            if (currentIndex % 2 == 0) {
                lineImageDrawer.drawLine(previous, middlePoint);
                // Debug.DrawLine(middlePoint, previous, Color.white, 1000f);
            } else {
                previous = middlePoint;
            }
            yield return new WaitForSeconds(speedOfLine);
        }
    }

    Vector2 secondBezierEquation (float time, Vector2 start, Vector2 end, Vector2 controlPt) {
        float factor = 1 - time;
        return Mathf.Pow(factor, 2) * start + 2 * factor * time * controlPt + Mathf.Pow(time, 2) * end;
    }

    Vector2 thridBezierEquation (float time, Vector2 start, Vector2 end, Vector2[] controlPt) {
        float factor = 1 - time;

        return Mathf.Pow(factor, 3) * start + 3 * Mathf.Pow(factor, 2) * time * controlPt[0] + 3 * Mathf.Pow(time, 2) * factor * controlPt[1] + Mathf.Pow(time, 3) * end;
    }
}
