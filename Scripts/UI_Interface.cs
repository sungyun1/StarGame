using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UI_Interface : MonoBehaviour
{

    public bool isMotionLocked = false;

    public IEnumerator MoveObject (GameObject obj, Vector3 Destination) {

        isMotionLocked = true;

        while (Vector3.Distance(obj.transform.position, Destination) >= 0.1) {
            obj.transform.position = Vector3.Lerp(obj.transform.position, Destination, 0.01f);
            yield return new WaitForSeconds(0.001f);
        }

        isMotionLocked = false;
    }

    public IEnumerator Fade (GameObject obj, float goal) {

        isMotionLocked = true;

        if (obj.GetComponent<Image>() == null) {
            throw new Exception("object has no image component");
        }
        else {
            Image img = obj.GetComponent<Image>();
            float cnt = img.color.a;
            while (Mathf.Abs(goal - cnt) >= 0.01f) {
                img.color = new Color(0, 0, 0, cnt);
                //cnt = Mathf.Lerp(cnt, goal, 0.0001f);
                cnt += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
        }

        isMotionLocked = false;
    }
}
