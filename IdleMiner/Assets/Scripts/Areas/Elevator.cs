using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : WorkingAreaBase {
    protected override void Start()
    {
        MovementSpeed = 1.75f;
        CollectionSpeed = 60f;
        CarryCapacity = 250f;
        base.Start();
    }

}
