using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingArea : MonoBehaviour {


    [SerializeField]
    private TextMesh containerAmountText;

    [SerializeField]
    private Transform containerTrasform;
    [SerializeField]
    private Transform collectAreaTransform;


    public Vector2 DropOffPosition
    {
        get
        {
            return containerTrasform.position;
        }
    }
    public Vector2 CollectPosition
    {
        get
        {
            return collectAreaTransform.position;
        }
    }


    public bool ManangerPresent = true;

    public float ContainerAmount { get; private set; }
    public float MovementSpeed { get; private set; }
    public float CollectionSpeed { get; private set; }
    public float CarryCapacity { get; private set; } 

    private void Start()
    {
        MovementSpeed = 1f;
        CollectionSpeed = 1f;
        CarryCapacity = 2.25f;
    }

    public void DropOff(float amountToAdd)
    {
        ContainerAmount += amountToAdd;
        UpdateText(ContainerAmount);
    }

    private void UpdateText(float newAmount)
    {
        containerAmountText.text = StringFormatHelper.GetCurrencyString(newAmount);
    }

}
