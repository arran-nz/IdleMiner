using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays area attributes and gives user an option to purchase upgrades for areas
/// </summary>
public class UpgradePanel : MonoBehaviour {

    [SerializeField]
    private Transform areaStatsSpawnParent;

    [SerializeField]
    private GameObject areaStatPrefab;

    [SerializeField]
    private Text headerDisplay;

    [SerializeField]
    private Text upgradeCostDisplay;

    private List<AreaAttributeDisplay> areaAttributeDisplays = new List<AreaAttributeDisplay>();
    private WorkingAreaBase workingArea;

    /// <summary>
    /// If the user can close the upgrade panel
    /// </summary>
    private bool CanClosePanel { get; set; }

    #region Configuration

    private const float STAT_SPACING = 20f;

    #endregion

    public void Initialize(WorkingAreaBase workingArea)
    {
        CanClosePanel = false;
        StartCoroutine(AllowClosePanel(0.2f));

        this.workingArea = workingArea;
        PopulatePanel();
    }

    /// <summary>
    /// To prevent accidently closing
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    private IEnumerator AllowClosePanel(float t)
    {
        yield return new WaitForSeconds(t);
        CanClosePanel = true;
    }

    /// <summary>
    /// Polpulate the panel with information from the calling WorkingAreaBase
    /// </summary>
    private void PopulatePanel()
    {
        // Set Header and Upgrade Cost
        headerDisplay.text = workingArea.WorkingAreaName + " Level " + workingArea.AreaLevel;
        upgradeCostDisplay.text = StringFormatter.GetCurrencyString(workingArea.CurrentUpgradeCost);

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
            currentAreaStat.UpdateAttributeDisplay(
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
            currentAreaStat.UpdateAttributeDisplay(
                currentAttribute.DisplayName,
                currentAttribute.GetDisplayString(),
                currentAttribute.StringFormatMethod(currentAttribute.GetUpgradeAmount()),
                spawnPos
                );

            areaAttributeDisplays.Add(currentAreaStat);

            visibleAttributeCount++;
        }
       
    }

    /// <summary>
    /// Calculates the next position for the Attribute display
    /// </summary>
    /// <returns>New Position</returns>
    private Vector3 GetAttributeSpawnPos(int count, Transform clone)
    {
        return new Vector3(
                clone.position.x,
                clone.position.y - (count * (STAT_SPACING + clone.GetComponent<RectTransform>().rect.height)));
    }

    #region Button Activated Methods

    /// <summary>
    /// Closes the Upgrade Panel
    /// </summary>
    public void ClosePanel()
    {
        if (CanClosePanel)
        {
            GameController.Instance.UI.CloseUpgradePanel();
        }
    }

    /// <summary>
    /// Check's if you can, and purchases the upgrade
    /// </summary>
    public void PurchaseUpgrade()
    {
        GameController.Instance.SpendCash(workingArea.CurrentUpgradeCost,
            () => {
                workingArea.UpgradeArea();
                PopulatePanel();

                GameController.Instance.UI.Notification.PopNotification(
                "Upgrade Successful!",
                NotificationMessage.NotifcationStyle.Info);

            });

    }

    #endregion
}
