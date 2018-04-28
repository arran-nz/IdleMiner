using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : WorkingAreaBase {

    [SerializeField]
    private Transform collectAreaTransform;

    protected override void Configure()
    {
        CollectPosition = collectAreaTransform.position;

        WorkingAreaName = "Top Level";
        CanAddWorkers = true;

        MovementSpeed.DisplayName = "Movement Speed";
        MovementSpeed.Value = 0.2m;

        CollectionSpeed.DisplayName = "Loading Speed";
        CollectionSpeed.Value = 30;

        CarryCapacity.DisplayName = "Load per Blob";
        CarryCapacity.Value = 150;

        Workers.DisplayName = "Transporter Blobs";
        Workers.Value = 1;

        // Every 12th upgrade, add a new worker
        Workers.UpgradeMethod = (x) => { return (AreaLevel % 12 == 0) ? 1 : 0; };

    }
}
