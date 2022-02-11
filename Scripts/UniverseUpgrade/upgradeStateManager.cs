using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class upgradeStateManager : popupClient
{
    public ResourceManager gameResource;
    public popUpController popupController;

    private string buyType = null;

    public override void beforePopup(string type) {
        buyType = type;
        popupController.openSpecificTypeOfPopup("binary", "업그레이드를 진행하시겠습니까?");
    }

    public override void afterPopup() {
        if (gameObject.activeSelf) {
            if (gameResource.gameData.amountOfStarDust >= 100) {
                upgradeSelectedUniverse();
                popupController.openSpecificTypeOfPopup("description", "구매가 완료되었습니다");
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
        beforePopup(EventSystem.current.currentSelectedGameObject.name); 
    }
    
}
