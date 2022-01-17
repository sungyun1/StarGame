using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour
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

    public event Action showResultPopup;

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

    public void chooseBehaviourWithKeyword (string name) {
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

        currentStrategy.openPopupUsing( popupController );
    }
    
    public void afterPopup () {

        string keyword = popupController.popupResult;
        
        if (gameObject.activeSelf) {

            int priceOfProduct = price[keyword];

            if (gameResource.gameData.amountOfStarDust >= priceOfProduct ) {
                currentStrategy.buy( keyword , pool );
                gameResource.onBuyStar( keyword , priceOfProduct );
                showResultPopup();
            }
            else {
                popupController.openToastMessage();
            }
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

    private ObjectType productType;

    private GameObject prefab = null;

    private int starindex = 0;

    void Awake() {
        whereStarIsLocated = GameObject.FindWithTag("Stars").gameObject;
    }

    void setLocation (GameObject location) {
        whereStarIsLocated = location;
    }

    public override void openPopupUsing( popUpController popup  ) {
        popup.openTripleChoice();
    }

    public override void buy(string type, gameObjectManager pool) {

        switch (type) {
            case "blue":
                productType = ObjectType.blueStar;
                break;
            case "yellow":
                productType = ObjectType.yellowStar;
                break;
            case "white":
                productType = ObjectType.whiteStar;
                break;
            default:
                break;
        }

        
        print("started");
        GameObject newStar = pool.chooseTypeOfPool(productType).pullObjectFromPoolTo(whereStarIsLocated);
        newStar.SetActive(true);

        Vector3 pos = new Vector3(
            UnityEngine.Random.Range(-2f, 2f),
            UnityEngine.Random.Range(-0.5f, 4f),
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