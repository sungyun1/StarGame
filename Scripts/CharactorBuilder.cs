using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharactorData {
    public string name;
    public string type;
    public int charactorID;

    public CharactorData (string name, string type, int id){
        this.name = name;
        this.type = type;
        this.charactorID = id;
    }
}

public class CharactorBuilder : MonoBehaviour{
    // 캐릭터 id를 받으면 그걸로 해당하는 캐릭터를 찾아온다.

    public CharactorDB database;

    public CharactorData build (int charactorID) {

        string name = database.getCharactorInformation(charactorID, CharactorDB.DataIndex.name);
        int index = charactorID;
        string type = database.getCharactorInformation(charactorID, CharactorDB.DataIndex.type);

        CharactorData ch = new CharactorData (name, type, index);
        return ch;
    }

}