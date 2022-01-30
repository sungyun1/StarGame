using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum DialogueType {
    sceneNumber = 0,
    personWhoSpeak,
    dialogue,
    isSceneImageHaveToChange
}

public enum cutSceneType {
    prolog,
    epilog,
    badEnding1,
    badEnding2
}

public class CutSceneManager : MonoBehaviour
{
    // private CSVReader reader = new CSVReader();

    private List<List<string>> dialogue = new List<List<string>>();

    public Text cutSceneText;
    public Image cutSceneImage;

    ///////////// 내부 연산용 변수 ////////////////

    private int currentScene = 0;
    private string textBuffer;
    private int lengthOfDialogue = 0;
    
    private cutSceneType type;
    private int currentImageNumber = 0;

    private bool isPlayerWantsQuickProgression = false;
    private bool isTypingStillProgressing = false;

    //////////////// 메소드 ////////////////////////

    void Start() {
        type = cutSceneType.prolog;
        loadDialogueFrom( type.ToString() + ".csv" );
        startDialogue();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isTypingStillProgressing) {
                isPlayerWantsQuickProgression = true;    
            }
            else {
                showNextScene();
            }
        }
    }

    void loadDialogueFrom(string filename) {
        CSVReader.setFileLocation("/Info/" + filename);
        dialogue = CSVReader.parse();
        lengthOfDialogue = dialogue.Count;
    }

    void startDialogue () {
        StartCoroutine (typeWord());
        showNextImage();
    }

    void showNextScene () {
        if (currentScene < lengthOfDialogue - 1) {
            currentScene++;
            textBuffer = null;
            StartCoroutine(typeWord());
            showNextImage();
        }
        else {
            goToMainGame();
        }
    }

    void showNextImage () { 

        if (dialogue[currentScene][(int)DialogueType.isSceneImageHaveToChange] == "1") { // 필요할 때만 리로드해야 함
            currentImageNumber++;
            cutSceneImage.sprite = Resources.Load<Sprite>(
                "CutScene/" + type.ToString() + "/" + currentImageNumber
            ) as Sprite;
        } 
        else { }
    }

    public IEnumerator typeWord() {

        isTypingStillProgressing = true;

        string target = dialogue[currentScene][(int)DialogueType.dialogue];
        int length = target.Length;
        int i = 0;

        while (i < length) {
            if (isPlayerWantsQuickProgression) {
                textBuffer = target;
                i = length;
            }
            else {
                textBuffer += target[i];
                i++;
            }
            cutSceneText.text = textBuffer;
            yield return new WaitForSeconds(0.07f);
        }

        isTypingStillProgressing = false;
        isPlayerWantsQuickProgression = false;
    }

    void goToMainGame () {
        SceneManager.LoadScene("MainScene");
    }

}
