using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : WorkingAreaBase {

    [SerializeField]
    private Transform collectAreaTransform;

    protected override void Start()
    {
        base.Start();

        CollectPosition = collectAreaTransform.position;

        WorkingAreaName = "Mine Shaft";

        MovementSpeed.DisplayName = "Rolling Speed";
        MovementSpeed.Value = 0.5m;

        CollectionSpeed.DisplayName = "Mining Speed";
        CollectionSpeed.Value = 20;

        CarryCapacity.DisplayName = "Load per Miner";
        CarryCapacity.Value = 50;

        Workers.DisplayName = "Miners";
        Workers.Value = 1;
    }

}
