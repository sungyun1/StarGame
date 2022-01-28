using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class StarGroup {
    public List<List<StarData>> stargroup = new List<List<StarData>>();

    public List<StarData> getHeadOfList(int num) 
    {
        for (int i = 0; i < stargroup.Count; i++) {
            if (stargroup[i][0].index == num) 
            {
                return stargroup[i];
            }
        }
        return null;
    }

    public void addStarToGroup (StarData head, StarData follow) 
    {
        List<StarData> list = getHeadOfList(head.index);

        // 별을 리스트에 추가한다.
        if (list == null) 
        {
            // 특정 번호가 없다면 추가한다. 그 이후에 follow를 붙인다.
            List<StarData> newlist = new List<StarData>();
            newlist.Add(head);
            newlist.Add(follow);
            stargroup.Add(newlist);
        }
        else 
        {
            list.Add(follow);
        }
    }
}


// 별자리는 특정 별 개수가 임의의 개수만큼 있을 때 , 캐릭터가 랜덤으로 생성되는 걸로~~

public class StarCanvas : MonoBehaviour
{
    public GameObject gameObjectManager;
    public ResourceManager gameResource;
    public Diary diary;
    public popUpController popupController;

    public starAnalyzer starAnalyzer = new starAnalyzer();

    public CharactorBuilder charactorBuilder;

    public gameObjectManager pool;

    ///////////////////////////////////////////

    public GameObject LineFolder;

    LineRenderer lr;

    ///////////////////////////////////////////////

    public event Action showNextPopup;

////////////////// 내부 연산용 변수
    private Star buffer = null;
    private int amountOfStarInBuffer = 0;
    private Star starAtMousePos;

    private StarGroup currentStarGroup = new StarGroup();

    private Vector3 coefficient = new Vector3(0.21f, 0.25f, 0);

    public event Action openYesOrNoPopup;

    private Vector3 mousePos;

    public event Action finished;

//////////////////////////// 필요한 함수

    void Awake() {
        popupController.finished += createCharactorFromData;
    }

    void Update() 
    {   
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) ) {
            
            findStarExistAtMousePoint(mousePos, out starAtMousePos);

            if (starAtMousePos != null && isCurrentStarHasSameTypeWithBuffer(starAtMousePos)) {
                storeStarToBuffer(starAtMousePos);
            }
        }
        else if (Input.GetMouseButton(0)) {
            
            findStarExistAtMousePoint(mousePos, out starAtMousePos);

            if (isStarAppropriateToStore(starAtMousePos)) {
                storeStarToBuffer(starAtMousePos);
            }

        }
        else if (Input.GetMouseButtonUp(0)) {
            amountOfStarInBuffer = 0;
        }
    }

    void findStarExistAtMousePoint (Vector3 mousePos, out Star result) {
        GameObject entity = gameObjectManager.GetComponent<InteractionManager>().pickEntity(mousePos);

        if (entity != null) {
            if (entity.GetComponent<Star>() != null) {
                result = entity.GetComponent<Star>();
            }
            else {
                result = null;
            }
        }  
        else result = null;
    }

    bool isStarAppropriateToStore (Star star) {

        if (star != null) {
            if (buffer.index != star.index) {
                if (isCurrentStarHasSameTypeWithBuffer(star)) {
                    return true;
                }
                else return false;
            }
            else return false;
        }
        else return false;
    }

    bool isCurrentStarHasSameTypeWithBuffer (Star star) {
        if (amountOfStarInBuffer == 0) {
            return true;
        }
        else {   
            string type = star.type;

            if (buffer.type == type) return true;
            else return false;
        }
    }

    void storeStarToBuffer (Star entity) 
    {
        

            // 별이 변경되었을 때만 작동
            if (amountOfStarInBuffer == 0)
            {
                buffer = entity;
                amountOfStarInBuffer ++;
            }

            else if (amountOfStarInBuffer == 1) 
            {

                    StarData item = new StarData();
                    item.index = entity.index;
                    item.position = entity.transform.position;
                    item.starType = entity.type;

                    StarData buf = new StarData();
                    buf.index = buffer.index;
                    buf.position = buffer.transform.position;
                    buf.starType = buffer.type;

                    currentStarGroup.addStarToGroup(buf, item);
                    drawLine(buf.position, item.position); 
                    buffer = entity;
            }
    }

    void drawLine(Vector3 p1, Vector3 p2) 
    {
        Vector3 fixed1 = p1 + coefficient;
        Vector3 fixed2 = p2 + coefficient;

        GameObject line = pool.chooseTypeOfPool(ObjectType.line).pullObjectFromPoolTo(LineFolder);
        lr = line.GetComponent<LineRenderer>();
        lr.positionCount += 1;
        lr.SetPosition(0, fixed1);
        lr.positionCount += 1;
        lr.SetPosition(1, fixed2);
    }

    public void createCharactorFromData ()
    {

        if (gameObject.activeSelf) { // 자기가 활성화 되어 있을 때만 ~
        
            pool.chooseTypeOfPool(ObjectType.line);

            int num = LineFolder.transform.childCount;

            for (int i = 0; i < num; i++) {
                GameObject line = LineFolder.transform.GetChild(0).gameObject;
                pool.returnObjectToPool(line);
            }

            starAnalyzer.setStarGroup(currentStarGroup);
            int charactorID = starAnalyzer.calculateCharactorID();

            if (charactorID == 99) {
                popupController.openToastMessage("지난번 사용했던 것보다 더 많은 별을 써야 합니다. 지난번 사용한 별 개수 : " + gameResource.gameData.normalStarGroupCreationLevel);
            }
            else {
                CharactorData ch = charactorBuilder.build(charactorID);
                charactorBuilder.createCharactorFromCharactorData(ch);

                gameResource.addCharactor(ch);
                gameResource.addStarGroup(ch.charactorID, currentStarGroup.stargroup);
                currentStarGroup.stargroup.Clear(); // 다 끝나면 비워야 한다.
                diary.isCharactorFound[ch.charactorID] = true;

                showNextPopup();
            }
            
        }

        finished();
    }

    public void onCharactorCreateButtonClicked () {
        if (openYesOrNoPopup != null) {
            openYesOrNoPopup();
        }
    }

}


