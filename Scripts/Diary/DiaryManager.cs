using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DiaryManager : MonoBehaviour
{
    public GameObject DiaryCanvas;
    public GameObject generalPage;
    public GameObject specificPage;

    private Vector3 openPos = new Vector3 ( 1080 / 2 , 1920 / 2, 0 );
    private Vector3 closePos = new Vector3 ( 1080 * 1.5f, 1920 / 2, 0);

    private bool isMoving = false;

    IEnumerator Move (GameObject Page, Vector3 Destination) {

        while (Vector3.Distance(Page.transform.position, Destination) >= 0.1) {
            Page.transform.position = Vector3.Lerp(Page.transform.position, Destination, 0.01f);
            yield return new WaitForSeconds(0.001f);
        }
        isMoving = false;
        print(Page.transform.position);
    }

    public void onOpenButtonClicked () {
        // Boy 가 눌렀을 때
        DiaryCanvas.GetComponent<Diary>().fillGeneralPage();
        StartCoroutine(Move(generalPage, openPos));
    }

    public void onCloseButtonClicked () {
        GameObject obj = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        StartCoroutine(Move(obj, closePos));
    }

    public void onSpecificCharactorChosen () {

        GameObject currentBtn = EventSystem.current.currentSelectedGameObject;
        string buttonNo = currentBtn.name.Substring(6,1);
        int btnNo = Convert.ToInt32(buttonNo);

        if (DiaryCanvas.GetComponent<Diary>().isCharactorFound[btnNo]) {
            DiaryCanvas.GetComponent<Diary>().fillSpecificPage(btnNo);
            StartCoroutine(Move(specificPage, openPos));
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
