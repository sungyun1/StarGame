using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class Data {
    public int amountOfStarDust;
    public int amountOfGem;

    // 별 관련
    public int whiteStarNum;
    public int yellowStarNum;
    public int blueStarNum;
    public int normalStarGroupCreationLevel = 2;
    public int rareStarGroupCreationLevel = 2;
    public int epicStarGroupCreationLevel = 2;
    
    // 업그레이드 내역
    public int telescopeLevel;

    public int playerLevel;

    public int normalUniverseLevel = 0;
    public int rareUniverseLevel = 0;
    public int epicUniverseLevel = 0;

    // 캐릭터 관련

    public List<CharactorData> myCharactors = new List<CharactorData>();

    // 별자리 보관 내역 (캐릭터 id + 그 캐릭터 별자리)
    public List<StarGroupData> stargroups = new List<StarGroupData>();

    // 진행 상황 관련
    public int currentDate = 0;

    ///// 저장 완료 액션
    
}

[System.Serializable]
public struct StarData {
    public Vector2 position; // 위치
    public string starType; // 무슨 별인지
    public int index; // 인덱싱
}

[System.Serializable]
public class StarGroupData {
    public int charactorID;
    public List<StarConnection> starGroup;
}

[System.Serializable]
public class StarConnection {
    public List<StarData> connections;
}

public class ResourceManager : MonoBehaviour
{

    public CharactorBuilder charactorBuilder;

    public Data gameData;

    public void Start() {
        restorePreviousGame();
    }

    void init() {
        Load();
        gameData.amountOfStarDust = 500;
        gameData.amountOfGem = 200;
        gameData.blueStarNum = 0;
        gameData.whiteStarNum = 0;
        gameData.yellowStarNum = 0;
        gameData.normalStarGroupCreationLevel = 2;
        gameData.rareStarGroupCreationLevel = 2;
        gameData.epicStarGroupCreationLevel = 2;
        gameData.telescopeLevel = 1;
        gameData.playerLevel = 1;
        gameData.normalUniverseLevel = 0;
        gameData.rareUniverseLevel = 0;
        gameData.epicUniverseLevel = 0;
        gameData.currentDate = 1;
        gameData.myCharactors.Clear();
        gameData.stargroups.Clear();
        saveCurrentGameInfo();
    }

    public void Load () {
        string dataFromFile = File.ReadAllText(Application.dataPath + "/gameStatus.json");
        gameData = JsonUtility.FromJson<Data>(dataFromFile);
    }

    public void onGatherStarDust (int amount) {
        gameData.amountOfStarDust += amount;
        saveCurrentGameInfo();
    }

    public void onBuyStar(string starType, int price) {
        gameData.amountOfStarDust -= price;
        switch (starType) {
            case "white":
                gameData.whiteStarNum ++;
                break;
            case "yellow":
                gameData.yellowStarNum ++;
                break;
            case "blue":
                gameData.blueStarNum ++;
                break;
            default:
                break;
        }
        
        saveCurrentGameInfo();
    }

    public void onUpgradeTelescope () {
        gameData.telescopeLevel += 1;
        saveCurrentGameInfo();
    }

    public void onUpgradeBoy () {
        gameData.playerLevel += 1;
        saveCurrentGameInfo();
    }

    public void onUpgradeUniverse (string type) {
        switch (type) {
            case "normal":
                gameData.normalUniverseLevel += 1;
                break;
            case "rare":
                gameData.rareUniverseLevel += 1;
                break;
            case "epic":
                gameData.epicUniverseLevel += 1;
                break;
        }
    }


    public void addCharactor(CharactorData chData) {
        gameData.myCharactors.Add(chData);

        switch (chData.type) {
            case "normal":
                gameData.normalStarGroupCreationLevel += 1;
                break;
            case "rare":
                gameData.rareStarGroupCreationLevel += 2;
                break;
            case "epic":
                gameData.epicStarGroupCreationLevel += 3;
                break;
            default:
                break;
        }

        saveCurrentGameInfo();
    }

    public void addStarGroup(int charactorID, List<StarConnection> stargroup) {
        if (stargroup != null) {
            StarGroupData data = new StarGroupData();
            data.charactorID = charactorID;
            data.starGroup = stargroup;
            gameData.stargroups.Add(data);
        }
        saveCurrentGameInfo();
    }

    public void onConditionForNextDaySatisfied () {
        gameData.currentDate += 1;
        saveCurrentGameInfo();
    }

    public void restorePreviousGame() {
        Load();

        List<CharactorData> charactors = gameData.myCharactors;
        foreach (CharactorData data in charactors) {
            charactorBuilder.createCharactorFromCharactorData(data);
        }

        /*  별들도 다시 만들어 줘야 함,,,
        for (int i = 0; i < gameData.whiteStarNum; i++) {

        }
        */
    }

    public void printData() {
        foreach (CharactorData data in gameData.myCharactors) {
            charactorBuilder.createCharactorFromCharactorData(data);
        }
    }

    public void saveCurrentGameInfo () {
        string newGameData = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.dataPath + "/gameStatus.json", newGameData);
    }
}
