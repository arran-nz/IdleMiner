using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorkerBase : MonoBehaviour {

    private TextMesh carryValueText;

    protected SpriteRenderer workerSprite;
    protected WorkingAreaBase myArea;



    protected float MovementSpeed
    {
        get
        {
            return myArea.MovementSpeed;
        }
    }
    protected float CollectionSpeed
    {
        get
        {
            return myArea.CollectionSpeed;
        }
    }
    protected float CarryCapacity
    {
        get
        {
            return myArea.CarryCapacity;
        }
    }

    protected float CurrentCarryAmount { get; private set; }


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
                Collect(WorkerStates.moveToDropOff, null);
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

    /// <summary>
    /// Called Everyime a collection amount has been update
    /// </summary>
    /// <param name="amount"></param>
    protected virtual void CollectionUpdate(float amount)
    {
        CurrentCarryAmount += amount;
        UpdateText(CurrentCarryAmount);
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
        myArea.AddToContainer(CurrentCarryAmount);
        CurrentCarryAmount = 0;
        UpdateText(CurrentCarryAmount);
        ChangeState(nextDesiredState);
    }


    protected virtual void Collect(WorkerStates nextDesiredState, System.Func<WorkerStates, WorkerStates> finishedCollecting)
    {
        float amountToCollect =  Time.deltaTime * CollectionSpeed;
        // Collect amount from Resource Amount in The Working Area every frame
        if ((CurrentCarryAmount + amountToCollect  <= CarryCapacity) && myArea.ResourceAmount > 0)
        {
            myArea.CollectResources(amountToCollect, CollectionUpdate);
        }
        else
        {
            float remainderLeft = (CarryCapacity - CurrentCarryAmount);
            myArea.CollectResources(remainderLeft, CollectionUpdate);

            //Invote the Finished Collecting Callback if it's not NULL
            if(finishedCollecting != null)
            {
                // Let the callback dertermine what the next state is and return.
                WorkerStates callbackDesiredState = finishedCollecting.Invoke(nextDesiredState);
                ChangeState(callbackDesiredState);
                return;
            }


            // Change to the next state
            ChangeState(nextDesiredState);
        }

    }

    protected virtual void MoveToCollect(WorkerStates nextDesiredState)
    {
        MoveToLocation(myArea.CollectPositions[0], nextDesiredState);
    }

    protected virtual void MoveToContainer(WorkerStates nextDesiredState)
    {
        MoveToLocation(myArea.ContainerPosition, WorkerStates.dropOff);
    }

    protected virtual void MoveToLocation(Vector2 position, WorkerStates nextDesiredState)
    {
        if ((Vector2)transform.position != position)
        {
            transform.position = Vector2.MoveTowards(transform.position, position, MovementSpeed * Time.deltaTime);
        }
        else
        {
            ChangeState(nextDesiredState);
        }
    }

    private void UpdateText(float newAmount)
    {
        carryValueText.text = StringFormatHelper.GetCurrencyString(newAmount);
    }

    private void ChangeState(WorkerStates newState)
    {
        CurrentState = newState;
    }

}
