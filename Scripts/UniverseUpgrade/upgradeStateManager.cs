using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class upgradeStateManager : popupClient
{
    public ResourceManager gameResource;
    public popUpController popupController;

    public List<Button> buttons = new List<Button>();

    private string buyType = null;

    private delegate void Callback();

    void Awake() {
        for (int i = 0; i < buttons.Count; i++) {
            buttons[i].onClick.AddListener(onUpgradeButtonClicked);
        }

        popupController.finished += afterPopup;
    }

    public override void beforePopup(string type) {
        buyType = type;
        popupController.openSpecificTypeOfPopup("binary", "업그레이드를 진행하시겠습니까?");
    }

    public override void afterPopup() {
        if (gameObject.activeSelf) {
            if (gameResource.gameData.amountOfStarDust >= 100) {
                upgradeSelectedUniverse();
                popupController.openSpecificTypeOfPopup("description", "한 단계 더 깊은 우주로 들어갔습니다.");
            }
            else {
                popupController.openToastMessage("돈이 부족합니다");
            }
        }
    }

    void upgradeSelectedUniverse () {
        gameResource.onUpgradeUniverse(buyType);
    }

    public void onUpgradeButtonClicked () {

        GameObject selected = EventSystem.current.currentSelectedGameObject;
        int value = Int32.Parse(selected.transform.parent.gameObject.name);
        if (value == gameResource.gameData.normalUniverseLevel + 1) { // 다음 단계가 아니면
            beforePopup(selected.name); 
        } else {

        }
        
    }
    
}
