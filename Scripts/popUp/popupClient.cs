using UnityEngine;
using System;


public interface IpopupClient {
    void beforePopup(string type); // 어떤 팝업을 열어야 하는지 전달
    void afterPopup(); // 팝업의 결과를 전달받음
}

public class popupClient : MonoBehaviour, IpopupClient {
    public virtual void beforePopup(string type) {
        throw new Exception("popup client must override beforePopup()");
    }

    public virtual void afterPopup() {
        throw new Exception("popup client must override afterPopup()");
    }
}