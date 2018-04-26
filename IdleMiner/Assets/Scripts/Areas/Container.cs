using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour {

    private TextMesh containerAmountText;

    public float ContainerAmount { get; private set; }

    private void Awake()
    {
        containerAmountText = gameObject.GetComponentInChildren<TextMesh>();
    }

    public void AddToContainer(float amountToAdd)
    {
        ContainerAmount += amountToAdd;
        UpdateContainerAmountText(ContainerAmount);
    }


    public virtual float CollectFromContainer(float amountToRemove)
    {
        if (ContainerAmount > 0)
        {
            if (ContainerAmount - amountToRemove > 0)
            {
                ContainerAmount -= amountToRemove;
                UpdateContainerAmountText(ContainerAmount);

                return amountToRemove;

            }
            else
            {
                float amountRemoved = ContainerAmount;
                ContainerAmount = 0;
                UpdateContainerAmountText(ContainerAmount);

                return amountRemoved;

            }
        }
        else
        {
            return 0;
        }
    }

    private void UpdateContainerAmountText(float newAmount)
    {
        containerAmountText.text = StringFormatHelper.GetCurrencyString(newAmount);
    }
}
