using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    
    // 여러 개의 파일을 각각 끌어와야 하고 그 파일에 담긴 parse 방식도 다를 것이다.

    public CharactorDB database;
    private string fileLocation;

    public List<List<string>> charactorInformation = new List<List<string>>();

    void Start() {
        setFileLocation("/Info/CharactorInfo.csv");
        parse();
    }

    public void setFileLocation (string path) {
        fileLocation = path;
    }

    public void parse () {

        // StreamReader sr = new StreamReader(Application.dataPath + fileLocation);

        bool firstLineProcessed = false; // 현재까지 읽은 라인 개수
        int currentIndex = 0;

        var lines = File.ReadAllLines(Application.dataPath + fileLocation);

        foreach (var sentence in lines) {

            if (firstLineProcessed == false) {
                firstLineProcessed = !firstLineProcessed; 
            }
            else {
                string[] line = sentence.Split(','); // " ~~ " 안에 있는 것들은 다 : 로 표기되었음

                charactorInformation.Add(new List<string>());

                for(int i = 0; i < line.Length; i++)
                {
                    line[i].Replace(":", ",");
                    print(line[i]);
                    charactorInformation[currentIndex].Add(line[i].ToString());
                }
                currentIndex++;
            }
        }

    }

    
    public void passInfoToCharactorDB () {

    }
}
