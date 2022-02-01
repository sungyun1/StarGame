using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupDialogueManager : MonoBehaviour
{
    // popup에 무슨 내용이 들어가는지 결정한다.

    private Dictionary<currentDescriptionState, string> popupMessasgeContext = new Dictionary<currentDescriptionState, string>();

    void Awake() {
        popupMessasgeContext.Add(currentDescriptionState.notEnoughStarDust, "별가루가 부족합니다.");
        popupMessasgeContext.Add(currentDescriptionState.notEnoughGem, "젬이 부족합니다.");
        popupMessasgeContext.Add(currentDescriptionState.whenBoughtStar, "별을 구매하였습니다.");
        popupMessasgeContext.Add(currentDescriptionState.whenCreatedStarGroup, "새로운 별자리를 만들었습니다.");
        popupMessasgeContext.Add(currentDescriptionState.whenUpgradedPlayer, "플레이어 업그레이에 성공했습니다.");
        popupMessasgeContext.Add(currentDescriptionState.whenUpgradedUniverse, "우주 업그레이드에 성공했습니다.");
    }

    public string chooseMessage (currentDescriptionState state) {
        return popupMessasgeContext[state];
    }

}
