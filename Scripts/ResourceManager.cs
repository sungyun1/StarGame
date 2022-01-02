using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    // 상점, 망원경 도감과 연계하여 구입한 자원을 관리한다.
    // 저장되어야 하는 것들은 전부 여기에 저장한다.
    
    // 주요 재화
    public int gemNum = 0;
    public int starDust;

    int tmp = 0;

    // 별 개수
     private int whiteStarNum = 0;
    private int yellowStarNum = 0;
    private int blueStarNum = 0;
    
    // 업그레이드 내역
    private int telescopeLevel = 0;

    private int playerLevel = 0;

    private List<Charactor> myCharactors = new List<Charactor>();

    public void Start() {
        starDust = 500;
        
    }

    public void Update() {
        
        
        
    }

    public void onBuyStar() {
        starDust -= 100;
        whiteStarNum += 1;
        print(whiteStarNum);
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
}
