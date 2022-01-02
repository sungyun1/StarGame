using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 인터랙션이 가능한 친구들은 전부 이 녀석을 상속받을 것이다.
public interface IEntity
{
    // 이녀석을 반드시 구현해야 함. 내부적으로 어떤 함수가
    // 정의되어 있는지 Unity 에게 알려주기 위한 기능
    // 터치 시 반응
    void onInteract();
}

public class Entity : MonoBehaviour, IEntity {

    public virtual void onInteract() {
        // print(gameObject.name); // 자기 자신의 이름을 출력.
    }

}
