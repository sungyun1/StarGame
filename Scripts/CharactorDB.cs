using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorDB : MonoBehaviour
{
    // question 들은 사실 여기서 다룰 이유가 없어서 다른 곳으로 빼낼 것임~

    public enum DataIndex {
        name = 0,
        type,
        description,
        backgroundSpritePath,
        hiddenStateSpritePath,
        openStateSpritePath,
        diaryDrawingSpritePath,
        diaryDetailSpritePath,
        diarySpaceMarkSpritePath
    }

    public int numberOfDataPerCharactor = 9;

    public CSVReader csvReader = new CSVReader();

    private Dictionary<int, string> charactorDatabase;

    private int currentIndex = 0;

    void Awake() {

        List<List<string>> info = csvReader
            .setFileLocation("Info/CharactorInfo.csv")
            .parse();

        for (int i = 0; i < info.Count; i++) {
            print(info[i][2]);
        }

        charactorDatabase = new Dictionary<int, string>();
        charactorDatabase.Add(0, "huggie");
        charactorDatabase.Add(1, "He is example for charactor development");
        charactorDatabase.Add(2, "question 1");
        charactorDatabase.Add(3, "answer 1");
        charactorDatabase.Add(4, "Charactor/Normal/Huggie");
        charactorDatabase.Add(5, "Charactor/Normal/Huggie");
        charactorDatabase.Add(6, "Charactor/DiaryOpened/Huggie");
        charactorDatabase.Add(7, "Charactor/Normal/Huggie");
        charactorDatabase.Add(8, "Charactor/Normal/Huggie");
        charactorDatabase.Add(9, "Diary/normal");
        //
        charactorDatabase.Add(10, "runner");
        charactorDatabase.Add(11, "2nd description");
        charactorDatabase.Add(12, "question 2");
        charactorDatabase.Add(13, "answer 2");
        charactorDatabase.Add(14, "Charactor/Normal/Runner");
        charactorDatabase.Add(15, "Charactor/Normal/Runner");
        charactorDatabase.Add(16, "Charactor/DiaryOpened/Runner");
        charactorDatabase.Add(17, "Charactor/Normal/Runner");
        charactorDatabase.Add(18, "Charactor/Normal/Runner");
        charactorDatabase.Add(19, "Diary/normal");
    }

    public string getCharactorInformation (int charactorNumber, DataIndex informationToFind) {

        string value = charactorDatabase[ charactorNumber * numberOfDataPerCharactor + (int) informationToFind ];
        return value;
    }

    public void pushCharactorInfo (List<List<string>> charactorInfo) {
        // 캐릭터에 관한 정보 입력

        int charactorNum = charactorInfo.Count;

        for (int i = 0; i < charactorNum; i++) {

            string name = charactorInfo[charactorNum][0];
            string type = charactorInfo[charactorNum][1];

            charactorDatabase.Add(currentIndex++, type); // type
            charactorDatabase.Add(currentIndex++, name); // name
            charactorDatabase.Add(currentIndex++, charactorInfo[charactorNum][2]); // description

            // charactorDatabase.Add(currentIndex++, charactorInfo[charactorNum][3]); // question
            // charactorDatabase.Add(currentIndex++, charactorInfo[charactorNum][4]); // answer

            charactorDatabase.Add(currentIndex++, "Charactor/" + type + name); // 캐릭터 이미지
            charactorDatabase.Add(currentIndex++, "Charactor/" + type + name); // 도감 실루엣
            charactorDatabase.Add(currentIndex++, "Charactor/DiaryOpened/" + name); // 도감 해금시
            charactorDatabase.Add(currentIndex++, "Charactor/" + type + name); // 다이어리 손그림
            charactorDatabase.Add(currentIndex++, "Charactor/" + type + name); // 다이어리 멋진그림
            charactorDatabase.Add(currentIndex++, "Diary/" + type); // 우주 마크
        }        
    }

    public void pushQuestionInfo (List<List<string>> questionInfo) {

    }

    public void addDirectoryForSetting() {
    }    
}

