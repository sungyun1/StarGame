using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader
{
    private string fileLocation;

    public CSVReader setFileLocation (string path) {
        if (File.Exists(Application.dataPath + "/" + path)) {
            fileLocation = path;
            return this;
        }
        else throw new System.Exception("there is no file on that directory");
    }

    // ~~.csv 를 해석해서 집어넣어주는 역할
    public List<List<string>> parse () {
        
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

                data.Add(new List<string>());

                for(int i = 0; i < line.Length; i++)
                {
                    string tmp = line[i].Replace(":", ",").Replace("\"", "");
                    data[currentIndex].Add(tmp);
                }
                currentIndex++;
            }
        }

        return data;
    }
}
