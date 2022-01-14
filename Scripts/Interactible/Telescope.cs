using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telescope : Entity
{

    public UniverseUpgradeManager upgradeMenu;

    public override void onInteract() {
        upgradeMenu.openUniverseUpgradeMenu();
    }
}
