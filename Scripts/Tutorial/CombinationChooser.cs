using System;
using UnityEngine;

public class CombinationChooser : MonoBehaviour {
    public ModeSwitchManager gameMode;
    public PlayerInput input;
    public Boy boy;
    public popUpController popUpController;
    public Telescope telescope;

    public void chooseCombinationOfAction (string str, ref Action target) {

        switch(str) {
            case "MVC":
                target += gameMode.onSlideUp;
                target += gameMode.switchState;
                break;
            case "MVS":
                target += gameMode.onSlideDown;
                target += gameMode.switchState;
                break;
            case "MVD":
                target += boy.onInteract;
                break;
            case "MVT":
                target += telescope.onInteract;
                break;
            case "MVH":
                target += gameMode.returnToHome;
                break;
            case "NXT":
                break;
            default: 
                throw new Exception("unexpected triplet code : actionsNeedToDo");
        }
    }


}