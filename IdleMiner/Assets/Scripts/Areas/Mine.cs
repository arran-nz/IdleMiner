using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : WorkingAreaBase {

    [SerializeField]
    private Transform collectAreaTransform;

    protected override void Start()
    {
        CollectPositions.Add(collectAreaTransform.position);

        MovementSpeed = 2.5f;
        CollectionSpeed = 70f;
        CarryCapacity = 100f;
        base.Start();
    }

}
