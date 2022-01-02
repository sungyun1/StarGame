using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StatusLoader : MonoBehaviour
{
    // json 을 이용해서 가져온다.

    public class UserData {
        public int currentStarDust;
        public int currentGem;

    }


    public UserData currentGame; // 현재 게임 상황

    public void checkIsItNewGame() {
        // 새로 시작하는지 확인한다.
        // 만일 새로 시작이면 모든 키를 0으로 초기화
        // 아니면 이전 키를 가져온다.
    }

    public void initialize () {
        currentGame.currentStarDust = 0;
        currentGame.currentGem = 0;
    }

    public void exitGame() {
        // json 파일로 저장
    }

    public void loadGame () {
        // save 의 모든 키를 가져다가 여기에 저장한다.
    }
}
