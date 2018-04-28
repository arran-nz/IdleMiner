using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour {

    [SerializeField]
    private Transform areaStatsSpawnParent;

    [SerializeField]
    private GameObject areaStatPrefab;

    [SerializeField]
    private Text headerDisplay;

    [SerializeField]
    private Text upgradeCostDisplay;

    private const float statSpacing = 20f;

    private List<AreaAttributeDisplay> areaAttributeDisplays = new List<AreaAttributeDisplay>();

    private WorkingAreaBase workingArea;

    public void Initialize(WorkingAreaBase workingArea)
    {
        this.workingArea = workingArea;
        PopulatePanel();
    }

    private Vector3 GetAttributeSpawnPos(int count, Transform clone)
    {
        return new Vector3(
                clone.position.x,
                clone.position.y - (count * (statSpacing + clone.GetComponent<RectTransform>().rect.height)));
    }

    private void PopulatePanel()
    {
        headerDisplay.text = workingArea.WorkingAreaName + " Level " + workingArea.AreaLevel;
        upgradeCostDisplay.text = StringFormatHelper.GetCurrencyString(workingArea.UpgradeCost);

        // Clear and destroy any existing attribute displays if present
        if (areaAttributeDisplays.Count > 0)
        {
            foreach (var item in areaAttributeDisplays)
            {
                Destroy(item.gameObject);
            }

            areaAttributeDisplays.Clear();
        }


        int visibleAttributeCount = 0;

        // If working area has the ability to add workers, display this
        if (workingArea.CanAddWorkers)
        {
            GameObject clone = Instantiate(areaStatPrefab, areaStatsSpawnParent);

            Vector3 spawnPos = GetAttributeSpawnPos(visibleAttributeCount, clone.transform);

            AreaAttributeDisplay currentAreaStat = clone.GetComponent<AreaAttributeDisplay>();
            currentAreaStat.UpdateStat(
                workingArea.Workers.DisplayName,
                workingArea.Workers.GetDisplayString(),
                workingArea.Workers.StringFormatMethod(workingArea.Workers.GetUpgradeAmount()),
                spawnPos
                );

            areaAttributeDisplays.Add(currentAreaStat);

            visibleAttributeCount = 1;
        }


        // Populate the <decimal> attribute displays with new data

        foreach (AreaAttribute<decimal> currentAttribute in workingArea.AreaAttributes)
        {
            GameObject clone = Instantiate(areaStatPrefab, areaStatsSpawnParent);

            Vector3 spawnPos = GetAttributeSpawnPos(visibleAttributeCount, clone.transform);

            AreaAttributeDisplay currentAreaStat = clone.GetComponent<AreaAttributeDisplay>();
            currentAreaStat.UpdateStat(
                currentAttribute.DisplayName,
                currentAttribute.GetDisplayString(),
                currentAttribute.StringFormatMethod(currentAttribute.GetUpgradeAmount()),
                spawnPos
                );

            areaAttributeDisplays.Add(currentAreaStat);

            visibleAttributeCount++;
        }

       
    }


    private void UpdateInformation()
    {
        PopulatePanel();
    }

    #region Button Activated Methods

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void PurchaseUpgrade()
    {
        workingArea.UpgradeArea();
        UpdateInformation();

    }

    #endregion
}
