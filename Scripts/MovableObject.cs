using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable {
    void move(Vector3 startPos, Vector3 endPos);
    // 인터랙션 시 각 애들이 움직일 방향 등을 결정한다.
    // startPos 에서 시작해서 endPos로 흘러간다.
}

public class MovableObject : MonoBehaviour, IMovable
{
    public bool isTransitionOperating = false;

    // 모든 개체들이 한 UI에 담겨서 움직이니까.
    public MovableObject parent;
    private Vector3 startPos;
    private Vector3 endPos;
    // Update is called once per frame
    public virtual void Update()
    {
        if (parent.isTransitionOperating) {
            move(startPos, endPos);
        }
        else move(endPos, startPos);
    }

    public void move (Vector3 startPos, Vector3 endPos) {

        if (Vector3.Distance(gameObject.transform.position, endPos) <= 0.001) {
            return;
        }

        gameObject.transform.position = Vector3.Lerp(
            startPos, endPos, 10f
        );
       
    }

    public void takeMotion() {
        // 일련의 move 로 이루어진 동작 + 추가로 구현할 동작
    }
}
