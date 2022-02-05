using UnityEngine;
using System;

public class TutorialStep {
    public string dialogue; // 출력될 문장
    public Action actionsNeedToDo = null; // 끝난 뒤 실행할 것
    public bool isThereConditionToCheck = false;

    public CheckConditionStrategy strategy;
    public bool[] objectToCheck;

    public bool checkCondition () {
        if (strategy != null) {
            return strategy.check();
        }
        else return false;
    }

    public void finishStep () {
        actionsNeedToDo?.Invoke();
    }
}
