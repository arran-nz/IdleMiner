using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays Area Attributes
/// </summary>
public class AreaAttributeDisplay : MonoBehaviour {

    [SerializeField]
    private Text statNameDisplay;
    [SerializeField]
    private Text currentValueDisplay;
    [SerializeField]
    private Text additionalValueDisplay;

    /// <summary>
    /// Updates the text fields and position for the attribute display
    /// </summary>
    public void UpdateAttributeDisplay(string statName, string currentValue, string additionalValue, Vector3 position)
    {
        statNameDisplay.text = statName;
        currentValueDisplay.text = currentValue;
        additionalValueDisplay.text = "+ " + additionalValue;

        RectTransform rT = GetComponent<RectTransform>();
        rT.position = position;
    }
}
