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

        StreamReader sr = new StreamReader(Application.dataPath + fileLocation);

        bool endOfFile = false;
        bool firstLineProcessed = false; // 현재까지 읽은 라인 개수
        int currentIndex = 0;
        while (!endOfFile)
        {
            // 한 줄 읽어오기.
            // 첫 번째 줄은 인덱싱이므로 무시해야 할 듯.
            string line = sr.ReadLine(); 

            if(line == null)
            {
                endOfFile = true;
                break;
            }

            if (firstLineProcessed != true) {
                firstLineProcessed = true;
            }

            else if (firstLineProcessed) {

                // 이거 일단, " 가 나오면 그 자리에 : 를 넣고 이후에 : 로 자르는 게 정신건강에 좋을 것 같다.
                // 이렇게 하면 특정한 형식에 구애를 받게 될 것 같아서, 이런 식으로 자르자.


                string[] lineArray = line.Split('\"'); // 크게 description 과 아닌 것으로 나눈다.
                string [] others = lineArray[0].Split(','); // 그걸 각각의 타입으로 쪼갠다.
                string sentence = lineArray[1].ToString();

                charactorInformation.Add(new List<string>());

                for(int i = 0; i < others.Length; i++)
                {
                    charactorInformation[currentIndex].Add(others[i].ToString());
                }
                charactorInformation[currentIndex].Add(sentence);
                currentIndex++;
            }
            
        }
    }

    public void passInfoToCharactorDB () {

    }
}
