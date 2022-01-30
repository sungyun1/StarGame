using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class starAnalyzer
{
    private List<StarConnection> data;

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
        foreach (StarConnection head in data) {
            num += 1;
        }
        return num;
    }

    int numberOfHub () {
        int num = 0;
        foreach (StarConnection head in data) {
            if (head.connections.Count >= 3) num++;
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
            
        }

        if (numberOfStar() < limit) {
            return false;
        }
        else return true;
    }

    public int calculateCharactorID() {

        string type = data[0].connections[0].starType;

        if (isNumberConditionSatisfied(type)) {
            int[] range = rangeOfCharactorID(type);
            Debug.Log(range[0]);
            Debug.Log(range[1]);

            int charactorID = Random.Range(range[0], range[1]);

            return charactorID;
        }

        else return 99;
    }

    public StarConnection getHeadOfList(int num) 
    {
        for (int i = 0; i < data.Count; i++) {
            var item = data[i];
            if (item.connections[0].index == num) 
            {
                return item;
            }
        }
        return null;
    }

    public void addStarToGroup (StarData head, StarData follow) 
    {
        StarConnection list = getHeadOfList(head.index);

        // 별을 리스트에 추가한다.
        if (list == null) 
        {
            // 특정 번호가 없다면 추가한다. 그 이후에 follow를 붙인다.
            StarConnection newlist = new StarConnection();
            newlist.connections = new List<StarData>();
            newlist.connections.Add(head);
            newlist.connections.Add(follow);
            data.Add(newlist);
        }
        else 
        {
            list.connections.Add(follow);
        }
    }
}
