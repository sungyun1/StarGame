using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Telescope : Entity
{

    public UniverseUpgradeManager upgradeMenu;
    public bool isMotionSwitchEnabled = true;

    public event Action checkEventForTutorial;
    public override void onInteract() {
        if (isMotionSwitchEnabled) {
            upgradeMenu.openUniverseUpgradeMenu();
            // checkEventForTutorial();
        }
        
    }
}
