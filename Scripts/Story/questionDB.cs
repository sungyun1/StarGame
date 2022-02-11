using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DataIndex {
       name = 0,
       type,
       question,
       firstChoice,
       secondChoice,
       answer,
       firstChoiceMessage,
       secondChoiceMessage
}

public class questionDB : MonoBehaviour
{
    private List<List<string>> data;

    void Awake() {
        CSVReader.setFileLocation("Info/QuestionInfo.csv");
        data = CSVReader.parse();
    }

    public string getInformation (int charactorNum, DataIndex indexOfInformatinToFind) {
        return data[charactorNum][(int) indexOfInformatinToFind];
        
    }
}
