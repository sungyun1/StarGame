using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorBuilder : MonoBehaviour{
    // 캐릭터 id를 받으면 그걸로 해당하는 캐릭터를 찾아온다.

    public CharactorDB database;

    public Charactor build (int charactorID) {

        string name = database.getCharactorInformation(charactorID, CharactorDB.DataIndex.name);
        int index = charactorID;
        string type = database.getCharactorInformation(charactorID, CharactorDB.DataIndex.type);
        Vector2 pos = new Vector2 (
            1.3f, -3.5f
        );

        Charactor ch = new Charactor (name, type, index);
        return ch;
    }

}