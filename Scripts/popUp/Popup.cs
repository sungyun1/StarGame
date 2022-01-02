using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface IpopupSender {
    void beforePopup(); // 팝업 이전에 실행할 함수
    void afterPopup();  // 팝업 이후에 실행할 함수
}

public class Popup : MonoBehaviour {

    public popUpController popUpController;

    public GameObject popupText;
    public GameObject popupImage;
    public GameObject BinaryButton;
    public GameObject OKButton;


    // popup의 State를 변경하기 위한 친구들
    // + 반투명 팝업 , 별 3개짜리 팝업 구현하기
    public enum popupState {
        binaryChoice = 0,
        description,
        tripletChoice,
        semiTransparent
    }

    private popupState currentState;

    // 버튼의 callback 들
    public delegate void Callback();

    private Callback OKCallback = null;
    private Callback YesCallback = null;
    private Callback NoCallback = null;

    
    void Awake() {
        switchState(popupState.binaryChoice);

        GameObject okbtn = OKButton.transform.Find("OKButton").gameObject;
        okbtn.GetComponent<Button>().onClick.AddListener(close);

        GameObject yesbtn = BinaryButton.transform.Find("YesButton").gameObject;
        GameObject nobtn = BinaryButton.transform.Find("NoButton").gameObject;

        // 예를 누르면 보통 바로 꺼지지 않으므로 ~
        // yesbtn.GetComponent<Button>().onClick.AddListener(close);
        nobtn.GetComponent<Button>().onClick.AddListener(close);

        SetCallBack(YesCallback, onYes);
    }

    public void SetCallBack (Callback call, Callback target) {
        target += call;
    }

    public void switchState (popupState State) {
        switch(State) {
            case popupState.binaryChoice:
                popupText.SetActive(true);
                popupImage.SetActive(true);
                BinaryButton.SetActive(true);
                OKButton.SetActive(false);
                break;
            case popupState.description:
                popupText.SetActive(true);
                popupImage.SetActive(true);
                BinaryButton.SetActive(false);
                OKButton.SetActive(true);
                break;
        }
        currentState = State;
    }

    
    public void close() {
        switch (EventSystem.current.currentSelectedGameObject.name) {
            case "OKButton":
                OKCallback?.Invoke();
                OKCallback = null;
                break;
            case "YesButton":
                YesCallback?.Invoke();
                YesCallback = null;
                break;
            case "NoButton":
                NoCallback?.Invoke();
                NoCallback = null;
                break;
        }

        switchState(popupState.binaryChoice);
        gameObject.SetActive(false);
    }

    public void callNextPopup() {
        switchState(popupState.description);
    }

    public void onYes () {
        // 하던 거 그대로 진행해 ~
        popUpController.proceed();
        callNextPopup();
    }

}