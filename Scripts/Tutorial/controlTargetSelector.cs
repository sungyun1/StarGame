using UnityEngine;
using System;

public class controlTargetSelector : MonoBehaviour {
    // 각 스텝이 어떤 대상을 컨트롤해야 하는지 결정함.
    // 각 스텝이 어떤 것을 검사해야 하는지 결정함.

    ///////////////////////////////////////

    public GameObject mainObject; // modeSwitchManager
    public GameObject Boy; // resourceManager
    public GameObject Telescope;

    public GameObject shop;
    public GameObject Diary;

    //////////////////////////////////////

    private bool[] onlyMode = new bool[] {true, false, false};
    private bool[] onlyBoy = new bool[] {false, true, false};
    private bool[] onlyTelescope = new bool[] {false, false, true};
    private bool[] nothing = new bool[] {false, false, false};

    ///////////////////////////////////////

    public void selectTargetToOperate (string str, ref TutorialStep step) {
        switch(str) {
            case "CSC": 
                step.isThereConditionToCheck = true;
                step.objectToCheck = onlyMode;
                step.strategy = new checkCurrentStatusStrategy(mainObject, "canvas");
                break;
            case "CSS": 
                step.isThereConditionToCheck = true;
                step.objectToCheck = onlyMode;
                step.strategy = new checkCurrentStatusStrategy(mainObject, "shop");
                break;
            case "CSD": 
                step.isThereConditionToCheck = true;
                step.objectToCheck = onlyBoy;
                step.strategy = new checkDiaryStrategy(Diary, "general");
                break;
            case "CSU": 
                step.isThereConditionToCheck = true;
                step.objectToCheck = nothing;
                step.strategy = new checkUpgradeStrategy(Telescope);
                break;
            case "CSH": 
                step.isThereConditionToCheck = true;
                step.objectToCheck = onlyMode;
                step.strategy = new checkCurrentStatusStrategy(mainObject, "home");
                break;
            case "BWS":
                step.isThereConditionToCheck = true;
                step.objectToCheck = nothing;
                step.strategy = new BuyStarConditionStrategy(Boy);
                break;
            case "CSG":
                step.isThereConditionToCheck = true;
                step.objectToCheck = nothing;
                step.strategy = new createCharactorStrategy(Boy);
                break;
            case "ODP":
                step.isThereConditionToCheck = true;
                step.objectToCheck = nothing;
                step.strategy = new checkDiaryStrategy(Diary, "detail");
                break;
            case "PRE":
                step.isThereConditionToCheck = false;
                step.objectToCheck = null;
                step.strategy = new NextStepStrategy();
                break;
            default: 
                step.isThereConditionToCheck = false;
                step.objectToCheck = null;
                step.strategy = new NextStepStrategy();
                break;
        }
    }
}