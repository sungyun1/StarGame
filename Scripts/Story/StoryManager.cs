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

    void Awake() {
        // diaryContext = reader.setFileLocation("Info/diaryContext.csv").parse();
    }

    void showTodayDiary() {

        int date = gameResource.gameData.currentDate;

        storyDate.text = "D - " + date.ToString();
        storyText.text = diaryContext[date][1];
    }

    void showNextPage() {
        
    }
}
