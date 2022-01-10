using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Entity
{
    // Start is called before the first frame update
    public string color { get; set;}
    public int index { get; set;}

    public bool isUsed = false;

    public override void onInteract() {
        print(this.index);
    }

}
