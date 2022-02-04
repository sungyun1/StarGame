using UnityEngine;

public class controlTargetSelector {
    // 각 스텝이 어떤 대상을 컨트롤해야 하는지 결정함.
    
    public ModeSwitchManager gameMode;
    public Boy boy;
    public Telescope telescope;

    public GameObject selectControlTarget (string str) {
        GameObject result = null;
        switch(str) {
            case "CSG":
                
            case "NOT":
                
            default: 
                result = gameMode.gameObject;
                break;
        }
        return result;
    }
}