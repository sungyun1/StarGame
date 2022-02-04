using System;
using UnityEngine;

public class StrategyBuilder : MonoBehaviour {

    public ResourceManager manager;
    public CheckConditionStrategy build (string str) 
    {
        switch(str) {
            case "CSH":

            case "BWS":
                return new BuyStarConditionStrategy(
                    manager
                );  
            case "CSG":
                return new createCharactorStrategy(
                    manager
                );
            case "NOT":
                return new NextStepStrategy();
            default: 
                return new NextStepStrategy();
        }
    }
}

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

    public BuyStarConditionStrategy (ResourceManager obj) {
        resource = obj;
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

    public createCharactorStrategy (ResourceManager obj) {
        resource = obj;
    }
    public bool check() {
        if (resource.gameData.myCharactors.Count > 0) {
            return true;
        }
        else return false;
    }
}
