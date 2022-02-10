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
    private Vector2 previous;
    public GameObject start;
    public GameObject end;
    public GameObject[] control = new GameObject [2];
    public Vector2[] controlObjects = new Vector2 [2];

    void Awake() {
        numOfPoints = 2 * numOfLineSegment;
        timeInterval = 1f / ( numOfPoints + 1);

        controlObjects[0] = control[0].transform.position;
        controlObjects[1] = control[1].transform.position;
    }

    void Start () {
        StartCoroutine(drawBezierCurve(3, start.transform.position, end.transform.position, controlObjects));
    }

    public IEnumerator drawBezierCurve (int degree, Vector2 start, Vector2 end, Vector2[] controlPts) {

        Vector2 middlePoint = start;
        Vector2 previous = new Vector2(0, 0);
        float time = 0;
        float factor = 0;
        int currentIndex = 0;

        while (time < 1) {
            time += timeInterval;
            factor = 1 - time;

            if (degree == 2) {
                middlePoint = secondBezierEquation(time, start, end, controlPts[0]);
            }
            else if (degree == 3) {
                middlePoint = thridBezierEquation(time, start, end, controlPts);
            }

            currentIndex += 1;

            if (currentIndex % 2 == 0) {
                lineImageDrawer.drawLine(middlePoint, previous);
                Debug.DrawLine(middlePoint, previous, Color.white, 1000f);
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
