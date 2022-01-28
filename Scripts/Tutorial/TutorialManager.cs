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
    public Telescope telescope;
    public GameObject maskingPanel;

    //////////////////////////////// 명령 제어기
    public StrategyBuilder builder;

    public CombinationChooser chooser;

    ///////////////////////// CSV parser

    public CSVReader reader = new CSVReader();
    private List<List<string>> fileContent = new List<List<string>>();

    ////////////////////////// step

    private List<TutorialStep> tutorialSteps = new List<TutorialStep>();
    private TutorialStep currentStep = null;

    private int index = 0;
    private bool isInDialogueProcess = true;

    //////////////////////////////////////////////////////////

    public void Awake() {

        createListOfTutorial();

        input.onInteract += onTap;

        startTutorial();
    }

    public void createListOfTutorial () {

        fileContent = reader.setFileLocation("Info/tutorial.csv").parse();

        int length = fileContent.Count;

        for (int i = 0; i < length; i++) {
            TutorialStep newTutorialStep = new TutorialStep();
            var item = fileContent[i];
            newTutorialStep.dialogue = item[1];

            newTutorialStep.strategy = builder.build(item[2]);
            
            print(newTutorialStep.strategy);
            chooser.chooseCombinationOfAction(item[3], ref newTutorialStep.actionsNeedToDo);

            tutorialSteps.Add(newTutorialStep);
        }
    }

    public void startTutorial() {
        gameObject.SetActive(true);
        currentStep = tutorialSteps[0];
        gameMode.isMotionSwitchEnabled = false;
        boy.isMotionSwitchEnabled = false;
        telescope.isMotionSwitchEnabled = false;
        
        startStep();
    }

    public void goToNextStep () {
        currentStep.finishStep();
        index++;
        if (index == tutorialSteps.Count) {
            endTutorial();
        }
        else {
            currentStep = tutorialSteps[index];
            toggleDialogueProcess();
            startStep();
        }
    }

    //////////////////////////////////////////////////////////

    public void startStep() {
        tutorialText.text = currentStep.dialogue;
    }

    public void toggleDialogueProcess() {
        isInDialogueProcess = !isInDialogueProcess;
        maskingPanel.SetActive(isInDialogueProcess);
        tutorialPanel.SetActive(isInDialogueProcess);
    }

    public void endTutorial() {
        gameObject.SetActive(false);
        gameMode.isMotionSwitchEnabled = true;
        boy.isMotionSwitchEnabled = true;
        telescope.isMotionSwitchEnabled = true;
    }

    /////////////////////////////////////////////////////////////////

    void onTap () {
        if (gameObject.activeSelf) {
            if (isInDialogueProcess) {
                toggleDialogueProcess();
            }
            else {
                if (currentStep.checkCondition()) {
                    goToNextStep();
                }
            }
        }
    }
}
