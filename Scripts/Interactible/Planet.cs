using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Planet : Entity {

    public event Action onPlanetClicked;

    public override void onInteract() {
        print("planet clicked");
        onPlanetClicked();
    }
}
