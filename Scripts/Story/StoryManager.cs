using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : UI_Interface
{
    public ResourceManager gameResource;

    // 일자별 기준
    private CSVReader reader = new CSVReader();
    private List<List<string>> diaryContext = new List<List<string>>();

    void Awake() {
        diaryContext = reader.setFileLocation("Info/diaryContext.csv").parse();
    }
}
