using UnityEngine;

public class LineImageDrawer : MonoBehaviour {
    
    public gameObjectManager pool;
    
    public GameObject LineFolder;
    
    private RectTransform img;

    private Vector2 coefficient = new Vector2(0.21f, 0.25f);

    private Vector2 coefficient2 = new Vector2(-540, -960);
    // 2670 960

    private Vector2 plane = new Vector2(1,0);

    public void drawLine(Vector2 p1, Vector2 p2) 
    {
        Vector2 fixed1 = p1 + coefficient;
        Vector2 fixed2 = p2 + coefficient;
        Vector2 directionVector = p2 - p1;
        
        GameObject lineImage = pool.chooseTypeOfPool("lineImage").pullObjectFromPoolTo(LineFolder);

        img = lineImage.GetComponent<RectTransform>();

        float magnitude = Vector2.Distance(p2, p1);
        float dotProduct = Vector2.Dot(directionVector, new Vector2(1, 0));
        
        img.sizeDelta = new Vector2(magnitude, 7f);
        img.anchoredPosition = p1 + coefficient2;

        float angle = Vector2.Angle(directionVector, plane); // 라디안 값
        img.rotation = Quaternion.Euler(0, 0, angle);

        print(img.anchoredPosition);
        
    }
}