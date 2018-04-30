using UnityEngine;

/// <summary>
/// Ground Area, used to transfer resouces from the elevator shaft to market. (Selling them)
/// </summary>
public class Ground : WorkingAreaBase {

    [SerializeField]
    private Transform collectArea;

    [SerializeField]
    private Transform depositArea;

    protected override void Configure()
    {
        CollectPosition = collectArea.position;
        DepositPosition = depositArea.transform.position;

        WorkingAreaName = "Top Level";
        CanAddWorkers = true;

        MovementDisplay = "Movement Speed";
        MovementStart = 0.35m;
        MovementUpgrade = 0.08m;

        CollectionDisplay = "Loading Speed";
        CollectionStart = 75m;
        CollectionUpgrade = 0.32m;

        CapacityDisplay = "Load per Blob";
        CapacityStart = 150m;
        CapacityUpgrade = 0.22m;

        WorkerDisplay = "Transporter Blobs";
        ExtraWorkerUpgradeLevel = 4;
        MaxWorkers = 10;

        AreaUpgradeStart = 100m;
        AreaUpgrade = 0.45m;

        base.Configure();
    }
}
