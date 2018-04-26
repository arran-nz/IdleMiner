using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : WorkingAreaBase {

    protected override void Start()
    {
        MovementSpeed = 1.25f;
        CollectionSpeed = 140;
        CarryCapacity = 200;

        base.Start();
    }

}
