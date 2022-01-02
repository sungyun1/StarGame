using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var list = fetchDataFromScvFile();
    }

    public List<Dictionary<int, string>> fetchDataFromScvFile () {
        var list = new List<Dictionary<int, string>>();
        StreamReader streamReader = new StreamReader(Application.persistentDataPath + "/charactorInfo.csv");

        bool endOfFile = false;
        while (!endOfFile) {
            string data = streamReader.ReadLine(); // 한 줄 읽는다.
            if (data == null) {
                endOfFile = true;
                break;
            }
            string[] data_values = data.Split(','); // ,를 기준으로 잘라서
            var tmp = new Dictionary<int, string>();
            for (int i = 0; i < 10; i++) {
                // 총 10개의 정보가 있으므로
                tmp.Add(i, data_values[i]);
                print(data_values[i]);
            }
        }

        return list;
    }

}
