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

    private List<GameObject> areaStatDisplayObjects = new List<GameObject>();

    private WorkingAreaBase workingArea;

    public void Initialize(WorkingAreaBase workingArea)
    {
        this.workingArea = workingArea;
        headerDisplay.text = workingArea.WorkingAreaName + " Level " + workingArea.AreaLevel;
        upgradeCostDisplay.text = StringFormatHelper.GetCurrencyString(workingArea.UpgradeCost);
        PopulateAreaStats();
    }

    private void PopulateAreaStats()
    {
        int count = 0;
        foreach (AreaStat stat in workingArea.AreaStats)
        {
            count++;
            GameObject clone = Instantiate(areaStatPrefab, areaStatsSpawnParent);
            areaStatDisplayObjects.Add(clone);

            Vector3 spawnPos = new Vector3(
                clone.transform.position.x,
                clone.transform.position.y - (count * (statSpacing + clone.GetComponent<RectTransform>().rect.height)));

            AreaStatDisplay currentAreaStat = clone.GetComponent<AreaStatDisplay>();
            currentAreaStat.UpdateStat(
                stat.DisplayName,
                stat.GetDisplayString(),
                StringFormatHelper.GetCurrencyString(0),
                spawnPos
                );

        }
    }


    private void UpdateInformation()
    {
        headerDisplay.text = workingArea.WorkingAreaName + " Level " + workingArea.AreaLevel;
        upgradeCostDisplay.text = StringFormatHelper.GetCurrencyString(workingArea.UpgradeCost);

    }

    #region Button Activated Methods

    public void ClosePanel()
    {
        foreach (GameObject stat in areaStatDisplayObjects)
        {
            Destroy(stat);
        }
        gameObject.SetActive(false);
    }

    public void PurchaseUpgrade()
    {
        workingArea.UpgradeArea();
        UpdateInformation();

    }

    #endregion
}
