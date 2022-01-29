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
    private CSVReader reader = new CSVReader();
    private List<List<string>> diaryContext = new List<List<string>>();

    ///////////////////////

    private int pageNum = 1;
    private int dayNum = 0;

    //////////////// methods ///////////////////

    void Awake() {
        diaryContext = reader.setFileLocation("Info/diaryContext.csv").parse();
        showdayNumDiary();
    }

    public void showdayNumDiary() {

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
