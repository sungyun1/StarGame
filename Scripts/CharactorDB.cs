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

    public CSVReader reader = new CSVReader();

    private List<List<string>> data = new List<List<string>>();

    void Awake() {

        data = reader
            .setFileLocation("Info/CharactorInfo.csv")
            .parse();

        addDirectoryAdressForSetting();
    }

    public string getCharactorInformation (int charactorNumber, DataIndex informationToFind) {

        string value = data[ charactorNumber ] [ (int) informationToFind ];
        return value;
    }

    public void addDirectoryAdressForSetting() {
        for (int i = 0; i < data.Count; i++) {

            string name = data[i][0];
            string type = data[i][1];

            data[i].Add("Charactor/Home/" + type + "/" + name); // 캐릭터 이미지
            data[i].Add("Charactor/Home/" + type + "/" + name); // 도감 실루엣
            data[i].Add("Charactor/DiaryOpened/" + name); // 도감 해금시
            data[i].Add("Charactor/DiaryOpened/" + name); // 다이어리 손그림
            data[i].Add("Charactor/Home/" + type + "/" + name); // 다이어리 멋진그림
            data[i].Add("Diary/" + type); // 우주 마크
        }
    }    
}

