using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    
    // 여러 개의 파일을 각각 끌어와야 하니까

    public CharactorDB database;
    private string fileLocation;

    public List<List<string>> charactorInformation = new List<List<string>>();

    void Start() {
        setFileLocation("/Info/CharactorInfo.csv").parse();
    }

    public CSVReader setFileLocation (string path) {
        fileLocation = path;
        return this;    
    }

    public void parse () {

        StreamReader sr = new StreamReader(Application.dataPath + fileLocation);

        bool endOfFile = false;
        int currentIndex = 0;
        while (!endOfFile)
        {
            string line = sr.ReadLine(); // 한 줄 읽어오기

            if(line == null)
            {
                endOfFile = true;
                break;
            }

            string[] lineArray = line.Split(',');

            charactorInformation.Add(new List<string>());

            for(int i = 0; i < lineArray.Length; i++)
            {
                charactorInformation[currentIndex].Add(lineArray[i].ToString());
            }
            currentIndex++;
        }
    }

    public bool determineSentenseOrNot() {
        return true;
    }
}
