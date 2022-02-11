using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class CSVReader
{
    private static string fileLocation;

    public static void setFileLocation (string path) {
        if (File.Exists(Application.dataPath + "/" + path)) {
            fileLocation = path;
        }
        else throw new System.Exception("there is no file on that directory");
    }

    // ~~.csv 를 해석해서 집어넣어주는 역할
    // 엔터가 있을 경우 삭제하기
    // 아무 내용도 없는 , 역시 삭제하기
    public static List<List<string>> parse () {
        
        List<List<string>> data = new List<List<string>>();

        bool firstLineProcessed = false; 
        int currentIndex = 0;

        var lines = File.ReadAllLines(Application.dataPath + "/" + fileLocation);

        foreach (var sentence in lines) {

            if (firstLineProcessed == false) {
                firstLineProcessed = !firstLineProcessed; 
            }
            else {
                string[] line = sentence.Split(',');
                int length = line.Length;

                data.Add(new List<string>());

                for(int i = 0; i < line.Length; i++)
                {
                    var item = line[i];
                    if (item != "") {
                        string tmp = item.Replace(":", ",").Replace("\"", "");
                        data[currentIndex].Add(tmp);
                        //Debug.Log(tmp);
                    }
                }
                currentIndex++;
            }
        }
        return data;
    }
}
