using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class popUpController : MonoBehaviour
{
    // 상황에 맞게 팝업을 켜고 종료한다.
    // 각 개체로부터 요청을 받아서 그에 맞게 실행한다.

    public Shop gameShop;
    public StarCanvas gameCanvas;

    public GameObject popup;

    private Dictionary<string, string> popUpQuestions;

    public event Action finished;

    public Stack tmpStack = new Stack();

    void Awake() {
        popUpQuestions = new Dictionary<string, string>();

        // 액션 등록 구간
        gameCanvas.openYesOrNoPopup += openYesOrNo;
        gameShop.openYesOrNoPopup += openYesOrNo;
    }

    void Start() {
        popup.SetActive(false);
    }

    void openYesOrNo() {
        popup.SetActive(true);
    }

    public void proceed() {
        // 그냥 진행해라~
        finished();
    }

}
