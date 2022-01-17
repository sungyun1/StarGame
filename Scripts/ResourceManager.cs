using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResourceManager : MonoBehaviour
{

    // 상점, 망원경 도감과 연계하여 구입한 자원을 관리한다.
    // 저장되어야 하는 것들은 전부 여기에 저장한다.
    
    // 주요 재화

    public Data gameData;

    public void Start() {
        Load();
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

    public void addCharactor(Charactor ch) {
        gameData.myCharactors.Add(ch);

    }

    public void addStarGroup(int charactorID, StarGroup stargroup) {
        if (stargroup != null) {
            gameData.stargroups.Add(charactorID, stargroup);
        }
    }
}
