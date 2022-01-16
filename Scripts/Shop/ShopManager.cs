using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ShopManager : MonoBehaviour
{

    public Text currentStarDust;
    public Text currentGem;

    public ResourceManager gameResource;

    public Shop gameShop;

    void Update () {
        currentStarDust.text = gameResource.starDust.ToString();
        currentGem.text = gameResource.gemNum.ToString();
    }
    
    public void onButtonClick() {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        gameShop.chooseBehaviourWithKeyword(buttonName);
    }
    

}
