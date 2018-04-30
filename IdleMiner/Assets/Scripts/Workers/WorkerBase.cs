using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Worker Base is the foundation for all workers
/// </summary>
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
    
    // Worker States for the state machine
    protected enum WorkerStates
    {
        MoveToCollect,
        Collect,
        MoveToDeposit,
        Deposit,
        ReceiveOrders
    }

    #region Properties

    protected bool InstantDeposit
    {
        get
        {
            return MyArea.InstantDeposit;
        }
    }
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

    #endregion



    protected virtual void Awake()
    {
        carryValueText = gameObject.GetComponentInChildren<TextMesh>();
        CarryValueTextRenderer = carryValueText.GetComponent<MeshRenderer>();
        WorkerSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        MyArea = gameObject.GetComponentInParent<WorkingAreaBase>();

        UpdateCarryAmountText(0);
        ChangeState(WorkerStates.ReceiveOrders);
    }

    /// <summary>
    /// Called Every Frame
    /// </summary>
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
            //  TODO: Implement touch feature to progress worker on the next state.
        }
    }

    /// <summary>
    /// Collect Resouces as defined in the Collection Method
    /// </summary>
    /// <param name="nextDesiredState"></param>
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

    /// <summary>
    /// Move to the Collection Postion
    /// </summary>
    /// <param name="nextDesiredState"></param>
    protected virtual void MoveToCollect(WorkerStates nextDesiredState)
    {
        MoveToLocation(MyArea.CollectPosition, nextDesiredState);
    }

    /// <summary>
    /// Move to Deposit Position
    /// </summary>
    /// <param name="nextDesiredState"></param>
    protected virtual void MoveToDeposit(WorkerStates nextDesiredState)
    {
        MoveToLocation(MyArea.DepositPosition, WorkerStates.Deposit);
    }

    /// <summary>
    /// Deposits the current load as the same rate as collection, unless instantDeposit is defined;
    /// </summary>
    /// <param name="nextDesiredState"></param>
    protected virtual void Deposit(WorkerStates nextDesiredState)
    {
        decimal desiredDepositAmount = (decimal)Time.deltaTime * CollectionSpeed;
        if (CurrentCarryAmount > desiredDepositAmount && !InstantDeposit)
        {
            DepositAction(desiredDepositAmount);
            RemoveCarryAmount(desiredDepositAmount);
        }
        else
        {
            DepositAction(CurrentCarryAmount);

            CurrentCarryAmount = 0;
            UpdateCarryAmountText(CurrentCarryAmount);

            ChangeState(nextDesiredState);
        }
    }

    /// <summary>
    /// Move to a location, when the worker has reach this It will progress states
    /// </summary>
    /// <param name="position"></param>
    /// <param name="nextDesiredState"></param>
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

    /// <summary>
    /// Add Carry Amount
    /// </summary>
    /// <param name="amount"></param>
    private void AddCarryAmount(decimal amount)
    {
        CurrentCarryAmount += amount;
        UpdateCarryAmountText(CurrentCarryAmount);
    }

    /// <summary>
    /// Remove Carry Amount
    /// </summary>
    /// <param name="amount"></param>
    private void RemoveCarryAmount(decimal amount)
    {
        CurrentCarryAmount -= amount;
        UpdateCarryAmountText(CurrentCarryAmount);
    }

    /// <summary>
    /// Update Carry Amount Text
    /// </summary>
    /// <param name="newAmount"></param>
    private void UpdateCarryAmountText(decimal newAmount)
    {
        carryValueText.text = StringFormatter.GetCurrencyString(newAmount);
    }

    /// <summary>
    /// Change State
    /// </summary>
    /// <param name="newState"></param>
    protected void ChangeState(WorkerStates newState)
    {
        CurrentState = newState;
    }

}
