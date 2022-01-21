using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telescope : Entity
{

    public UniverseUpgradeManager upgradeMenu;
    public bool isMotionSwitchEnabled = true;

    public override void onInteract() {
        if (isMotionSwitchEnabled) {
            upgradeMenu.openUniverseUpgradeMenu();
        }
    }
}
