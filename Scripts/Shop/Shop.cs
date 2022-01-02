using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour
{
    
    // 필요한 외부 오브젝트
    public GameObject interactibleObject;
    public popUpController popUp;
    public ResourceManager gameResource;

    // 스트레티지 목록
    private Strategy currentStrategy;
    private Strategy buyStarStrategy;
    private Strategy startGatchaStrategy;
    private Strategy upgradeBoyStrategy;

    // 내부 변수
    private Dictionary<string, int> productList = new Dictionary<string, int>();

    public event Action openYesOrNoPopup;

    void Start () {
        productList.Add("buyStarBtn", 100);
        productList.Add("gatchaBtn", 100);
        productList.Add("upgradeBoyBtn", 100);

        buyStarStrategy = gameObject.AddComponent<BuyStarStrategy>();
        startGatchaStrategy = gameObject.AddComponent<startGatchaStrategy>();
        upgradeBoyStrategy = gameObject.AddComponent<upgradeBoyStrategy>();

        popUp.finished += afterPopup;
    }

    public void buyProduct (string name) {

        if (productList.ContainsKey(name)) { // 실제로 그 아이템이 존재할 때
            if (gameResource.starDust >= productList[name]) {
                selectProduct(name);
                
                if (openYesOrNoPopup != null) {
                    openYesOrNoPopup(); // 팝업을 열고 팝업 처리가 끝나면,,,
                }
            }
            else {
                // 살 수 없다는 메세지 팝업 띄워야 함
            }
        }
        else {
            // 키가 포함되지 않은 경우
        }
    }
    
    public void afterPopup () {
        if (gameObject.activeSelf) {
            currentStrategy.buy( interactibleObject, gameResource );
        }
    }

    // 스트레티지 패턴 적용
    public void selectProduct(string name) {
        switch(name) {
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
    public abstract void buy(GameObject Stars, ResourceManager gameResource);
}

class BuyStarStrategy : Strategy {

    public GameObject yellowStarPrefab;
    public GameObject blueStarPrefab;
    public GameObject whiteStarPrefab;

    private int starindex = 0;

    void Start() {
        yellowStarPrefab = Resources.Load<GameObject>("Prefabs/YellowStar");
        blueStarPrefab = Resources.Load<GameObject>("Prefabs/BlueStar");
        whiteStarPrefab = Resources.Load<GameObject>("Prefabs/WhiteStar");
    }

    public override void buy(GameObject Stars, ResourceManager gameResource) {

        GameObject newStar = Instantiate(blueStarPrefab, Stars.transform);

        Vector3 pos = new Vector3(
                UnityEngine.Random.Range(-2f, 2f),
                UnityEngine.Random.Range(-0.5f, 4f),
                1
            );

        newStar.transform.position = pos;
        newStar.GetComponent<Star>().index = starindex;
        starindex++;

        gameResource.onBuyStar();

    }
}

class startGatchaStrategy : Strategy {
    public override void buy(GameObject interactibleObject, ResourceManager gameResource) {
    }
}

class upgradeBoyStrategy : Strategy {
    public override void buy(GameObject interactibleObject, ResourceManager gameResource) {
        gameResource.onUpgradeBoy();
    }
}