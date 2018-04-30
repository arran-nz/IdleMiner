using UnityEngine;

/// <summary>
/// Elevator Area, used to extract resources from the mines
/// </summary>
public class Elevator : WorkingAreaBase {

    protected override void Configure()
    {
        DepositPosition = DepositContainer.transform.position;

        WorkingAreaName = "Elevator";
        CanAddWorkers = false;

        MovementDisplay = "Flying Speed";
        MovementStart = 0.6m;
        MovementUpgrade = 0.045m;

        CollectionDisplay = "Collection Speed";
        CollectionStart = 50m;
        CollectionUpgrade = 0.54m;

        CapacityDisplay = "Carry Amount";
        CapacityStart = 80m;
        CapacityUpgrade = 0.48m;

        WorkerDisplay = "Elevator Birds";

        AreaStartLevel = 1;

        AreaUpgradeStart = 60m;
        AreaUpgrade = 0.5m;

        base.Configure();
    }

}
