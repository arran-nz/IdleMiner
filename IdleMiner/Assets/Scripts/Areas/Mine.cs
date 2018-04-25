using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : WorkingAreaBase {

    [SerializeField]
    private Transform collectAreaTransform;

    protected override void Start()
    {
        CollectPositions.Add(collectAreaTransform.position);

        MovementSpeed = 2.0f;
        CollectionSpeed = 50f;
        CarryCapacity = 100f;
        ResourceAmount = Mathf.Infinity;
        base.Start();
    }
}
