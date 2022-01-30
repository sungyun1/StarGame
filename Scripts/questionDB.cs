using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionDB : MonoBehaviour
{
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

    private List<List<string>> data;

    void Awake() {
        CSVReader.setFileLocation("Info/QuestionInfo.csv");
        data = CSVReader.parse();
    }

    void getInformation (int charactorNum, DataIndex indexOfInformatinToFind) {
        print(
            data[charactorNum][(int) indexOfInformatinToFind]
        );
    }
}
