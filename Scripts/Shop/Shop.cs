using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Shop : popupClient
{
    
    // 필요한 외부 오브젝트
    public GameObject starLocation;
    public popUpController popupController;
    public ResourceManager gameResource;

    public gameObjectManager pool;

    // 스트레티지 목록
    private Strategy currentStrategy;
    private Strategy buyStarStrategy;
    private Strategy startGatchaStrategy;
    private Strategy upgradeBoyStrategy;

    // 내부 변수
    private Dictionary<string, int> price = new Dictionary<string, int>();


    ///////////////////////////

    void Start () {
        price.Add("white", 100);
        price.Add("yellow", 200);
        price.Add("blue", 300);

        buyStarStrategy = gameObject.AddComponent<BuyStarStrategy>();
        startGatchaStrategy = gameObject.AddComponent<startGatchaStrategy>();
        upgradeBoyStrategy = gameObject.AddComponent<upgradeBoyStrategy>();

        popupController.finished += afterPopup;
    }

    public override void beforePopup(string popupTypeName) {
        chooseCurrentStrategyWithKeyword(popupTypeName);
        currentStrategy.openPopupUsing( popupController );
    }

    public override void afterPopup () {

        string keyword = popupController.popupResult;
        int amount = popupController.detailPopupResult;
        
        if (gameObject.activeSelf) {

            int priceOfProduct = price[keyword];

            for (int i = 0; i < amount; i++) {
                if (gameResource.gameData.amountOfStarDust >= priceOfProduct ) {
                    currentStrategy.buy( keyword , pool );
                    gameResource.onBuyStar( keyword , priceOfProduct );
                    popupController.openSpecificTypeOfPopup("description", "별 구매가 완료되었습니다!");
                }
                else {
                    popupController.openToastMessage("돈이 부족합니다");
                    i = amount + 1;
                }
            }
            
        }
    }

    public void chooseCurrentStrategyWithKeyword (string name) {
        switch (name) {
            case "buyStarBtn":
                currentStrategy = buyStarStrategy;
                break;
            case "gatchaBtn":
                currentStrategy = startGatchaStrategy;
                break;
            case "upgradeBoyBtn":
                currentStrategy = upgradeBoyStrategy;
                break;
            default:
                break;
        }
    }

}

////////////////////////////////////////////////////////////////

abstract class Strategy : MonoBehaviour{
    public abstract void openPopupUsing( popUpController popup );
    public abstract void buy (string type, gameObjectManager pool) ;
}

class BuyStarStrategy : Strategy {

    public GameObject whereStarIsLocated; 

    private GameObject prefab = null;

    private int starindex = 0;
    void Start () {
        whereStarIsLocated = GameObject.FindWithTag("Stars").gameObject;
    }

    public override void openPopupUsing( popUpController popup  ) {
        popup.openSpecificTypeOfPopup("triplet", "별을 구매하시겠습니까?");
    }

    public override void buy(string type, gameObjectManager pool) {


        GameObject newStar = pool.chooseTypeOfPool(type).pullObjectFromPoolTo(whereStarIsLocated);
        newStar.SetActive(true);

        Vector3 pos = new Vector3(
            UnityEngine.Random.Range(-2f, 2f) + whereStarIsLocated.transform.position.x,
            UnityEngine.Random.Range(-0.5f, 4f) + whereStarIsLocated.transform.position.y,
            1
        );

        newStar.transform.position = pos;
        Star str = newStar.GetComponent<Star>();
        str.index = starindex;
        str.type = type;
        str.isUsed = false;
        starindex++;
    }
}

class startGatchaStrategy : Strategy {

    public override void openPopupUsing( popUpController popup ) {
        
    }
    public override void buy(string type, gameObjectManager pool ) {
    }
}

class upgradeBoyStrategy : Strategy {

    public override void openPopupUsing( popUpController popup ) {
        
    }
    public override void buy(string type, gameObjectManager pool) {
    }
}