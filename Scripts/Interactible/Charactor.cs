using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : Entity {
    new public string name; 

    public int charactorIndex;
    public string type; // 0 : normal 1 : epic 2 : rare

    public Charactor (string name, string type, int charactorIndex) {
        this.name = name;
        this.type = type;
        this.charactorIndex = charactorIndex;
    }

    public CharactorData charactorToCharactorData() {
        return new CharactorData(name, type, charactorIndex); 
    }
}

