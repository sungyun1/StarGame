using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StarData {
    public Vector2 position; // 위치
    public int starType; // 무슨 별인지
    public int index; // 인덱싱
}

public class starAnalyzer
{
    private List<List<StarData>> data;

    void setStarGroup (List<List<StarData>> data) {
        this.data = data;
    }

    int numberOfCertainStar(int type) {
        int num = 0;
        foreach (List<StarData> head in data) {
            foreach (StarData item in head) {
                if (item.starType == type) num++;
            }
        }
        return num / 2;
    }

    int numberOfHub () {
        int num = 0;
        foreach (List<StarData> head in data) {
            if (head.Count >= 3) num++;
        }
        return num / 2;
    }
}
