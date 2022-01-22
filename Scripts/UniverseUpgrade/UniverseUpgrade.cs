using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UniverseUpgrade : MonoBehaviour
{

    public PlayerInput playerInput;
    public Image telescopeImage;

    ////////////////////////////////
    
    private float currentDegree = 0;
    public bool isMotionLocked = true;
    int inputType = 99;

    void Awake() {
        playerInput.onInteract += spinTelescope;
    }

    public void spinTelescope() {
        if (!isMotionLocked) {
            inputType = playerInput.typeofInput;
            if (inputType == 1) {
                StartCoroutine(spinTelescopeImageAtDegreeOf(-120));
            } else if (inputType == 2) {
                StartCoroutine(spinTelescopeImageAtDegreeOf(120));
            }
        }
    }

    public IEnumerator spinTelescopeImageAtDegreeOf (float degree) {

        isMotionLocked = true;

        float goal = currentDegree + degree;
        while (Mathf.Abs(currentDegree - goal) >= 0.01) {
            currentDegree = Mathf.Lerp(currentDegree, goal, 0.05f);
            telescopeImage.transform.rotation = Quaternion.Euler(0, 0, currentDegree);
            yield return new WaitForSeconds(0.01f);
        }

        currentDegree = goal;
        isMotionLocked = false;
    }



    
}
