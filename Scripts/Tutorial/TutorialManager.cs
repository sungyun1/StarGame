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

    public Popup popup;
    public ModeSwitchManager gameMode;
    public GameObject tutorialPanel;
    public Text tutorialText;
    public PlayerInput input;
    public Boy boy;
    public Telescope telescope;
    public GameObject maskingPanel;
    
    //////////////////////////////// 명령 제어기

    public CombinationChooser chooser;

    public controlTargetSelector selector;

    ///////////////////////// CSV parser

    private List<List<string>> fileContent = new List<List<string>>();

    ////////////////////////// step

    private List<TutorialStep> tutorialSteps = new List<TutorialStep>();
    private TutorialStep currentStep = null;

    private int index = 0;
    private bool isDialogueProcessDone = false;
    private bool isPopupInteractionFinished = false;

    //////////////////////////////////////////////////////////

    public void Awake() {

        createListOfTutorial();

        input.onInteract += onTap;
        gameMode.checkEventForTutorial += determineIsConditionSatisfied;
        popup.checkEventForTutorial += determineIsConditionSatisfied;

        startTutorial();
    }

    void onTap () {
        
        if (gameObject.activeSelf) {
            
            if (currentStep.isThereConditionToCheck) {
                toggleDialogueProcess(false);
                determineIsConditionSatisfied();
            }
            else {
                goToNextStep();
            }
        }
    }

    public void createListOfTutorial () {

        CSVReader.setFileLocation("Info/tutorial.csv");
        fileContent = CSVReader.parse();

        int length = fileContent.Count;

        for (int i = 0; i < length; i++) {
            var item = fileContent[i];

            TutorialStep newTutorialStep = new TutorialStep();
            newTutorialStep.dialogue = item[1];
            selector.selectTargetToOperate(item[2], ref newTutorialStep);
            chooser.chooseCombinationOfAction(item[3], ref newTutorialStep.actionsNeedToDo);

            tutorialSteps.Add(newTutorialStep);
        }
    }

    public void startTutorial() {
        gameObject.SetActive(true);
        currentStep = tutorialSteps[0];
        lockMovementExcept(new bool[] {false , false , false});
        
        processCurrentStep();
    }

    public void goToNextStep () {
        currentStep.finishStep();
        index++;
        if (index == tutorialSteps.Count) {
            endTutorial();
        }
        else {
            currentStep = tutorialSteps[index];
            lockMovementExcept(currentStep.objectToCheck);
            toggleDialogueProcess(true);
            processCurrentStep();
        }
    }

    //////////////////////////////////////////////////////////

    public void processCurrentStep() {
        tutorialText.text = currentStep.dialogue;
    }

    public void endTutorial() {
        gameObject.SetActive(false);
        lockMovementExcept(new bool[] {true, true, true});
    }

    public void toggleDialogueProcess(bool value) {
        maskingPanel.SetActive(value);
        tutorialPanel.SetActive(value);
    }

    public void lockMovementExcept(bool[] value) {
        if (value != null) {
            gameMode.isMotionSwitchEnabled = value[0];
            boy.isMotionSwitchEnabled = value[1];
            telescope.isMotionSwitchEnabled = value[2];
        }
        else {}
    }


    void determineIsConditionSatisfied () {
        if (currentStep.checkCondition()) {
            goToNextStep();
        }
    }

    void togglePopupInteractionFactor () {
        isPopupInteractionFinished = !isPopupInteractionFinished;
    }
}
