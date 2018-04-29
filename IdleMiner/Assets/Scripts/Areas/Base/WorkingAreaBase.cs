using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int AreaLevel { get; private set; }

    public System.Func<int, decimal> UpgradeCostMethod { get; protected set; }

    #endregion

    #region Attributes
    public AreaAttribute<decimal> MovementSpeed { get; protected set; }
    public AreaAttribute<decimal> CollectionSpeed { get; protected set; }
    public AreaAttribute<decimal> CarryCapacity { get; protected set; }
    public AreaAttribute<int> Workers { get; protected set; }

    public List<AreaAttribute<decimal>> AreaAttributes { get; protected set; }

    #endregion

    #region Default Configuration

    public string WorkingAreaName = "Working Area";

    protected string MovementDisplay = "Movement Speed";
    protected decimal MovementStart = 1;
    protected decimal MovementUpgrade = 0.10m;

    protected string CollectionDisplay = "Loading Speed";
    protected decimal CollectionStart = 100m;
    protected decimal CollectionUpgrade = 0.10m;

    protected string LoadDisplay = "Load per Worker";
    protected decimal LoadStart = 100m;
    protected decimal LoadUpgrade = 0.10m;

    protected string WorkerDisplay = "Workers";
    private const int workerStart = 1;

    protected decimal AreaUpgradeCost = 10m;
    protected int AreaStartLevel = 1;

    protected int ExtraWorkerUpgradeLevel = 0;

    protected float WorkerSpawnRangeX = 0.2f;

    #endregion

    private void Start()
    {
        AreaLevel = AreaStartLevel;
        ManangerPresent = true;

        Configure();
    }

    /// <summary>
    /// Configure the attributes and upgrades each worker and this area will have.
    /// </summary>
    protected virtual void Configure()
    {
        AreaAttributes = new List<AreaAttribute<decimal>>();

        MovementSpeed = new AreaAttribute<decimal>
        {
            DisplayName = MovementDisplay,
            Value = MovementStart,
            StringFormatMethod = StringFormatHelper.GetMovementString,
            UpgradeMethod = (x) => { return x * MovementUpgrade; }
        };

        CollectionSpeed = new AreaAttribute<decimal>
        {
            DisplayName = CollectionDisplay,
            Value = CollectionStart,
            StringFormatMethod = StringFormatHelper.GetCurrencyPerSecondString,
            UpgradeMethod = (x) => { return x * CollectionUpgrade; }
        };

        CarryCapacity = new AreaAttribute<decimal>
        {
            DisplayName = LoadDisplay,
            Value = LoadStart,
            StringFormatMethod = StringFormatHelper.GetCapacityString,
            UpgradeMethod = (x) => { return x * LoadUpgrade; }
        };

        Workers = new AreaAttribute<int>
        {
            DisplayName = WorkerDisplay,
            Value = workerStart,
            StringFormatMethod = StringFormatHelper.GetWorkersString,
            UpgradeMethod = (x) => {
                if (ExtraWorkerUpgradeLevel > 0 && CanAddWorkers)
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

        UpgradeCostMethod = (x) => { return AreaUpgradeCost * x; };


        AreaAttributes.Add(MovementSpeed);
        AreaAttributes.Add(CollectionSpeed);
        AreaAttributes.Add(CarryCapacity);

        ExtraConfigCheck();

    }

    protected virtual void ExtraConfigCheck()
    {
        if (workerStart > 1)
        {
            AddWorkers(workerStart - 1);
        }

        if (AreaStartLevel > 1)
        {
            for (int i = 0; i < AreaStartLevel - 1; i++)
            {
                UpgradeArea();
            }
        }
    }

    private void Awake()
    {
        upgradeButton = GetComponentInChildren<SpriteButton>();
        DepositContainer = gameObject.GetComponentInChildren<Container>();
    }

    public void OpenUpgradePanel()
    {
        GameController.Instance.UpgradePanel.Initialize(this);
        GameController.Instance.UpgradePanel.gameObject.SetActive(true);
    }

    public void UpgradeArea()
    {
        foreach (AreaAttribute<decimal> item in AreaAttributes)
        {
            item.Value += item.GetUpgradeAmount();
        }

        int newWorkers = Workers.GetUpgradeAmount();
        AddWorkers(newWorkers);

        AreaLevel++;

        upgradeButton.SetText(AreaLevel.ToString());
    }

    protected void AddWorkers(int amount)
    {
        if(amount >= 1)
        {
            Workers.Value += amount;

            for (int i = 0; i < amount; i++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-WorkerSpawnRangeX, WorkerSpawnRangeX),
                    0);

                Instantiate(
                    workerPrefab,
                    workerPrefab.transform.position + randomOffset,
                    new Quaternion(0,0,0,0),
                    this.transform);
            }
        }
    }



}
