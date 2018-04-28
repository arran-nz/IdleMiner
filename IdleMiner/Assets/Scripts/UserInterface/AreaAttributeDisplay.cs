using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaAttributeDisplay : MonoBehaviour {

    [SerializeField]
    private Text statNameDisplay;
    [SerializeField]
    private Text currentValueDisplay;
    [SerializeField]
    private Text additionalValueDisplay;

    public void UpdateStat(string statName, string currentValue, string additionalValue, Vector3 position)
    {
        statNameDisplay.text = statName;
        currentValueDisplay.text = currentValue;
        additionalValueDisplay.text = "+ " + additionalValue;

        RectTransform rT = GetComponent<RectTransform>();
        rT.position = position;
    }
}
