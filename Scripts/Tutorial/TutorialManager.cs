using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TutorialManager : MonoBehaviour
{
    // 튜토리얼을 진행한다.
    // 뒤의 ModeManager를 끔으로서 메인 화면에서 벗어나지 못하게 한다.

    ///////////////////// 사용해야 하는 기능

    public ModeSwitchManager gameMode;
    public GameObject tutorialPanel;
    public Text tutorialText;
    public PlayerInput input;
    public Boy boy;
    public popUpController popUpController;
    public Telescope telescope;
    public GameObject maskingPanel;
    public StarCanvas starCanvas;
    public Shop shop;

    ///////////////////////// CSV parser

    public CSVReader reader = new CSVReader();
    private List<List<string>> fileContent = new List<List<string>>();

    ////////////////////////// step

    public class TutorialStep {
        public string dialogue; // 출력될 문장
        public Action actionsNeedToDo = null; // 끝난 뒤 실행할 것
        public Action conditionsNeedToCheck = null;

        public bool isConditionSatisfied = false;

        public void checkWhetherConditionIsSatisfied () {
            isConditionSatisfied = true;
        }

        public void finishStep () {
            actionsNeedToDo?.Invoke();
        }
    }


    private List<TutorialStep> tutorialSteps = new List<TutorialStep>();
    private TutorialStep currentStep = null;

    private int index = 0;
    private bool isMaskingPanelOn = true;
    private bool isDialogueFinished = false;

    //////////////////////////////////////////////////////////

    public void Awake() {

        createListOfTutorial();

        input.onInteract += onTap;

        currentStep = tutorialSteps[index];

        gameObject.SetActive(true);
        startTutorial();
    }

    public void createListOfTutorial () {

        fileContent = reader.setFileLocation("Info/tutorial.csv").parse();

        int length = fileContent.Count;

        for (int i = 0; i < length; i++) {
            TutorialStep newTutorialStep = new TutorialStep();
            var item = fileContent[i];
            newTutorialStep.dialogue = item[1];

            newTutorialStep.conditionsNeedToCheck += newTutorialStep.checkWhetherConditionIsSatisfied;
            chooseCombinationOfCondition(item[2], ref newTutorialStep.conditionsNeedToCheck);
            
            newTutorialStep.actionsNeedToDo = null;
            chooseCombinationOfAction(item[3], ref newTutorialStep.actionsNeedToDo);

            tutorialSteps.Add(newTutorialStep);
        }
    }

    public void startTutorial() {
        gameObject.SetActive(true);
        currentStep = tutorialSteps[0];
        gameMode.isMotionSwitchEnabled = false;
        boy.isMotionSwitchEnabled = false;
        telescope.isMotionSwitchEnabled = false;
        startCurrentStep();
    }

    public void goToNextStep () {
        if (currentStep.isConditionSatisfied) {
            currentStep.finishStep();
            isDialogueFinished = false;
            index++;
            if (index == tutorialSteps.Count) {
                endTutorial();
            }
            else {
                currentStep = tutorialSteps[index];
                startCurrentStep();
            }
        }
        
    }

    public void startCurrentStep() {
        tutorialText.text = currentStep.dialogue;
    }

    public void endTutorial() {
        gameObject.SetActive(false);
        gameMode.isMotionSwitchEnabled = true;
        boy.isMotionSwitchEnabled = true;
        telescope.isMotionSwitchEnabled = true;
    }

    void onTap () {
        if (gameObject.activeSelf) {
            if (isDialogueFinished) {
                
            }
            else {

            }
            goToNextStep();
        }
    }

    void toggleMaskingPanel () {
        isMaskingPanelOn = !isMaskingPanelOn;
        maskingPanel.SetActive(isMaskingPanelOn);
    }

    void chooseCombinationOfAction (string str, ref Action target) {

        switch(str) {
            case "MVC":
                target += gameMode.onSlideUp;
                target += gameMode.switchState;
                break;
            case "MVS":
                target += gameMode.onSlideDown;
                target += gameMode.switchState;
                break;
            case "MVD":
                target += boy.onInteract;
                break;
            case "MVT":
                target += telescope.onInteract;
                break;
            case "MVH":
                target += gameMode.returnToHome;
                break;
            case "TMP":
                target += toggleMaskingPanel;
                break;
            case "NXT":
                break;
            default: 
                throw new Exception("unexpected triplet code");
        }
    }

    void chooseCombinationOfCondition (string str, ref Action target) 
    {
        switch(str) {
            case "BWS":
                shop.finished += target;
                break;
            case "CSG":
                starCanvas.finished += target;
                break;
            case "NOT":
                target?.Invoke();
                break;
            default: 
                throw new Exception("unexpected triplet code");
        }
    }



}
