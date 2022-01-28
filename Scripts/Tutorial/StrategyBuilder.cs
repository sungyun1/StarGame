using System;
using UnityEngine;

public class StrategyBuilder : MonoBehaviour {

    private ResourceManager manager;
    void Awake() {
        manager = GameObject.Find("Boy").GetComponent<ResourceManager>();
        print(manager);
    }
    public CheckConditionStrategy build (string str) 
    {
        switch(str) {
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
                throw new Exception("unexpected triplet code : conditionsNeedToCheck");
        }
    }
}


public class CheckConditionStrategy {

    public virtual bool check() {
        return true;
    }
}

public class NextStepStrategy : CheckConditionStrategy {
    public override bool check() {
        return true;
    }
}

public class BuyStarConditionStrategy : CheckConditionStrategy {

    private ResourceManager resource;

    public BuyStarConditionStrategy (ResourceManager obj) {
        resource = obj;
    }

    public override bool check() {
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
    public override bool check() {
        if (resource.gameData.myCharactors.Count > 0) {
            return true;
        }
        else return false;
    }
}