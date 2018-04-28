using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : WorkingAreaBase {

    [SerializeField]
    private Transform collectAreaTransform;

    protected override void Configure()
    {
        CollectPosition = collectAreaTransform.position;

        WorkingAreaName = "Mine Shaft";
        CanAddWorkers = true;

        MovementSpeed.DisplayName = "Rolling Speed";
        MovementSpeed.Value = 0.5m;

        CollectionSpeed.DisplayName = "Mining Speed";
        CollectionSpeed.Value = 20;

        CarryCapacity.DisplayName = "Load per Miner";
        CarryCapacity.Value = 50;

        Workers.DisplayName = "Miners";
        Workers.Value = 1;

        // Every 10th upgrade, add a new worker
        Workers.UpgradeMethod = (x) => { return (AreaLevel % 10 == 0) ? 1 : 0; };
    }

}
