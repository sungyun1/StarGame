using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : UI_Interface
{
    // 진행상황 관리하는 친구
    public ResourceManager gameResource;
    public Text storyText;
    public Text storyDate;

    // 일자별 기준
    private List<List<string>> diaryContext = new List<List<string>>();

    ///////////////////////

    private int pageNum = 1;
    private int dayNum = 0;

    //////////////// methods ///////////////////

    void Awake() {
        gameObject.SetActive(false);
        CSVReader.setFileLocation("Info/diaryContext.csv");
        diaryContext = CSVReader.parse();
        showTodayDiary();
    }

    public void showTodayDiary() {

        dayNum = gameResource.gameData.currentDate;

        storyDate.text = "D-" + (dayNum+1);
        storyText.text = diaryContext[dayNum][pageNum];
    }

    public void showNextPage() {

        pageNum++;
        if (pageNum >= diaryContext[dayNum].Count) {
            gameObject.SetActive(false);
        }
        else {
            storyText.text = diaryContext[dayNum][pageNum];
        }
    }

    public void showCharactorQuestions () {
        
    }
}
