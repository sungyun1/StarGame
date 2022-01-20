using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCarouselManager : MonoBehaviour
{
    // 버튼 오브젝트들
    // 별을 회전시키는 함수

    public Button whiteBtn;
    public Button yellowBtn;
    public Button blueBtn;

    Transform whiteTransform;
    Transform blueTransform;
    Transform yellowTransform;

    public PlayerInput input;
    
    private bool isMotionLocked = false;

    private int frontButtonNum = 1;
    private List<Button> buttons;
    float currentDegree = 0;

    float radian = 180 / Mathf.PI;

    void Start () {
        input.onInteract += spinCarousel;

        whiteTransform = whiteBtn.transform;
        blueTransform = blueBtn.transform;
        yellowTransform = yellowBtn.transform;

        buttons = new List<Button>();
        buttons.Add(whiteBtn);
        buttons.Add(blueBtn);
        buttons.Add(yellowBtn);
    }

    void spinCarousel () {
        
        if (gameObject.activeSelf && !isMotionLocked) { 

            int type = input.typeofInput;
            float degreeNeedToSpin = 0;

            if (type == 3) {
                frontButtonNum = (frontButtonNum + 1) % 3;
                degreeNeedToSpin = 120f;
                // setButton();
                StartCoroutine(Spin(degreeNeedToSpin));
                
            }
            else if (type == 4) {
                frontButtonNum = (frontButtonNum - 1) % 3;
                degreeNeedToSpin = -120f;
                // setButton();
                StartCoroutine(Spin(degreeNeedToSpin));
            }
            else {
            
            }
            
        }
    }

    Vector3 calculatePosition (float degree) {
        float position = 213 * Mathf.Sin(degree);
        return new Vector3 (position, -100, 0);
    }

    Vector3 calculateMagnitude (float degree) {
        float tmp = Mathf.Cos(0.5f * degree);
        float mag = Mathf.Abs(tmp) + 0.7f;
        return new Vector3 (
            mag, mag, 0
        );
    }

    public IEnumerator Spin (float degreeNeedToSpin) {

        isMotionLocked = true;
        float tripleRadian = (2 * Mathf.PI) / 3f;

        float radianDegree = degreeNeedToSpin * Mathf.PI / 180;

        float goal = currentDegree + radianDegree;

        while (Mathf.Abs( currentDegree - goal ) >= 0.01f) {
            
            yellowTransform.localPosition = calculatePosition(currentDegree);
            whiteTransform.localPosition = calculatePosition(currentDegree + tripleRadian);
            blueTransform.localPosition = calculatePosition(currentDegree + tripleRadian * 2);
            
            yellowBtn.GetComponent<Image>().transform.localScale = calculateMagnitude(currentDegree);
            whiteBtn.GetComponent<Image>().transform.localScale = calculateMagnitude(currentDegree + tripleRadian);
            blueBtn.GetComponent<Image>().transform.localScale = calculateMagnitude(currentDegree + tripleRadian * 2);
        
            currentDegree = Mathf.Lerp(currentDegree, goal, 0.03f);
            yield return new WaitForSeconds(0.01f);
        }

        print(currentDegree);
        currentDegree = goal;
        
        isMotionLocked = false;
    }

    void setButton () {
        for (int i = 0; i < 3; i++) {
            var item = buttons[i].gameObject;
            if (i == frontButtonNum) {
                item.SetActive(true);
            } else item.SetActive(false);
        }
    }
    

}
