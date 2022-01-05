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

    public CSVReader cSVReader;

    private Dictionary<int, string> charactorDatabase;

    private int currentIndex = 0;

    void Awake() {

        // 일단 CSVReader 에서 정보를 받아온다.
        // charactorInfo.csv 랑 questionAndAnswers.csv 에서 각각 받아옴
        // 나머지 정보들은 이름으로 메울 수 있다.


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

        string value = charactorDatabase[ charactorNumber * 10 + (int) informationToFind ];
        return value;
    }

    public void pushInfoToDictionary (List<List<string>> charactorInfo, List<List<string>> questionAndAnswer) {
        // 받아온 정보를 전부 집어넣는다.

        int charactorNum = charactorInfo.Count;

        for (int i = 0; i < charactorNum; i++) {

            string type = charactorInfo[charactorNum][0];
            string name = charactorInfo[charactorNum][1];

            charactorDatabase.Add(currentIndex++, type); // type
            charactorDatabase.Add(currentIndex++, name); // name
            charactorDatabase.Add(currentIndex++, charactorInfo[charactorNum][2]); // description

            charactorDatabase.Add(currentIndex++, charactorInfo[charactorNum][3]); // question
            charactorDatabase.Add(currentIndex++, charactorInfo[charactorNum][4]); // answer

            charactorDatabase.Add(currentIndex++, "Charactor/" + type + name); // 캐릭터 이미지
            charactorDatabase.Add(currentIndex++, "Charactor/" + type + name); // 도감 실루엣
            charactorDatabase.Add(currentIndex++, "Charactor/DiaryOpened/" + name); // 도감 해금시
            charactorDatabase.Add(currentIndex++, "Charactor/" + type + name); // 다이어리 손그림
            charactorDatabase.Add(currentIndex++, "Charactor/" + type + name); // 다이어리 멋진그림
            charactorDatabase.Add(currentIndex++, "Diary/" + type); // 우주 마크
        }        
    }

    
}

