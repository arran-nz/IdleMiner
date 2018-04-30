using UnityEngine;

/// <summary>
/// The Mine Area, where resources are generated infinitely.
/// </summary>
public class Mine : WorkingAreaBase {

    [SerializeField]
    private Transform collectAreaTransform;

    /// <summary>
    /// The Individual Minw Index from top to bottom ( From 1 > )
    /// </summary>
    private int mineIndex = 1;


    protected override void Configure()
    {
        CollectPosition = collectAreaTransform.position;
        DepositPosition = DepositContainer.transform.position;

        int UpgradeCostPower = MineManager.UPGRADE_COST_POWER;

        decimal indexAmount = (decimal)Mathf.Pow(UpgradeCostPower, mineIndex);

        WorkingAreaName = "Mine Shaft";

        CollectionDisplay = "Mining Speed";
        CollectionUpgrade = 0.10m;
        decimal mineOneCollectionStart = 42.5m;
        CollectionStart = indexAmount * (mineOneCollectionStart / UpgradeCostPower);

        CapacityDisplay = "Load per Miner";
        CapacityUpgrade = 0.10m;
        decimal mineOneCapacityStart = 61.2m;
        CapacityStart = indexAmount * (mineOneCapacityStart / UpgradeCostPower);

        decimal mineOneUpgradeStart = 100m;
        AreaUpgradeStart = (mineOneUpgradeStart * indexAmount) / UpgradeCostPower;
        AreaUpgrade = 0.06m;

        MovementDisplay = "Rolling Speed";
        MovementUpgrade = 0.02m;
        MovementStart = 0.4m;


        CanAddWorkers = true;
        InstantDeposit = true;
        WorkerDisplay = "Miners";
        ExtraWorkerUpgradeLevel = 12;
        MaxWorkers = 6;

        AreaStartLevel = 1;



        base.Configure();
    }

    public void SetMineIndex(int x)
    {
        mineIndex = x;
    }

}
