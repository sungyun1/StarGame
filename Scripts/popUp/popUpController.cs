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
    public int detailPopupResult;

    public event Action finished;

    void Awake() {
        popUpQuestions = new Dictionary<string, string>();

        popupScript = popup.GetComponent<Popup>();
    }

    void Start() {
        popup.SetActive(false);
    }

    public void openSpecificTypeOfPopup (string type, string message) {
        popupScript.popupText.text = message;
        switch (type) {
            case "binary":
                popupScript.switchState(Popup.popupState.binaryChoice);
                break;
            case "triplet":
                popupScript.switchState(Popup.popupState.tripletChoice);
                break;
            case "description":
                popupScript.switchState(Popup.popupState.description);
                break;
            default:
                throw new Exception("state of Popup is not allocated");
        }
        if (popup.activeSelf != true) {
            popup.SetActive(true);
        }
    }

    public void openToastMessage (string message) {
        popupScript.toastMessage.text = message;
        StartCoroutine(popupScript.callToastMessage());
    }

    public void proceedWithKeyword(string result) {
        // 그냥 진행해라~
        popupResult = result;
        detailPopupResult = popupScript.currentStarInShopping;
        finished();
    }

}
