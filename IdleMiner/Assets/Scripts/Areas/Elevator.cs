using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : WorkingAreaBase {

    protected override void Configure()
    {
        WorkingAreaName = "Elevator";
        CanAddWorkers = false;

        MovementSpeed.DisplayName = "Flying Speed";
        MovementSpeed.Value = 1;
        MovementSpeed.UpgradeMethod = (x) => { return x * 0.01m; };

        CollectionSpeed.Value = 50;

        CarryCapacity.Value = 200;

        Workers.DisplayName = "Elevator Birds";
        Workers.Value = 1;


    }

}
