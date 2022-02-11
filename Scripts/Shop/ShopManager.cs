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
        currentStarDust.text = gameResource.gameData.amountOfStarDust.ToString();
        currentGem.text = gameResource.gameData.amountOfGem.ToString();
    }
    
    public void onButtonClick() {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        gameShop.beforePopup(buttonName);
    }
    

}
