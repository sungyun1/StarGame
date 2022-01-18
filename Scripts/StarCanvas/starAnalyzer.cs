using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StarData {
    public Vector2 position; // 위치
    public string starType; // 무슨 별인지
    public int index; // 인덱싱
}

public class starAnalyzer
{
    private List<List<StarData>> data;

    public ResourceManager resource;

    public void setStarGroup (StarGroup Target) {
        if (Target != null) {
            this.data = Target.stargroup;
        }
    }

    int[] rangeOfCharactorID (string StarType) {

        int[] result;

        switch (StarType) {
            case "white":
                result = new int[2] {0, 9};
                break;
            case "yellow":
                result = new int[2] {10, 16};
                break;
            case "blue":
                result = new int[2] {17, 19};
                break;
            default:
                result = null;
                break;
        }

        return result;
    }

    int numberOfStar () {
        int num = 0;
        foreach (List<StarData> head in data) {
            num += 1;
        }
        return num;
    }

    int numberOfHub () {
        int num = 0;
        foreach (List<StarData> head in data) {
            if (head.Count >= 3) num++;
        }
        return num / 2;
    }

    bool isNumberConditionSatisfied (string type) {
        
        var limit = 2;
        if (type == "normal") {
            limit = resource.gameData.normalStarGroupCreationLevel;
        }
        else if (type == "rare") {
            limit = resource.gameData.rareStarGroupCreationLevel;
        }
        else if (type == "epic") {
            limit = resource.gameData.epicStarGroupCreationLevel;
        } else {
            Debug.Log("에러 발생");
        }

        if (numberOfStar() < limit) {
            return false;
        }
        else return true;
    }

    public int calculateCharactorID() {

        string type = data[0][0].starType;

        if (isNumberConditionSatisfied(type)) {
            int[] range = rangeOfCharactorID(type);
            Debug.Log(range[0]);
            Debug.Log(range[1]);

            int charactorID = Random.Range(range[0], range[1]);

            return charactorID;
        }

        else return 99;
    }
}
