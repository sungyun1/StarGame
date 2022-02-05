using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DiaryManager : UI_Interface
{
    public GameObject DiaryCanvas;
    public GameObject generalPage;
    public GameObject specificPage;

    private Vector3 openPos = new Vector3 ( 1080 / 2 , 1920 / 2, 0 );
    private Vector3 closePos = new Vector3 ( 1080 * 1.5f, 1920 / 2, 0);

    private bool isMoving = false;
    public bool isGeneralPageOpen = false;
    public bool isSpecificPageOpen = false;

    public void onOpenButtonClicked () {
        // Boy 가 눌렀을 때
        DiaryCanvas.GetComponent<Diary>().fillGeneralPage();
        isGeneralPageOpen = true;

        if (isMotionLocked == false) {
            StartCoroutine(MoveObject(generalPage, openPos));
        }
    }

    public void onCloseButtonClicked () {
        if (isMotionLocked == false) {
            GameObject obj = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
            if (obj.name == "generalPage") {
                isGeneralPageOpen = false;
            }
            else if (obj.name == "detailPage") {
                isSpecificPageOpen = false;
            }
            StartCoroutine(MoveObject(obj, closePos));
        }
        
    }

    public void onSpecificCharactorChosen () {

        isSpecificPageOpen = true;

        GameObject currentBtn = EventSystem.current.currentSelectedGameObject;
        string buttonNo = currentBtn.name.Substring(6,1);
        int btnNo = Convert.ToInt32(buttonNo);

        if (DiaryCanvas.GetComponent<Diary>().isCharactorFound[btnNo]) {
            DiaryCanvas.GetComponent<Diary>().fillSpecificPage(btnNo);
            StartCoroutine(MoveObject(specificPage, openPos));
        }
        else {
            print("the charactor is not found");
        }
        
    }

    public void onMarkSelectionButtonClicked () {
        DiaryCanvas.GetComponent<Diary>().isMarkSelected = !DiaryCanvas.GetComponent<Diary>().isMarkSelected;
        DiaryCanvas.GetComponent<Diary>().fillSpecificPage(0);
    }

    
}
