using UnityEngine;
using System;

public class TutorialStep {
    public string dialogue; // 출력될 문장
    public Action actionsNeedToDo = null; // 끝난 뒤 실행할 것
    public Action conditionsNeedToCheck = null; // 체크해야 할 것

    public CheckConditionStrategy strategy = new CheckConditionStrategy();

    public void checkCondition () {
        strategy.check();
    }

    public void finishStep () {
        actionsNeedToDo?.Invoke();
    }
}

public class CheckConditionStrategy {
    public void check() {
        Debug.Log("hello");
    }
}