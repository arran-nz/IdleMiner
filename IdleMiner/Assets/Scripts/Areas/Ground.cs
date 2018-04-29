using UnityEngine;

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
        MovementStart = 0.55m;
        MovementUpgrade = 0.10m;

        CollectionDisplay = "Loading Speed";
        CollectionStart = 75m;
        CollectionUpgrade = 0.13m;

        LoadDisplay = "Load per Blob";
        LoadStart = 150m;
        LoadUpgrade = 0.15m;

        WorkerDisplay = "Transporter Blobs";
        ExtraWorkerUpgradeLevel = 8;

        AreaUpgradeCost = 30m;

        base.Configure();
    }
}
