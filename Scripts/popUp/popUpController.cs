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

    private Popup popupScript;

    private Dictionary<string, string> popUpQuestions;

    public string popupResult;

    public event Action finished;

    void Awake() {
        popUpQuestions = new Dictionary<string, string>();

        popupScript = popup.GetComponent<Popup>();

        // 액션 등록 구간
        gameCanvas.openYesOrNoPopup += openYesOrNo;
        gameCanvas.showNextPopup += popupScript.callNextPopup;

        gameShop.showResultPopup += popupScript.callNextPopup;
    }

    void Start() {
        popup.SetActive(false);
    }

    public void openYesOrNo() {
        popupScript.switchState(Popup.popupState.binaryChoice);
        popup.SetActive(true);
    }

    public void openTripleChoice () {
        popupScript.switchState(Popup.popupState.tripletChoice);
        popup.SetActive(true);
    }

    public void openToastMessage (string message) {
        popupScript.toastMessage.text = message;
        StartCoroutine(popupScript.callToastMessage());
    }

    public void proceedWithKeyword(string result) {
        // 그냥 진행해라~
        popupResult = result;
        finished();
    }

}
