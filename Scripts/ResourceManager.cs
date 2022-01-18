using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    // 캐릭터 관련

    public List<CharactorData> myCharactors = new List<CharactorData>();

    // 별자리 보관 내역 (캐릭터 id + 그 캐릭터 별자리)
    public List<StarGroupData> stargroups = new List<StarGroupData>();
}

[System.Serializable]
public class StarGroupData {
    public int charactorID;
    public List<List<StarData>> starGroup;
}

public class ResourceManager : MonoBehaviour
{

    public CharactorBuilder charactorBuilder;

    public Data gameData;

    public void Start() {
        restorePreviousGame();
    }

    public void Load () {
        string dataFromFile = File.ReadAllText(Application.dataPath + "/gameStatus.json");
        print(dataFromFile);
        gameData = JsonUtility.FromJson<Data>(dataFromFile);
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
    }

    public void onUpgradeTelescope () {
        gameData.telescopeLevel += 1;
    }

    public void onUpgradeBoy () {
        gameData.playerLevel += 1;
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
    }

    public void addStarGroup(int charactorID, List<List<StarData>> stargroup) {
        if (stargroup != null) {
            StarGroupData data = new StarGroupData();
            data.charactorID = charactorID;
            data.starGroup = stargroup;
            gameData.stargroups.Add(data);
        }
    }

    public void restorePreviousGame() {
        // 캐릭터 화면에 표시
        Load();
        foreach (CharactorData data in gameData.myCharactors) {
            charactorBuilder.createCharactorFromCharactorData(data);
        }
    }

    public void saveCurrentGameInfo () {
        string newGameData = JsonUtility.ToJson(gameData);
        print(newGameData);
        File.WriteAllText(Application.dataPath + "/gameStatus.json", newGameData);
    }
}
