using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boy : Entity
{
    public DiaryManager diaryManager;
    public bool isMotionSwitchEnabled = true;

    public override void onInteract() {
        if (isMotionSwitchEnabled) {
            diaryManager.onOpenButtonClicked();
        }
        
    }


}
