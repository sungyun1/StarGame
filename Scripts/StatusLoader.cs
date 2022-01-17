using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[System.Serializable]
public class Data {
    public int amountOfStarDust;
    public int amountOfGem;

    public int whiteStarNum;
    public int yellowStarNum;
    public int blueStarNum;
    
    // 업그레이드 내역
    public int telescopeLevel;

    public int playerLevel;

    public List<Charactor> myCharactors = new List<Charactor>();

    // 별자리 보관 내역 (이름 + 그 캐릭터 별자리)
    public Dictionary<int, StarGroup> stargroups = new Dictionary<int, StarGroup>();
}

public class StatusLoader : MonoBehaviour
{

    public Data currentGameData;
    public ResourceManager resourceManager; // 얘한테서 받아오고 저장

    void Start() {
        Init();
    }

    void Init () {
        currentGameData = new Data();
        currentGameData.amountOfStarDust = 200;
        currentGameData.amountOfGem = 500;
        currentGameData.whiteStarNum = 0;
        currentGameData.yellowStarNum = 0;
        currentGameData.blueStarNum = 0;
        currentGameData.telescopeLevel = 0;
        currentGameData.playerLevel = 0;

        Save();
    }

    public void Load () {
        string dataFromFile = File.ReadAllText(Application.dataPath + "/gameStatus.json");
        print(dataFromFile);
        currentGameData = JsonUtility.FromJson<Data>(dataFromFile);
    }

    public void Save () {
        string newGameData = JsonUtility.ToJson(currentGameData);
        print(newGameData);
        File.WriteAllText(Application.dataPath + "/gameStatus.json", newGameData);
    }
}
