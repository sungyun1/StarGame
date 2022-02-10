using UnityEngine;

public class LineImageDrawer : MonoBehaviour {
    
    public gameObjectManager pool;
    
    public GameObject LineFolder;
    
    private RectTransform img;

    private Vector2 coefficient = new Vector2(0.21f, 0.25f);

    public void drawLine(Vector2 p1, Vector2 p2) 
    {
        print(p1.x);
        print(p1.y);
        Vector2 fixed1 = p1 + coefficient;
        Vector2 fixed2 = p2 + coefficient;
        Vector2 directionVector = p2 - p1;
        
        GameObject lineImage = pool.chooseTypeOfPool("lineImage").pullObjectFromPoolTo(LineFolder);

        img = lineImage.GetComponent<RectTransform>();

        float magnitude = Vector2.Distance(p2, p1);
        float dotProduct = Vector2.Dot(directionVector, new Vector2(1, 0));
        
        img.sizeDelta = new Vector2(magnitude, 7f);
        print(img.anchoredPosition);
        
    }
}