using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : WorkingAreaBase {

    [SerializeField]
    private Transform collectAreaTransform;

    protected override void Start()
    {
        base.Start();

        CollectPosition = collectAreaTransform.position;

        WorkingAreaName = "Top Level";

        MovementSpeed.DisplayName = "Movement Speed";
        MovementSpeed.Value = 0.5m;

        CollectionSpeed.DisplayName = "Loading Speed";
        CollectionSpeed.Value = 40;

        CarryCapacity.DisplayName = "Load per Blob";
        CarryCapacity.Value = 750;

        Workers.DisplayName = "Transporter Blobs";
        Workers.Value = 1;

    }
}
