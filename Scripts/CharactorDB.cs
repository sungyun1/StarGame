using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CharactorDB : MonoBehaviour
{
    // 10개 단위로 조직되어 있다. 원하는 키는 DataIndex에서 추출해서 사용
    public enum DataIndex {
        type = 0,
        name,
        description,
        question,
        answer,
        backgroundSpritePath,
        hiddenStateSpritePath,
        openStateSpritePath,
        diaryDrawingSpritePath,
        diaryDetailSpritePath,
        diarySpaceMarkSpritePath
    }

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

        string value = charactorDatabase[ charactorNumber * 11 + (int) informationToFind ];
        return value;
    }

    public void pushCharactorInfo (List<List<string>> charactorInfo) {
        // 캐릭터에 관한 정보 입력

        int charactorNum = charactorInfo.Count;

        for (int i = 0; i < charactorNum; i++) {

            string type = charactorInfo[charactorNum][0];
            string name = charactorInfo[charactorNum][1];

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

    
}

