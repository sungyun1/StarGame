using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Diary : MonoBehaviour {

    public Image CharactorImage;
    public Image SpaceMarkImage;
    public Text CharactorDescriptionText;
    public Button Canclebutton;

    public bool isMarkSelected = false;

    public CharactorDB charactorDB;

    public List<bool> isCharactorFound;
    public List<Button> Buttons;
    private int totalCharactorNum = 22;
    private int buttonNum = 6;
    
    public void Start () {

        // found 면 신경쓰고, 아니면 신경 쓰지 않음. 열람조차 안 되게 함.

        isCharactorFound = new List<bool>();

        for (int i = 0; i < totalCharactorNum; i++) {
            isCharactorFound.Add(false);
        }

    }

    public void fillGeneralPage () {

        for (int i = 0; i < buttonNum; i++) {

            CharactorDB.DataIndex info = CharactorDB.DataIndex.hiddenStateSpritePath;

            if (isCharactorFound[i] == true) {
                info = CharactorDB.DataIndex.openStateSpritePath;
            }

            Buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(
                charactorDB.getCharactorInformation(
                    i, info
                )
            ) as Sprite;

        }
    }

    public void fillSpecificPage (int charactorNumber) {

        if ( isCharactorFound[charactorNumber] ) {

            // description
            CharactorDescriptionText.text = charactorDB.getCharactorInformation(charactorNumber, CharactorDB.DataIndex.description);
            
            // charactor image

            string detailpath;
            if (isMarkSelected) 
            {
                detailpath = charactorDB.getCharactorInformation(charactorNumber, CharactorDB.DataIndex.diaryDetailSpritePath);
            }
            else 
            {
                detailpath = charactorDB.getCharactorInformation(charactorNumber, CharactorDB.DataIndex.diaryDrawingSpritePath);
            }

            CharactorImage.sprite = Resources.Load<Sprite> (
                detailpath
            ) as Sprite;
            
            // space mark
            SpaceMarkImage.sprite = Resources.Load<Sprite> (
                charactorDB.getCharactorInformation(charactorNumber, CharactorDB.DataIndex.diarySpaceMarkSpritePath)
            ) as Sprite;
        }
    }

}

