using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // 튜토리얼을 진행한다.
    // 뒤의 ModeManager를 끔으로서 메인 화면에서 벗어나지 못하게 한다.

    public ModeSwitchManager gameMode;
    public CSVReader reader = new CSVReader();
    private List<List<string>> tutorialDialogue = new List<List<string>>();
    public GameObject tutorialPanel;
    public Text tutorialText;

    public class TutorialStep {
        private string explanation; // 문장
        public void checkConditionIsSatisfied () {
            // 검사할 것
        }

        public void finishStep () {
            // 실행할 것
        }

        public void determineInputIsAllowed () {
            // 허용되지 않은 입력 ex. 캔버스에서 벗어나지 못하게 한다.
        }
    }

    private List<TutorialStep> tutorialSteps = new List<TutorialStep>();
    private TutorialStep currentStep = null;

    public void Awake() {
        // tutorialDialogue = reader.setFileLocation("tutorial.csv").parse();
        // startTutorial();
        tutorialText.text = "hello";

        if (tutorialDialogue != null) {
            foreach (List<string> Head in tutorialDialogue) {
            // tutorialStep 완성
            }
        }

        gameObject.SetActive(false);
        
    }

    public void startTutorial() {
        gameObject.SetActive(true);
        gameMode.isMotionSwitchEnabled = false;
    }

    public void goToNextStep () {

    }

    public void endTutorial() {
        gameObject.SetActive(false);
        gameMode.isMotionSwitchEnabled = true;
    }
}
