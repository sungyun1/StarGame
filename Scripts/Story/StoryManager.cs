using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface CheckListForEachDay {
    // 각 날짜별 스토리
    bool check(ResourceManager obj);
}

public class firstDay : CheckListForEachDay {
    public bool check(ResourceManager obj) {
        if (obj.gameData.myCharactors.Count >= 3) {
            return true;
        }
        else return false;
    }
}

public class secondDay : CheckListForEachDay {
    public bool check(ResourceManager obj) {
        return true;
    }
}

public class thirdDay : CheckListForEachDay {
    public bool check(ResourceManager obj) {
        return true;
    }
}

public class fourthDay : CheckListForEachDay {
    public bool check(ResourceManager obj) {
        return true;
    }
}

public class StoryManager : MonoBehaviour
{
    public ResourceManager resourceManager;

    /////////////////////////////////////
    private List<CheckListForEachDay> checkList = new List<CheckListForEachDay>();
    private CheckListForEachDay currentDayList;

    /////////////////////////////// methods

    void Awake() {
        init();
        currentDayList = checkList[0];
    }

    void init() { // 초기 체크리스트들을 등록한다.
        checkList.Add(new firstDay());
        checkList.Add(new secondDay());
        checkList.Add(new thirdDay());
        checkList.Add(new fourthDay());
    }

    public void checkCondition () {
        if (currentDayList.check(resourceManager)) {
            resourceManager.onConditionForNextDaySatisfied();
        }
    }

}
