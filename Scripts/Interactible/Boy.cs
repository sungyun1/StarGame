using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boy : Entity
{
    public DiaryManager diaryManager;

    public override void onInteract() {
        diaryManager.onOpenButtonClicked();
    }


}
