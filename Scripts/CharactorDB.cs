using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CharactorDB : MonoBehaviour
{

    struct CharactorData {
        string name;
        string description;
        string question;
        string answer;
        string backgroundSpritePath;
        string hiddenStateSpritePath;
        string openStateSpritePath;
        string diaryDrawingSpritePath;
        string diaryDetailSpritePath;
        string diarySpaceMarkSpritePath;
    }

    // 10개 단위로 조직되어 있다. 원하는 키는 DataIndex에서 추출해서 사용
    public enum DataIndex {
        name = 0,
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

    private Dictionary<int, string> charactorDatabase;

    void Awake() {
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
        charactorDatabase.Add(9, "Diary/SpaceMark1");
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
        charactorDatabase.Add(19, "Diary/SpaceMark1");


    }

    public string getCharactorInformation (int charactorNumber, DataIndex informationToFind) {

        string value = charactorDatabase[ charactorNumber * 10 + (int) informationToFind ];
        return value;
    }

    public List<Dictionary<int, string>> fetchDataFromScvFile () {

        int currentIndex = 0;

        var list = new List<Dictionary<int, string>>();
        StreamReader streamReader = new StreamReader(Application.persistentDataPath + "/charactorInfo.csv");

        bool endOfFile = false;
        while (!endOfFile) {
            string data = streamReader.ReadLine(); // 한 줄 읽는다.
            if (data == null) {
                endOfFile = true;
                break;
            }
            else if (data[0] == '{' || data[0] == '}') {
                // 그냥 넘긴다. 내가 보기 좋으라고 만들어 놓은 것.
            }
            else {
                string[] data_values = data.Split(':'); // :를 기준으로 잘라서
                var tmp = new Dictionary<int, string>();
                for (int i = 0; i < 10; i++) {
                // 총 10개의 정보가 있으므로
                    tmp.Add(currentIndex, data_values[i]);
                    currentIndex++; // 인덱스는 계속해서 증가해야 함
                }
            }
            
        }

        return list;
    }
}

