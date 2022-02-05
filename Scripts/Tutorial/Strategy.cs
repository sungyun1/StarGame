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

public class checkDiaryStrategy : CheckConditionStrategy {
    private DiaryManager diaryManager;
    private string page;

    public checkDiaryStrategy (GameObject obj, string pageType) {
        diaryManager = obj.GetComponent<DiaryManager>();
        page = pageType;
    }

    public bool check() {
        if (page == "general") {
            if (diaryManager.isGeneralPageOpen) {
                return true;
            }
            else return false;
        }
        else if (page == "detail") {
            if (diaryManager.isSpecificPageOpen) {
                return true;
            }
            else return false;
        }
        
        else return false;
    }
}

public class checkUpgradeStrategy : CheckConditionStrategy {
    private UniverseUpgradeManager Manager;

    public checkUpgradeStrategy (GameObject obj) {
        Manager = obj.GetComponent<UniverseUpgradeManager>();
    }

    public bool check() {
        if (Manager.isUniverseUpgradeOpen) {
            return true;
        }
        else return false;
    }
}
