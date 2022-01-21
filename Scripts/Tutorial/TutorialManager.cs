using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TutorialManager : MonoBehaviour
{
    // 튜토리얼을 진행한다.
    // 뒤의 ModeManager를 끔으로서 메인 화면에서 벗어나지 못하게 한다.

    public ModeSwitchManager gameMode;
    public GameObject tutorialPanel;
    public Text tutorialText;
    public PlayerInput input;
    public Boy boy;
    public Telescope telescope;

    ///////////////////////// CSV parser

    public CSVReader reader = new CSVReader();
    private List<List<string>> tutorialDialogue = new List<List<string>>();

    ////////////////////////// step

    public class TutorialStep {
        public string dialogue; // 출력될 문장
        public Action actionsNeedToDo = null;
        public void checkConditionIsSatisfied () {
            // 검사할 것
        }

        public void finishStep () {
            actionsNeedToDo?.Invoke();
        }

        public void determineInputIsAllowed () {
            // 허용되지 않은 입력 ex. 캔버스에서 벗어나지 못하게 한다.
        }
    }

    private List<TutorialStep> tutorialSteps = new List<TutorialStep>();
    private TutorialStep currentStep = null;

    public TutorialStep one = new TutorialStep();
    public TutorialStep two = new TutorialStep();

    //////////////////////////////////////////////////////////

    public void Awake() {
        // tutorialDialogue = reader.setFileLocation("tutorial.csv").parse();
        // startTutorial();

        one.dialogue = "one";
        one.actionsNeedToDo = null;
        chooseCombinationOfAction("MVC", ref one.actionsNeedToDo);
        two.dialogue = "two";

        input.onInteract += test;

        currentStep = one;

        gameObject.SetActive(true);

    }

    public void startTutorial() {
        gameObject.SetActive(true);
        gameMode.isMotionSwitchEnabled = false;
    }

    public void goToNextStep () {
        currentStep.finishStep();
        currentStep = two;
        startCurrentStep();
    }

    public void startCurrentStep() {
        tutorialText.text = currentStep.dialogue;
    }

    public void endTutorial() {
        gameObject.SetActive(false);
        gameMode.isMotionSwitchEnabled = true;
    }

    void test () {
        goToNextStep();
    }

    void chooseCombinationOfAction (string str, ref Action target) {

        print("started");

        switch(str) {
            case "MVC":
                target += gameMode.onSlideUp;
                break;
            case "MVS":
                target += gameMode.onSlideDown;
                break;
            case "MVD":
                target += boy.onInteract;
                break;
            case "MVT":
                target += telescope.onInteract;
                break;
            default: break;
        }
    }


}
