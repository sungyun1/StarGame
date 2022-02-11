using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Boy : Entity
{
    public DiaryManager diaryManager;
    public ResourceManager gameResource;
    public bool isMotionSwitchEnabled = true;
    public event Action checkEventForTutorial;

    private float previousTime;
    private float timeInterval = 10;

    private int temporaryStarDust = 0;

    public Button starGatheringButton;

    public override void onInteract() {
        if (isMotionSwitchEnabled) {
            diaryManager.onOpenButtonClicked();
            // checkEventForTutorial();
        }   
    }

    void Update () {
        temporaryStarDust += 1;
        if (Time.time >= previousTime + timeInterval) {
            showButton();
        }
    }

    public void showButton () {
        starGatheringButton.gameObject.SetActive(true);
    }

    public void onButtonClicked() {
        starGatheringButton.gameObject.SetActive(false);
        gameResource.onGatherStarDust(temporaryStarDust);
        temporaryStarDust = 0;
        previousTime = Time.time;
    }


}
