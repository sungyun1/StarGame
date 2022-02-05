using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UniverseUpgradeManager : UI_Interface
{

    public UniverseUpgrade universe;

    public ModeSwitchManager gameMode;

    public GameObject DetailUniverse;
    public GameObject normalUniverse;
    public GameObject rareUniverse;
    public GameObject epicUniverse;

    public GameObject panel;

    //////////////////////////////////

    private Vector3 openPos = new Vector3 ( 1080 / 2 , 1920 / 2, 0 );
    private Vector3 closePos = new Vector3 ( 1080 * 1.5f, 1920 / 2, 0);

    public bool isUniverseUpgradeOpen = false;

    public void openUniverseUpgradeMenu () {
        isUniverseUpgradeOpen = true;
        StartCoroutine(MoveObject(universe.gameObject, openPos));
        gameMode.isMotionSwitchEnabled = false;
        universe.isMotionLocked = false;
    }

    public void closeUniverseUpgradeMenu () {
        isUniverseUpgradeOpen = false;
        StartCoroutine(MoveObject(universe.gameObject, closePos));
        gameMode.isMotionSwitchEnabled = true;
        universe.isMotionLocked = true;
    }

    public void openSpecificPage () {
        chooseSpecificUniverse();
        StartCoroutine(MoveObject(DetailUniverse, openPos));
        StartCoroutine(Fade(panel, 0.5f));
    }

    public void closeSpecificPage () {
        StartCoroutine(MoveObject(DetailUniverse, closePos));
    }

    public void chooseSpecificUniverse() {
        switch (EventSystem.current.currentSelectedGameObject.name) {
            case "normal":
                normalUniverse.SetActive(true);
                rareUniverse.SetActive(false);
                epicUniverse.SetActive(false);
                break;
            case "rare":
                normalUniverse.SetActive(false);
                rareUniverse.SetActive(true);
                epicUniverse.SetActive(false);
                break;
            case "epic":
                normalUniverse.SetActive(false);
                rareUniverse.SetActive(false);
                epicUniverse.SetActive(true);
                break;
            default:
                break;
        }
    }
}
