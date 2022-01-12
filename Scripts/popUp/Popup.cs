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
    public GameObject TripleButton;
    public GameObject OKButton;
    public Text toastMessage;
    public Button Exit;

    // popup의 State를 변경하기 위한 친구들
    public enum popupState {
        binaryChoice = 0,
        description,
        tripletChoice,
        semiTransparent
    }

    private popupState currentState;

    ///////////////////////////

    public string result;

    // 버튼의 callback 들
    public delegate void Callback();

    private Callback OKCallback = null;
    private Callback YesCallback = null;
    private Callback NoCallback = null;
    private Callback WhiteCallback = null;
    private Callback YellowCallback = null;
    private Callback BlueCallback = null;

    
    void Awake() {
        switchState(popupState.tripletChoice);

        toastMessage.text = "돈이 부족합니다";
        toastMessage.gameObject.SetActive(false);

        GameObject okbtn = OKButton.transform.Find("OKButton").gameObject;
        okbtn.GetComponent<Button>().onClick.AddListener(close);

        GameObject yesbtn = BinaryButton.transform.Find("YesButton").gameObject;
        GameObject nobtn = BinaryButton.transform.Find("NoButton").gameObject;

        GameObject whiteStarBtn = TripleButton.transform.Find("whiteStarBuyBtn").gameObject;
        GameObject yellowStarBtn = TripleButton.transform.Find("yellowStarBuyBtn").gameObject;
        GameObject blueStarBtn = TripleButton.transform.Find("blueStarBuyBtn").gameObject;


        YesCallback += onYes;
        WhiteCallback += onFirstChoice;
        YellowCallback += onSecondChoice;
        BlueCallback += onThirdChoice;

        OKCallback += onOK;

        // 예를 누르면 보통 바로 꺼지지 않으므로 ~
        nobtn.GetComponent<Button>().onClick.AddListener(close);
        yesbtn.GetComponent<Button>().onClick.AddListener(close);
        whiteStarBtn.GetComponent<Button>().onClick.AddListener(close);
        blueStarBtn.GetComponent<Button>().onClick.AddListener(close);
        yellowStarBtn.GetComponent<Button>().onClick.AddListener(close);
        Exit.GetComponent<Button>().onClick.AddListener(() => {
            gameObject.SetActive(false);
        });
    }

    public void SetCallBack (Callback target, Callback call) {
        print("callback added");
        target += call;
    }

    public void switchState (popupState State) {
        switch(State) {
            case popupState.binaryChoice:
                popupText.SetActive(true);
                popupImage.SetActive(true);
                BinaryButton.SetActive(true);
                TripleButton.SetActive(false);
                OKButton.SetActive(false);
                break;
            case popupState.description:
                popupText.SetActive(true);
                popupImage.SetActive(true);
                BinaryButton.SetActive(false);
                TripleButton.SetActive(false);
                OKButton.SetActive(true);
                break;
            case popupState.tripletChoice:
                popupText.SetActive(true);
                popupImage.SetActive(false);
                BinaryButton.SetActive(false);
                TripleButton.SetActive(true);
                OKButton.SetActive(false);
                break;
        }
        currentState = State;
    }

    public void close() {
        switch (EventSystem.current.currentSelectedGameObject.name) {
            case "OKButton":
                OKCallback?.Invoke();
                gameObject.SetActive(false);
                break;
            case "YesButton":
                YesCallback?.Invoke();
                break;
            case "NoButton":
                NoCallback?.Invoke();
                gameObject.SetActive(false);
                break;
            case "whiteStarBuyBtn":
                WhiteCallback?.Invoke();

                break;
            case "yellowStarBuyBtn":
                YellowCallback?.Invoke();
                break;
            case "blueStarBuyBtn":
                BlueCallback?.Invoke();
                break;
            default:
                break;
        }

    }

    public void callNextPopup() {
        switchState(popupState.description);
    }

    public void onYes () {
        result = "yes";
        sendCompletionMessage();
    }

    public void onFirstChoice() {
        result = "white";
        sendCompletionMessage();
    }

    public void onSecondChoice () {
        result = "yellow";
        sendCompletionMessage();
    }

    public void onThirdChoice() {
        result = "blue";
        sendCompletionMessage();
    }

    public void onOK () {
        gameObject.SetActive(false);
    }

    public void sendCompletionMessage () {
        popUpController.proceedWithKeyword(result);
    }

    public IEnumerator callToastMessage () {

        toastMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        toastMessage.gameObject.SetActive(false);
    }

}