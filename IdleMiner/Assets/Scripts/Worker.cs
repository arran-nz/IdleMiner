using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour {

    protected SpriteRenderer workerSprite;
    private TextMesh carryValueText;
    private WorkingArea myArea;

    private float movementSpeed
    {
        get
        {
            return myArea.MovementSpeed;
        }
    }
    private float collectionSpeed
    {
        get
        {
            return myArea.CollectionSpeed;
        }
    }
    private float carryCapacity
    {
        get
        {
            return myArea.CarryCapacity;
        }
    }


    private WorkerStates currentState;
    protected enum WorkerStates
    {
        moveToCollect,
        collect,
        moveToContainer,
        dropOff,
        waitForOrders

    }

    private float currentCarryAmount;

    protected virtual void Awake()
    {
        carryValueText = gameObject.GetComponentInChildren<TextMesh>();
        workerSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        myArea = gameObject.GetComponentInParent<WorkingArea>();

        UpdateText(0);
        currentState = WorkerStates.waitForOrders;
    }

    // Update is called once per frame
    void Update () {

        switch (currentState)
        {
            case WorkerStates.moveToCollect:
                MoveToLocation(myArea.CollectPosition, WorkerStates.collect);
                workerSprite.flipX = false;
                break;
            case WorkerStates.collect:
                Collect(WorkerStates.moveToContainer);
                break;
            case WorkerStates.moveToContainer:
                MoveToLocation(myArea.DropOffPosition, WorkerStates.dropOff);
                workerSprite.flipX = true;
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

    protected virtual void WaitForWork(WorkerStates nextDesiredState)
    {
        if (myArea.ManangerPresent)
        {
            currentState = nextDesiredState;
        }
        else
        {
            //  WAIT FOR USER TO TAP SCREEN
        }
    }

    protected virtual void EmptyLoad(WorkerStates nextDesiredState)
    {
        myArea.DropOff(currentCarryAmount);
        currentCarryAmount = 0;
        UpdateText(currentCarryAmount);
        currentState = nextDesiredState;
    }


    protected virtual void Collect(WorkerStates nextDesiredState)
    {
        float amountToAdd = (1 * Time.deltaTime) * collectionSpeed;
        if (currentCarryAmount + amountToAdd  <= carryCapacity)
        {
            currentCarryAmount += amountToAdd;
            UpdateText(currentCarryAmount);
        }
        else
        {
            currentCarryAmount = carryCapacity;
            UpdateText(currentCarryAmount);

            currentState = nextDesiredState;
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
            currentState = nextDesiredState;
        }
    }

    void UpdateText(float newAmount)
    {
        carryValueText.text = StringFormatHelper.GetCurrencyString(newAmount);
    }

}
