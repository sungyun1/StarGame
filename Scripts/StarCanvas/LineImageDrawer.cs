using UnityEngine;

public class LineImageDrawer : MonoBehaviour {
    
    public gameObjectManager pool;
    
    public GameObject LineFolder;
    
    private RectTransform img;

    private Vector2 coefficient = new Vector2(0.21f, 0.25f);

    public void drawLine(Vector2 p1, Vector2 p2) 
    {
        Vector2 fixed1 = p1 + coefficient;
        Vector2 fixed2 = p2 + coefficient;
        
        GameObject lineImage = pool.chooseTypeOfPool("lineImage").pullObjectFromPoolTo(LineFolder);

        img = lineImage.GetComponent<RectTransform>();

        float magnitude = Vector2.Distance(p1, p2);

        print(img.rect.height);
        
    }
}