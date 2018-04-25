using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : WorkingAreaBase {

    protected override void Start()
    {
        MovementSpeed = 1.75f;
        CollectionSpeed = 20f;
        CarryCapacity = 100f;
        base.Start();
    }

}
