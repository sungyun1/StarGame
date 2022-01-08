using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// 별자리는 특정 별 개수가 임의의 개수만큼 있을 때 , 캐릭터가 랜덤으로 생성되는 걸로~~

public class StarCanvas : MonoBehaviour
{
    public GameObject gameObjectManager;
    public GameObject gameResource;
    
    public GameObject Diary;

    public popUpController popUp;

    public CharactorBuilder charactorBuilder = new CharactorBuilder();
    public GameObject CharactorPrefab;
    public GameObject CharactorFolder;

    public GameObject Lines;

    public GameObject LinePrefab;

    LineRenderer lr;

////////////////// 내부 연산용 변수
    private List<StarData> buffer = new List<StarData>();

    private StarGroup tmp = new StarGroup();

    private Vector3 coefficient = new Vector3(0.21f, 0.25f, 0);

    public event Action openYesOrNoPopup;

////////////////// 자료구조

public struct StarData {
    public Vector2 position; // 위치
    public int starType; // 무슨 별인지
    public int index; // 인덱싱
}

class StarGroup {
    private List<List<StarData>> stargroup = new List<List<StarData>>();

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

//////////////////////////// 필요한 함수

    void Awake() {
        popUp.finished += createCharactorFromData;
    }

    void Update() 
    {
        if (Input.GetMouseButtonDown(0)) {
            storeStar();
        }
        else if (Input.GetMouseButton(0)) {

        }
        else if (Input.GetMouseButtonUp(0)) {
            
        }
    }

    void storeStar () 
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject entity = gameObjectManager.GetComponent<GameObjectManager>().pickEntity(mousePos);

        if (entity != null) 
        {
            StarData item;
            item.index = entity.GetComponent<Star>().index;
            item.position = entity.GetComponent<Transform>().position;
            item.starType = 0;

            if (buffer.Count == 0)
            {
                buffer.Add(item);
            }

            else if (buffer.Count == 1) 
            {
                buffer.Add(item);
                tmp.addStarToGroup(buffer[0], buffer[1]);
                drawLine(buffer[0].position, buffer[1].position);
                buffer.Clear();
            }
        }
    }

    void drawLine(Vector3 p1, Vector3 p2) 
    {
        Vector3 fixed1 = p1 + coefficient;
        Vector3 fixed2 = p2 + coefficient;

        GameObject line = Instantiate(LinePrefab, Lines.transform);
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
        if (gameObject.activeSelf) { // 자기가 활성화 되어 있을 때만 ~
            Charactor ch = charactorBuilder
                .setName("러너")
                .setType(0)
                .setIndex(1)
                .setPosition(new Vector2 (1.3f, -3.5f))
            .build();

            GameObject newch = Instantiate(CharactorPrefab, CharactorFolder.transform);
            newch.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Charactor/Home/normal/" + ch.name);
            newch.transform.position = ch.position;

            gameResource.GetComponent<ResourceManager>().addCharactor(ch);
            Diary.GetComponent<Diary>().isCharactorFound[ch.charactorIndex] = true;
        }
    }

    public void onCharactorCreateButtonClicked () {
        // 안 하면 nullRef 에러
        if (openYesOrNoPopup != null) {
            openYesOrNoPopup();    
        }
    }
}


