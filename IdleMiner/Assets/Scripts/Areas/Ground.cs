using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : WorkingAreaBase {

    [SerializeField]
    private Transform collectAreaTransform;

    protected override void Start()
    {
        CollectPosition = collectAreaTransform.position;
        MovementSpeed = 0.5f;
        CollectionSpeed = 40;
        CarryCapacity = 750;

        base.Start();
    }
}
