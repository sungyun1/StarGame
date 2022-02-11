using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    public void onInteract(Vector3 touchPos) {

        var entity = pickEntity(touchPos);

        if (entity == null) {
            // 엔티티가 아니면, 아무것도 검사되지 않으면
        }
        else if (entity.GetComponent<Entity>() != null) {
            entity.GetComponent<Entity>().onInteract();
        }
    }

    public GameObject pickEntity (Vector3 touchPos) {

        Vector3 mousePosition = touchPos;

        RaycastHit2D hitInfo = Physics2D.Raycast(
            mousePosition, new Vector3(0, 0, -1), 10f
        );

        if (hitInfo) {
            return hitInfo.collider.gameObject;
        }
        else return null;
    }
}
