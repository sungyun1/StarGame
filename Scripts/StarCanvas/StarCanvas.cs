using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public popUpController popUp;

    public starAnalyzer starAnalyzer = new starAnalyzer();

    public CharactorBuilder charactorBuilder;

    public ObjectPoolManager lineObjectPool;

    ///////////////////////////////////////////
    public GameObject CharactorPrefab;
    public GameObject CharactorFolder;

    public GameObject Lines;

    public GameObject LinePrefab;

    LineRenderer lr;

////////////////// 내부 연산용 변수
    private StarData buffer = new StarData();
    private int amountOfStarInBuffer = 0;
    private string typeOfStar = "blue";
    private Star starAtMousePos;


    private StarGroup tmp = new StarGroup();

    private Vector3 coefficient = new Vector3(0.21f, 0.25f, 0);

    public event Action openYesOrNoPopup;

    private Vector3 mousePos;

//////////////////////////// 필요한 함수

    void Awake() {
        popUp.finished += createCharactorFromData;

        lineObjectPool.setObjectPrefab(LinePrefab);

    }

    void Update() 
    {   
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) ) {
            
            findStarExistAtMousePoint(mousePos, out starAtMousePos);

            if (starAtMousePos != null) {
                storeStarToBuffer(starAtMousePos);
            }
        }
        else if (Input.GetMouseButton(0)) {
            
            findStarExistAtMousePoint(mousePos, out starAtMousePos);

            if (starAtMousePos != null && buffer.index != starAtMousePos.index) {
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
                print("star found");
                result = entity.GetComponent<Star>();
            }
            else {
                print("star not found");
                result = null;
            }
        }  
        else result = null;
    }

    bool ifStarHasAlreadyBeenVisited (Star star) {
        int index = star.index;

        if (tmp.getHeadOfList(index) == null) return false;
        else return true; 
    }

    void storeStarToBuffer (Star entity) 
    {
            StarData item;
            item.index = entity.index;
            item.position = entity.transform.position;
            item.starType = 0;

                // 별이 변경되었을 때만 작동
                if (amountOfStarInBuffer == 0)
                {
                    buffer = item;
                    amountOfStarInBuffer ++;
                }

                else if (amountOfStarInBuffer == 1) 
                {
                    tmp.addStarToGroup(buffer, item);
                    drawLine(buffer.position, item.position); 
                    buffer = item;
                }
    }

    void drawLine(Vector3 p1, Vector3 p2) 
    {
        Vector3 fixed1 = p1 + coefficient;
        Vector3 fixed2 = p2 + coefficient;

        GameObject line = lineObjectPool.pullObjectFromPoolTo(Lines);
        lr = line.GetComponent<LineRenderer>();
        lr.positionCount += 1;
        lr.SetPosition(0, fixed1);
        lr.positionCount += 1;
        lr.SetPosition(1, fixed2);
    }

    void restoreStarGroupFromData (StarGroup data) 
    {

    }

    public void createCharactorFromData ()
    {

        int num = Lines.transform.childCount;

        for (int i = 0; i < num; i++) {

        }

        // starAnalyzer.setStarGroup(tmp);
        int charactorID = starAnalyzer.calculateCharactorID();

        if (gameObject.activeSelf) { // 자기가 활성화 되어 있을 때만 ~

            Charactor ch = charactorBuilder.build(charactorID);

            GameObject newch = Instantiate(CharactorPrefab, CharactorFolder.transform);
            newch.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Charactor/Home/normal/" + ch.name);
            newch.transform.position = new Vector2 (1.3f, -3.5f);

            gameResource.addCharactor(ch);
            diary.isCharactorFound[ch.charactorIndex] = true;
        }
    }

    public void onCharactorCreateButtonClicked () {
        if (openYesOrNoPopup != null) {
            openYesOrNoPopup();
        }
        // gameResource.addStarGroup(0, tmp);
    }
}


