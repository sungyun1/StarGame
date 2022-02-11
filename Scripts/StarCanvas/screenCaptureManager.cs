using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class screenCaptureManager : MonoBehaviour
{

    private float margin = 15;
    private Texture2D screenTex;
    private Rect areaWantToCapture;
    public bool willTakeScreenShot = false;
    private string folderPath;

    void Awake() {
        folderPath = Application.dataPath + "/" + "ScreenShot";

        int width = Screen.width;
        int height = Screen.height;
        screenTex = new Texture2D (
            width, height, TextureFormat.RGB24, false
        );

        areaWantToCapture = new Rect(
            0f, 0f, width, height
        );

        // willTakeScreenShot = true;
    }

    private void OnPostRender() {
        if (willTakeScreenShot) {
            willTakeScreenShot = false;

            if (Directory.Exists(folderPath)) {
                saveImage();
            } else {
                throw new Exception("Screenshot 폴더가 없습니다");
            }

        }
    }

    void saveImage() {
        screenTex.ReadPixels(areaWantToCapture, 0, 0);
        string filePath = folderPath + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".png";

        if (File.Exists(filePath) == false) {
            using (FileStream fs = File.Create(filePath)) {
                print("screenshot start");
                File.WriteAllBytes(filePath, screenTex.EncodeToPNG());
                print("screenshot end");
            }
        }
        
        
        
        Destroy(screenTex);
    }

    void loadImage() {

    }
}
