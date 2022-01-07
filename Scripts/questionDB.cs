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

    private CSVReader reader = new CSVReader();

    private List<List<string>> data;

    void Awake() {
        data = reader  
            .setFileLocation("Info/QuestionInfo.csv")
            .parse();
    }

    void getInformation (int charactorNum, DataIndex indexOfInformatinToFind) {
        print(
            data[charactorNum][(int) indexOfInformatinToFind]
        );
    }
}
