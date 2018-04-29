using UnityEngine;

public class Elevator : WorkingAreaBase {

    protected override void Configure()
    {
        DepositPosition = DepositContainer.transform.position;

        WorkingAreaName = "Elevator";
        CanAddWorkers = false;

        MovementDisplay = "Flying Speed";
        MovementStart = 1m;
        MovementUpgrade = 0.01m;

        CollectionDisplay = "Collection Speed";
        CollectionStart = 50m;
        CollectionUpgrade = 0.1m;

        LoadDisplay = "Load per Bird";
        LoadStart = 200m;
        LoadUpgrade = 0.14m;

        WorkerDisplay = "Elevator Birds";

        AreaStartLevel = 1;

        AreaUpgradeCost = 32m;

        base.Configure();
    }

}
