using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container, used for storing value
/// </summary>
public class Container : MonoBehaviour {

    private TextMesh containerAmountText;

    private decimal containerAmount;

    /// <summary>
    /// Does the container hold any value
    /// </summary>
    public bool HasValue
    {
        get
        {
            return containerAmount > 0;
        }
    }

    private void Awake()
    {
        containerAmountText = gameObject.GetComponentInChildren<TextMesh>();
    }

    /// <summary>
    /// Add value to this container
    /// </summary>
    /// <param name="amountToAdd"></param>
    public void AddToContainer(decimal amountToAdd)
    {
        containerAmount += amountToAdd;
        UpdateContainerAmountText(containerAmount);
    }

    /// <summary>
    /// Remove value from container if the amount is there
    /// </summary>
    /// <param name="amountToRemove"></param>
    /// <returns></returns>
    public decimal CollectFromContainer(decimal amountToRemove)
    {
        if (containerAmount > 0)
        {
            if (containerAmount - amountToRemove > 0)
            {
                // Container Not Empty

                containerAmount -= amountToRemove;
                UpdateContainerAmountText(containerAmount);

                return amountToRemove;

            }
            else
            {
                // Container Empty

                decimal amountRemoved = containerAmount;
                containerAmount = 0;
                UpdateContainerAmountText(containerAmount);

                return amountRemoved;

            }
        }
        else
        {
            // Nothing to collect

            return 0;
        }
    }

    /// <summary>
    /// Update the container amount display text
    /// </summary>
    /// <param name="newAmount"></param>
    private void UpdateContainerAmountText(decimal newAmount)
    {
        containerAmountText.text = StringFormatter.GetCurrencyString(newAmount);
    }
}
