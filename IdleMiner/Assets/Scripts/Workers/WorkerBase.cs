using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorkerBase : MonoBehaviour {

    private TextMesh carryValueText;
    protected MeshRenderer CarryValueTextRenderer;

    protected SpriteRenderer WorkerSprite;
    protected WorkingAreaBase MyArea;

    protected Func<decimal, decimal> CollectionMethod = (x) => {
        Debug.Log("Collection Method Not Set"); return 0; };

    protected Action<decimal> DepositAction = (x) => {
        Debug.Log("Deposit Action Not Set");
    };

    protected decimal MovementSpeed
    {
        get
        {
            return MyArea.MovementSpeed.Value;
        }
    }
    protected decimal CollectionSpeed
    {
        get
        {
            return MyArea.CollectionSpeed.Value;
        }
    }
    protected decimal CarryCapacity
    {
        get
        {
            return MyArea.CarryCapacity.Value;
        }
    }

    protected decimal CurrentCarryAmount { get; private set; }


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
        CarryValueTextRenderer = carryValueText.GetComponent<MeshRenderer>();
        WorkerSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        MyArea = gameObject.GetComponentInParent<WorkingAreaBase>();

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
                Collect(WorkerStates.MoveToDeposit);
                break;

            // Move to the deposit container position
            case WorkerStates.MoveToDeposit:
                MoveToDeposit(WorkerStates.Deposit);
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
        if (MyArea.ManangerPresent)
        {
            ChangeState(nextDesiredState);
        }
        else
        {
            //  WAIT FOR USER TO TAP SCREEN
        }
    }

    protected virtual void Collect(WorkerStates nextDesiredState)
    {
        // Desired amount to collect each frame
        decimal desiredCollectionAmount =  (decimal)Time.deltaTime * CollectionSpeed;

        if ((CurrentCarryAmount + desiredCollectionAmount  <= CarryCapacity))
        {
            decimal amountCollected = CollectionMethod(desiredCollectionAmount);

            AddCarryAmount(amountCollected);

        }
        else
        {
            decimal remainderLeft = (CarryCapacity - CurrentCarryAmount);
            decimal amountCollected = CollectionMethod(remainderLeft);

            AddCarryAmount(amountCollected);


            // Change to the next state once carry amount has reached capacity
            ChangeState(nextDesiredState);
        }

    }

    protected virtual void MoveToCollect(WorkerStates nextDesiredState)
    {
        MoveToLocation(MyArea.CollectPosition, nextDesiredState);
    }

    protected virtual void MoveToDeposit(WorkerStates nextDesiredState)
    {
        MoveToLocation(MyArea.DepositPosition, WorkerStates.Deposit);
    }

    protected void Deposit(WorkerStates nextDesiredState)
    {
        DepositAction(CurrentCarryAmount);
        CurrentCarryAmount = 0;

        UpdateCarryAmountText(CurrentCarryAmount);
        ChangeState(nextDesiredState);
    }

    protected void MoveToLocation(Vector2 position, WorkerStates nextDesiredState)
    {
        if ((Vector2)transform.position != position)
        {
            transform.position = Vector2.MoveTowards(transform.position, position, (float)MovementSpeed * Time.deltaTime);
        }
        else
        {
            ChangeState(nextDesiredState);
        }
    }

    protected void AddCarryAmount(decimal amount)
    {
        CurrentCarryAmount += amount;
        UpdateCarryAmountText(CurrentCarryAmount);
    }

    private void UpdateCarryAmountText(decimal newAmount)
    {
        carryValueText.text = StringFormatHelper.GetCurrencyString(newAmount);
    }

    protected void ChangeState(WorkerStates newState)
    {
        CurrentState = newState;
    }

}
