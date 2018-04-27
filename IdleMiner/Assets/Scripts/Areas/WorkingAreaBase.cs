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

    public AreaStat MovementSpeed ;
    public AreaStat CollectionSpeed;
    public AreaStat CarryCapacity;
    public AreaStat Workers;

    public List<AreaStat> AreaStats = new List<AreaStat>();

    public string WorkingAreaName { get; protected set; }
    public int AreaLevel { get; private set; }
    public decimal UpgradeCost { get; private set; }

    [SerializeField]
    private UpgradePanel upgradePanel;

    protected virtual void Start()
    {
        WorkingAreaName = "Working Area";

        MovementSpeed = new AreaStat
        {
            DisplayName = "Movement Speed",
            StringFormatMethod = StringFormatHelper.GetMovementString
        };

        CollectionSpeed = new AreaStat
        {
            DisplayName = "Collection Speed",
            StringFormatMethod = StringFormatHelper.GetCurrencyPerSecondString
        };

        CarryCapacity = new AreaStat {
            DisplayName = "Carry Capacity",
            StringFormatMethod = StringFormatHelper.GetMovementString
        };

        Workers = new AreaStat {
            DisplayName = "Workers",
            StringFormatMethod = StringFormatHelper.GetWorkersString
        };

        AreaStats.Add(MovementSpeed);
        AreaStats.Add(CollectionSpeed);
        AreaStats.Add(CarryCapacity);
        AreaStats.Add(Workers);

        ManangerPresent = true;
        AreaLevel = 1;
    }

    private void Awake()
    {
        DepositContainer = gameObject.GetComponentInChildren<Container>();
    }

    public void OpenUpgradePanel()
    {
        upgradePanel.Initialize(this);
        upgradePanel.gameObject.SetActive(true);
        Debug.Log("Open Upgrade Panel");
    }

    public void UpgradeArea()
    {
        Debug.Log("Upgrade Area");
        AreaLevel++;
    }

}

public class AreaStat
{
    public string DisplayName;
    public System.Func<decimal, string> StringFormatMethod;

    public decimal Value;

    public string GetDisplayString()
    {
        return StringFormatMethod.Invoke(Value);
    }
}
