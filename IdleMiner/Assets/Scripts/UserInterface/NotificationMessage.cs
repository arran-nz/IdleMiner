using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to display notifcations
/// </summary>
public class NotificationMessage : MonoBehaviour
{
    private Text textDisplay;
    private Image image;

    public Color Alert;
    public Color Info;

    public enum NotifcationStyle
    {
        Alert,
        Info
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        textDisplay = GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Display a custom Notification
    /// </summary>
    /// <param name="message"></param>
    /// <param name="style"></param>
    public void PopNotification(string message, NotifcationStyle style)
    {
        gameObject.SetActive(true);

        textDisplay.text = message;

        switch (style)
        {
            case NotifcationStyle.Alert:
                image.color = Alert;
                break;
            case NotifcationStyle.Info:
                image.color = Info;
                break;
        }

        StartCoroutine(HideNotification(1f));



    }

    /// <summary>
    /// Hides Notification in X seconds
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator HideNotification(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
