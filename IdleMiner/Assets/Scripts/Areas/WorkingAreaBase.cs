using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorkingAreaBase : MonoBehaviour {


    [SerializeField]
    private TextMesh containerAmountText;
    [SerializeField]
    private Transform containerTransform;

    public List<Vector2> CollectPositions = new List<Vector2>();

    public Vector2 ContainerPosition
    {
        get
        {
            return containerTransform.position;
        }
    }


    public bool ManangerPresent { get; private set; }
    public float ContainerAmount { get; private set; }


    public float MovementSpeed { get; protected set; }
    public float CollectionSpeed { get; protected set; }
    public float CarryCapacity { get; protected set; }

    public float ResourceAmount { get; set; }

    protected virtual void Start()
    {
        ManangerPresent = true;
    }

    public void AddToContainer(float amountToAdd)
    {
        ContainerAmount += amountToAdd;
        UpdateText(ContainerAmount);
    }

    public void RemoveFromContainer(float amountToRemove)
    {
        ContainerAmount -= amountToRemove;
        UpdateText(ContainerAmount);
    }


    public void CollectResources(float amountToCollect, System.Action<float> collectionUpdate)
    {
        if (ResourceAmount > 0)
        {
            if (ResourceAmount - amountToCollect > 0)
            {
                ResourceAmount -= amountToCollect;
                collectionUpdate.Invoke(amountToCollect);
            }
            else
            {
                float amountCollected = ResourceAmount;
                ResourceAmount = 0;
                collectionUpdate.Invoke(amountCollected);
            }
        }
    }

    private void UpdateText(float newAmount)
    {
        containerAmountText.text = StringFormatHelper.GetCurrencyString(newAmount);
    }

}
