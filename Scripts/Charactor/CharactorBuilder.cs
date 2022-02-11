using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharactorData {
    public string name;
    public string type;
    public int charactorID;

    public Vector2 position; // 저장을 생각하면 여기도 있어야 함

    public CharactorData (string name, string type, int id){
        this.name = name;
        this.type = type;
        this.charactorID = id;
    }
}

public class CharactorBuilder : MonoBehaviour{

    public CharactorDB database;
    public GameObject CharactorPrefab;
    public GameObject CharactorFolder;

    public CharactorData build (int charactorID) {

        string name = database.getCharactorInformation(charactorID, CharactorDB.DataIndex.name);
        int index = charactorID;
        string type = database.getCharactorInformation(charactorID, CharactorDB.DataIndex.type);

        CharactorData ch = new CharactorData (name, type, index);
        ch.position = new Vector2 (
            Random.Range(-2, 2),
            Random.Range(-2.27f, -3.6f)
        );
        return ch;
    }

    public void createCharactorFromCharactorData (CharactorData data) {
        GameObject newch = Instantiate(CharactorPrefab, CharactorFolder.transform);
        newch.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Charactor/Home/" + data.type + '/' + data.name);
        newch.transform.position = data.position;
    }

}