using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UserInterface Manager is used to manage all UI elements
/// </summary>
public class UserInterfaceManager : MonoBehaviour {

    [SerializeField]
    public NotificationMessage Notification;
    [SerializeField]
    public UpgradePanel UpgradePanel;
    [SerializeField]
    public Text CashDisplay;

    /// <summary>
    /// Used to block input for game items below a UI item
    /// </summary>
    public bool IsScreenCovered { get; private set; }

    /// <summary>
    /// Opens the upgrade panel
    /// </summary>
    /// <param name="workingArea"></param>
    public void OpenUpgradePanel(WorkingAreaBase workingArea)
    {
        UpgradePanel.gameObject.SetActive(true);
        UpgradePanel.Initialize(workingArea);

        IsScreenCovered = true;
    }

    /// <summary>
    /// Closes upgrade panel
    /// </summary>
    public void CloseUpgradePanel()
    {
        UpgradePanel.gameObject.SetActive(false);

        IsScreenCovered = false;
    }

}
