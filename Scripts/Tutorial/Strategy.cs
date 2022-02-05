using System;
using UnityEngine;

public interface CheckConditionStrategy {
    bool check();
}


public class NextStepStrategy : CheckConditionStrategy {
    public bool check() {
        return true;
    }
}

public class BuyStarConditionStrategy : CheckConditionStrategy {

    private ResourceManager resource;

    public BuyStarConditionStrategy (GameObject obj) {
        resource = obj.GetComponent<ResourceManager>();
    }

    public bool check() {
        if (resource.gameData.whiteStarNum > 0) {
            return true;
        }
        else return false;
    }
}

public class createCharactorStrategy : CheckConditionStrategy {

    private ResourceManager resource;

    public createCharactorStrategy (GameObject obj) {
        resource = obj.GetComponent<ResourceManager>();
    }
    public bool check() {
        if (resource.gameData.myCharactors.Count > 0) {
            return true;
        }
        else return false;
    }
}

public class checkCurrentStatusStrategy : CheckConditionStrategy {
    private ModeSwitchManager gameMode;
    private string placeHeadingTo;
    public checkCurrentStatusStrategy (GameObject mode, string location) {
        gameMode = mode.GetComponent<ModeSwitchManager>();
        placeHeadingTo = location;
    }

    public bool check() {
        if (gameMode.currentNode.name == placeHeadingTo) {
            return true;
        }
        else return false;
    }
}
