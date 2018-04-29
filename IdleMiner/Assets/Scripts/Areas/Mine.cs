using UnityEngine;

public class Mine : WorkingAreaBase {

    [SerializeField]
    private Transform collectAreaTransform;

    /// <summary>
    /// The Individual's Index from top to bottom ( From 1 > )
    /// </summary>
    private int mineIndex;

    protected override void Configure()
    {
        CollectPosition = collectAreaTransform.position;
        DepositPosition = DepositContainer.transform.position;

        WorkingAreaName = "Mine Shaft";
        CanAddWorkers = true;

        decimal startDampener = 0.75m;

        MovementDisplay = "Rolling Speed";
        MovementStart = (mineIndex * 0.5m) * startDampener;

        CollectionDisplay = "Mining Speed";
        CollectionStart = (mineIndex * 20) * startDampener;

        LoadDisplay = "Load per Miner";
        LoadStart = (mineIndex * 50) * startDampener;

        WorkerDisplay = "Miners";
        ExtraWorkerUpgradeLevel = 10;

        AreaStartLevel = 1;

        base.Configure();
    }

    protected override void ExtraConfigCheck()
    {
        MovementSpeed.UpgradeMethod = (x) => { return mineIndex * (x * 0.04m); };
        CollectionSpeed.UpgradeMethod = (x) => { return mineIndex * (x * 0.115m); };
        CarryCapacity.UpgradeMethod = (x) => { return mineIndex * (x * 0.15m); };
        UpgradeCostMethod = (x) => { return mineIndex * (x * 38); };

        base.ExtraConfigCheck();
    }

    public void SetMineIndex(int x)
    {
        mineIndex = x;
    }

}
