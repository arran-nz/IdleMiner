using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBase : MonoBehaviour {

    private TextMesh carryValueText;

    protected SpriteRenderer workerSprite;
    protected WorkingAreaBase myArea;



    protected float movementSpeed
    {
        get
        {
            return myArea.MovementSpeed;
        }
    }
    protected float collectionSpeed
    {
        get
        {
            return myArea.CollectionSpeed;
        }
    }
    protected float carryCapacity
    {
        get
        {
            return myArea.CarryCapacity;
        }
    }

    private float currentCarryAmount;


    protected WorkerStates CurrentState { get; private set; }
    protected enum WorkerStates
    {
        moveToCollect,
        collect,
        moveToDropOff,
        dropOff,
        waitForOrders
    }


    protected virtual void Awake()
    {
        carryValueText = gameObject.GetComponentInChildren<TextMesh>();
        workerSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        myArea = gameObject.GetComponentInParent<WorkingAreaBase>();

        UpdateText(0);
        ChangeState(WorkerStates.waitForOrders);
    }

    // Update is called once per frame
    private void Update () {

        switch (CurrentState)
        {
            case WorkerStates.moveToCollect:
                MoveToCollect(WorkerStates.collect);
                break;
            case WorkerStates.collect:
                Collect(WorkerStates.moveToDropOff, CollectionUpdate);
                break;
            case WorkerStates.moveToDropOff:
                MoveToContainer(WorkerStates.dropOff);
                break;
            case WorkerStates.dropOff:
                EmptyLoad(WorkerStates.waitForOrders);
                break;
            case WorkerStates.waitForOrders:
                WaitForWork(WorkerStates.moveToCollect);
                break;
            default:
                break;
        }

    }

    protected virtual void CollectionUpdate(float amount)
    {
        currentCarryAmount += amount;
        UpdateText(currentCarryAmount);
    }

    protected virtual void MoveToCollect(WorkerStates nextDesiredState)
    {
        MoveToLocation(myArea.CollectPosition, nextDesiredState);
    }

    protected virtual void MoveToContainer(WorkerStates nextDesiredState)
    {
        MoveToLocation(myArea.ContainerPosition, WorkerStates.dropOff);
    }

    protected virtual void WaitForWork(WorkerStates nextDesiredState)
    {
        if (myArea.ManangerPresent)
        {
            ChangeState(nextDesiredState);
        }
        else
        {
            //  WAIT FOR USER TO TAP SCREEN
        }
    }

    protected virtual void EmptyLoad(WorkerStates nextDesiredState)
    {
        myArea.AddToContainer(currentCarryAmount);
        currentCarryAmount = 0;
        UpdateText(currentCarryAmount);
        ChangeState(nextDesiredState);
    }


    protected virtual void Collect(WorkerStates nextDesiredState, System.Action<float> collectionCallback)
    {
        float amountToCollect =  Time.deltaTime * collectionSpeed;
        // Collect amount from Resource Amount in The Working Area every frame
        if ((currentCarryAmount + amountToCollect  <= carryCapacity) && myArea.ResourceAmount > 0)
        {
            myArea.CollectResources(amountToCollect, collectionCallback);
        }
        else
        {
            // If worker has remaining carryCapactiy, fill it up with remaing resources - Finished collecting
            if (currentCarryAmount < carryCapacity)
            {
                float remainderLeft = (carryCapacity - currentCarryAmount);
                myArea.CollectResources(remainderLeft, collectionCallback);

            }

            // Change to the next state
            ChangeState(nextDesiredState);

        }

    }

    protected virtual void MoveToLocation(Vector2 position, WorkerStates nextDesiredState)
    {
        if ((Vector2)transform.position != position)
        {
            transform.position = Vector2.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime);
        }
        else
        {
            ChangeState(nextDesiredState);
        }
    }

    protected void UpdateText(float newAmount)
    {
        carryValueText.text = StringFormatHelper.GetCurrencyString(newAmount);
    }

    protected void ChangeState(WorkerStates newState)
    {
        CurrentState = newState;
    }

}
