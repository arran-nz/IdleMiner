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
        MoveToCollect,
        Collect,
        MoveToDeposit,
        Deposit,
        ReceiveOrders
    }


    protected virtual void Awake()
    {
        carryValueText = gameObject.GetComponentInChildren<TextMesh>();
        workerSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        myArea = gameObject.GetComponentInParent<WorkingAreaBase>();

        UpdateCarryAmountText(0);
        ChangeState(WorkerStates.ReceiveOrders);
    }

    private void Update () {

        switch (CurrentState)
        {
            // Move to the collect position
            case WorkerStates.MoveToCollect:
                MoveToCollect(WorkerStates.Collect);
                break;

            // Collect Infinte amount unless specified on override
            case WorkerStates.Collect:
                Collect(WorkerStates.MoveToDeposit, (x) => { return x; });
                break;

            // Move to the deposit container position
            case WorkerStates.MoveToDeposit:
                MoveToContainer(WorkerStates.Deposit);
                break;

            // Deposit carry amount into container
            case WorkerStates.Deposit:
                Deposit(WorkerStates.ReceiveOrders);
                break;

            // Wait for orders from manager if present
            case WorkerStates.ReceiveOrders:
                ReceiveOrders(WorkerStates.MoveToCollect);
                break;

            default:
                break;
        }

    }

    protected virtual void ReceiveOrders(WorkerStates nextDesiredState)
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

    protected virtual void Collect(WorkerStates nextDesiredState, System.Func<float, float> collectionMethod)
    {
        // Desired amount to collect each frame
        float desiredCollectionAmount =  Time.deltaTime * CollectionSpeed;

        if ((CurrentCarryAmount + desiredCollectionAmount  <= CarryCapacity))
        {
            float amountCollected = collectionMethod(desiredCollectionAmount);

            AddCarryAmount(amountCollected);

        }
        else
        {
            float remainderLeft = (CarryCapacity - CurrentCarryAmount);
            float amountCollected = collectionMethod(remainderLeft);

            AddCarryAmount(amountCollected);


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
        MoveToLocation(myArea.DepositContainerPosition, WorkerStates.Deposit);
    }

    protected void MoveToLocation(Vector2 position, WorkerStates nextDesiredState)
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

    protected void AddCarryAmount(float amount)
    {
        CurrentCarryAmount += amount;
        UpdateCarryAmountText(CurrentCarryAmount);
    }

    protected void Deposit(WorkerStates nextDesiredState)
    {
        myArea.DepositContainer.AddToContainer(CurrentCarryAmount);
        CurrentCarryAmount = 0;
        UpdateCarryAmountText(CurrentCarryAmount);
        ChangeState(nextDesiredState);
    }

    private void UpdateCarryAmountText(float newAmount)
    {
        carryValueText.text = StringFormatHelper.GetCurrencyString(newAmount);
    }

    private void ChangeState(WorkerStates newState)
    {
        CurrentState = newState;
    }

}
