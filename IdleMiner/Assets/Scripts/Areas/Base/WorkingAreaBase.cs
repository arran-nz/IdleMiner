using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// The foundation for all working areas
/// </summary>
public abstract class WorkingAreaBase : MonoBehaviour {

    [HideInInspector]
    public Container DepositContainer;
    private SpriteButton upgradeButton;

    [SerializeField]
    private GameObject workerPrefab;

    #region Properties

    public Vector2 DepositPosition { get; protected set; }
    public Vector2 CollectPosition { get; protected set; }

    public bool ManangerPresent { get; private set; }
    public bool CanAddWorkers { get; protected set; }
    public bool InstantDeposit { get; protected set; }
    public int AreaLevel { get; private set; }

    public decimal CurrentUpgradeCost { get; private set; }
    protected Func<decimal> UpgradeCostMethod { get; set; }

    #endregion

    #region Attributes
    public AreaAttribute<decimal> MovementSpeed { get; protected set; }
    public AreaAttribute<decimal> CollectionSpeed { get; protected set; }
    public AreaAttribute<decimal> CarryCapacity { get; protected set; }
    public AreaAttribute<int> Workers { get; protected set; }

    public List<AreaAttribute<decimal>> AreaAttributes { get; protected set; }

    #endregion

    #region Default Configuration

    [HideInInspector]
    public string WorkingAreaName = "Working Area";

    protected string MovementDisplay = "Movement Speed";
    protected decimal MovementStart = 1;
    protected decimal MovementUpgrade = 0.10m;

    protected string CollectionDisplay = "Loading Speed";
    protected decimal CollectionStart = 10m;
    protected decimal CollectionUpgrade = 0.10m;

    protected string CapacityDisplay = "Load per Worker";
    protected decimal CapacityStart = 10m;
    protected decimal CapacityUpgrade = 0.10m;

    protected string WorkerDisplay = "Workers";
    private const int workerStart = 1;

    protected decimal AreaUpgradeStart = 10m;
    protected decimal AreaUpgrade = 0.1m;
    protected int AreaStartLevel = 1;

    protected int ExtraWorkerUpgradeLevel = 0;
    protected int MaxWorkers = 10;

    protected float WorkerSpawnRangeX = 0.2f;

    #endregion

    private void Start()
    {
        AreaLevel = AreaStartLevel;
        ManangerPresent = true;

        Configure();
    }

    private void Awake()
    {
        upgradeButton = GetComponentInChildren<SpriteButton>();
        DepositContainer = gameObject.GetComponentInChildren<Container>();
    }

    /// <summary>
    /// Configure the attributes and upgrades each worker will have.
    /// </summary>
    protected virtual void Configure()
    {
        AreaAttributes = new List<AreaAttribute<decimal>>();

        MovementSpeed = new AreaAttribute<decimal>
        {
            DisplayName = MovementDisplay,
            Value = MovementStart,
            StringFormatMethod = StringFormatter.GetMovementString,
            UpgradeMethod = (x) => { return x * MovementUpgrade; }
        };

        CollectionSpeed = new AreaAttribute<decimal>
        {
            DisplayName = CollectionDisplay,
            Value = CollectionStart,
            StringFormatMethod = StringFormatter.GetCurrencyPerSecondString,
            UpgradeMethod = (x) => { return x * CollectionUpgrade; }
        };

        CarryCapacity = new AreaAttribute<decimal>
        {
            DisplayName = CapacityDisplay,
            Value = CapacityStart,
            StringFormatMethod = StringFormatter.GetCapacityString,
            UpgradeMethod = (x) => { return x * CapacityUpgrade; }
        };

        Workers = new AreaAttribute<int>
        {
            DisplayName = WorkerDisplay,
            Value = workerStart,
            StringFormatMethod = StringFormatter.GetWorkersString,
            UpgradeMethod = (x) => {
                if (ExtraWorkerUpgradeLevel > 0 && CanAddWorkers && x < MaxWorkers)
                {
                    // Every X (ExtraWorkerUpgradeLevel) upgrades add 1 worker
                    return (AreaLevel % ExtraWorkerUpgradeLevel == 0) ? 1 : 0;
                }
                else
                {
                    return 0;
                }
            }
        };

        UpgradeCostMethod = () => 
        {
            return CurrentUpgradeCost * AreaUpgrade;
        };

        CurrentUpgradeCost = AreaUpgradeStart;


        AreaAttributes.Add(MovementSpeed);
        AreaAttributes.Add(CollectionSpeed);
        AreaAttributes.Add(CarryCapacity);


        // Cycle though and upgrade area if start level is GREATER than 1.
        if (AreaStartLevel > 1)
        {
            for (int i = 0; i < AreaStartLevel - 1; i++)
            {
                UpgradeArea();
            }
        }

    }

    /// <summary>
    /// Opens the upgrade panel
    /// </summary>
    public void OpenUpgradePanel()
    {
        GameController.Instance.UI.OpenUpgradePanel(this);
    }

    /// <summary>
    /// Upgrades this area, improving the worker's attributes
    /// </summary>
    public void UpgradeArea()
    {
        foreach (AreaAttribute<decimal> item in AreaAttributes)
        {
            item.Value += item.GetUpgradeAmount();
        }

        int newWorkers = Workers.GetUpgradeAmount();
        CurrentUpgradeCost += UpgradeCostMethod();
        AddWorkers(newWorkers);

        AreaLevel++;

        upgradeButton.SetText(AreaLevel.ToString());
    }

    /// <summary>
    /// Adds more working to this area.
    /// </summary>
    /// <param name="amount">Amount of new workers</param>
    protected void AddWorkers(int amount)
    {
        if(amount >= 1)
        {
            Workers.Value += amount;

            for (int i = 0; i < amount; i++)
            {
                Vector2 randomOffset = new Vector3(
                    UnityEngine.Random.Range(-WorkerSpawnRangeX, WorkerSpawnRangeX),
                    0);

                Instantiate(
                    workerPrefab,
                    DepositPosition + randomOffset,
                    new Quaternion(0,0,0,0),
                    this.transform);
            }
        }
    }



}
