using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    // 상점, 망원경 도감과 연계하여 구입한 자원을 관리한다.
    // 저장되어야 하는 것들은 전부 여기에 저장한다.
    
    // 주요 재화
    public int gemNum = 0;
    public int starDust = 0;

    // 별 개수
     private int whiteStarNum = 0;
    private int yellowStarNum = 0;
    private int blueStarNum = 0;
    
    // 업그레이드 내역
    private int telescopeLevel = 0;

    private int playerLevel = 0;

    private List<Charactor> myCharactors = new List<Charactor>();

    // 별자리 보관 내역 (이름 + 그 캐릭터 별자리)
    public Dictionary<int, StarGroup> stargroups = new Dictionary<int, StarGroup>();

    public void Start() {
        giveInitialResource();
    }

    public void giveInitialResource() {
        starDust = 500;
    }

    public void onBuyStar(string starType, int price) {
        starDust -= price;
        switch (starType) {
            case "white":
                whiteStarNum ++;
                break;
            case "yellow":
                yellowStarNum ++;
                break;
            case "blue":
                blueStarNum ++;
                break;
            default:
                break;
        }
    }

    public void onUpgradeTelescope () {
        telescopeLevel += 1;
    }

    public void onUpgradeBoy () {
        playerLevel += 1;
    }

    public void addCharactor(Charactor ch) {
        myCharactors.Add(ch);

    }

    public void addStarGroup(int charactorID, StarGroup stargroup) {
        if (stargroup != null) {
            stargroups.Add(charactorID, stargroup);
        }
    }
}
