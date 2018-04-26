using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : WorkingAreaBase {

    [SerializeField]
    private Transform collectAreaTransform;

    protected override void Start()
    {
        CollectPosition = collectAreaTransform.position;
        MovementSpeed = 2.5f;
        CollectionSpeed = 150;
        CarryCapacity = 75;

        base.Start();
    }

}
