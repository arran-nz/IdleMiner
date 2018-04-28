using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorkingAreaBase : MonoBehaviour {

    [HideInInspector]
    public Container DepositContainer;

    public Vector2 CollectPosition { get; protected set; }

    public Vector2 DepositContainerPosition
    {
        get
        {
            return DepositContainer.transform.position;
        }
    }


    public bool ManangerPresent { get; private set; }

    public AreaAttribute<decimal> MovementSpeed ;
    public AreaAttribute<decimal> CollectionSpeed;
    public AreaAttribute<decimal> CarryCapacity;
    public AreaAttribute<int> Workers;

    public List<AreaAttribute<decimal>> AreaAttributes = new List<AreaAttribute<decimal>>();

    public string WorkingAreaName { get; protected set; }
    public bool CanAddWorkers { get; protected set; }
    public int AreaLevel { get; private set; }
    public decimal UpgradeCost { get; private set; }

    [SerializeField]
    private GameObject workerPrefab;



    [SerializeField]
    private UpgradePanel upgradePanel;

    private void Start()
    {
        WorkingAreaName = "Working Area";

        MovementSpeed = new AreaAttribute<decimal>
        {
            DisplayName = "Movement Speed",
            StringFormatMethod = StringFormatHelper.GetMovementString,
            UpgradeMethod = (x) => { return x * 0.02m; }
        };

        CollectionSpeed = new AreaAttribute<decimal>
        {
            DisplayName = "Collection Speed",
            StringFormatMethod = StringFormatHelper.GetCurrencyPerSecondString,
            UpgradeMethod = (x) => { return x * 0.02m; }
        };

        CarryCapacity = new AreaAttribute<decimal> {
            DisplayName = "Carry Capacity",
            StringFormatMethod = StringFormatHelper.GetCapacityString,
            UpgradeMethod = (x) => { return x * 0.02m; }
        };

        Workers = new AreaAttribute<int> {
            DisplayName = "Workers",
            StringFormatMethod = StringFormatHelper.GetWorkersString,
            UpgradeMethod = (x) => { return 0; }
        };

        AreaAttributes.Add(MovementSpeed);
        AreaAttributes.Add(CollectionSpeed);
        AreaAttributes.Add(CarryCapacity);

        AreaLevel = 1;
        ManangerPresent = true;

        Configure();

    }

    protected virtual void Configure()
    {
        // Overide Custom Settings here if needed
    }

    private void Awake()
    {
        DepositContainer = gameObject.GetComponentInChildren<Container>();
    }

    public void OpenUpgradePanel()
    {
        upgradePanel.Initialize(this);
        upgradePanel.gameObject.SetActive(true);
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
    }

    protected void AddWorkers(int amount)
    {
        if(amount >= 1)
        {
            Workers.Value += amount;

            for (int i = 0; i < amount; i++)
            {
                Instantiate(workerPrefab, this.transform);
            }
        }
    }

}
