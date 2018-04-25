using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : WorkingAreaBase {

    protected override void Start()
    {
        MovementSpeed = 2.0f;
        CollectionSpeed = 10f;
        CarryCapacity = 50f;
        base.Start();
    }
}
