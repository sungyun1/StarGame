using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : Entity {
    new public string name; 

    public int charactorIndex;
    public int type; // 0 : normal 1 : epic 2 : rare
    public Vector2 position; // 놓일 위치

    public Charactor (string name, int type, int charactorIndex, Vector2 position ) {
        this.name = name;
        this.type = type;
        this.charactorIndex = charactorIndex;
        this.position = position;
    }
}

public class CharactorBuilder {
    // Builder 패턴
    // 메소드 체이닝으로 정보 입력할 수 있도록 할 것.

    private string name;
    private int type;

    private int index;
    private Vector2 position;

    public CharactorBuilder setName (string charactorName) {
        this.name = charactorName;
        return this;
    }

    public CharactorBuilder setPosition (Vector2 pos) {
        this.position = pos;
        return this;
    }

    public CharactorBuilder setIndex (int index) {
        this.index = index;
        return this;
    }

    public CharactorBuilder setType (int type) {
        this.type = type;
        return this;
    }

    public Charactor build () {
        Charactor ch = new Charactor (this.name, this.type, this.index, this.position);
        return ch;
    }

}