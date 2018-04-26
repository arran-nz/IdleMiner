using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour {

    private TextMesh containerAmountText;

    private decimal containerAmount;

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

    public void AddToContainer(decimal amountToAdd)
    {
        containerAmount += amountToAdd;
        UpdateContainerAmountText(containerAmount);
    }


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
            return 0;
        }
    }

    private void UpdateContainerAmountText(decimal newAmount)
    {
        containerAmountText.text = StringFormatHelper.GetCurrencyString(newAmount);
    }
}
