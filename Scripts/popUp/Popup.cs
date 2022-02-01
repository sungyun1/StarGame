using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public enum currentDescriptionState {
    whenBoughtStar = 0,
    whenCreatedStarGroup,
    whenUpgradedUniverse,
    whenUpgradedPlayer,
    notEnoughStarDust,
    notEnoughGem,
}

public class Popup : MonoBehaviour {

    public popUpController popUpController;
    public popupDialogueManager dialogueManager;

/////////////////////////////////////////////////////
    public GameObject popupTextObject;
    public Text popupText;
    public GameObject popupImage;
    public GameObject BinaryButton;
    public GameObject TripleButton;
    public GameObject OKButton;
    public Text toastMessage;
    public Button Exit;
    public GameObject amountOfStar;
    public Text amountOfStarBuying;
    public int currentStarInShopping = 0;

    // popup의 State를 변경하기 위한 친구들
    public enum popupState {
        binaryChoice = 0,
        description,
        tripletChoice,
        semiTransparent
    }

    private popupState currentState;
    private currentDescriptionState descriptionState;

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

    public event Action OKPressed;

    void Awake() {
        switchState(popupState.tripletChoice);

        amountOfStarBuying.text = currentStarInShopping.ToString();

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

    void Update () {
        if (amountOfStar.activeSelf) {
            amountOfStarBuying.text = currentStarInShopping.ToString();
        }
    }

    public void SetCallBack (Callback target, Callback call) {
        print("callback added");
        target += call;
    }

    public void switchState (popupState State) {
        switch(State) {
            case popupState.binaryChoice:
                popupTextObject.SetActive(true);
                popupImage.SetActive(true);
                BinaryButton.SetActive(true);
                TripleButton.SetActive(false);
                OKButton.SetActive(false);
                amountOfStar.SetActive(false);
                break;
            case popupState.description:
                popupTextObject.SetActive(true);
                popupImage.SetActive(true);
                BinaryButton.SetActive(false);
                TripleButton.SetActive(false);
                OKButton.SetActive(true);
                amountOfStar.SetActive(false);
                break;
            case popupState.tripletChoice:
                popupTextObject.SetActive(true);
                popupImage.SetActive(false);
                BinaryButton.SetActive(false);
                TripleButton.SetActive(true);
                OKButton.SetActive(false);
                amountOfStar.SetActive(true);
                
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
                descriptionState = currentDescriptionState.whenBoughtStar;
                WhiteCallback?.Invoke();
                break;
            case "yellowStarBuyBtn":
                descriptionState = currentDescriptionState.whenBoughtStar;
                YellowCallback?.Invoke();
                break;
            case "blueStarBuyBtn":
                descriptionState = currentDescriptionState.whenBoughtStar;
                BlueCallback?.Invoke();
                break;
            case "plus":

            default:
                break;
        }

    }

    public void callNextPopup() {
        choosePopupMessage();
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
        OKPressed();
        gameObject.SetActive(false);
    }

    public void onPlusButton () {
        currentStarInShopping += 1;
    }

    public void onMinusButton() {
        if (currentStarInShopping != 0 ) {
            currentStarInShopping -= 1;
        } else {}
    }

    public void sendCompletionMessage () {
        popUpController.proceedWithKeyword(result);
    }

    public IEnumerator callToastMessage () {

        toastMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        toastMessage.gameObject.SetActive(false);
    }

    void choosePopupMessage ( ) {
        popupText.text = dialogueManager.chooseMessage(descriptionState);
    }

}