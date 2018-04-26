using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorOperator : WorkerBase {

    private Mine[] mines;
    private GameObject mineContainer;

    int currentMineIndex = 0;

    private void ChangeSelectedMine(int newIndex)
    {
        currentMineIndex = newIndex;
    }

    protected override void Awake()
    {
        base.Awake();
        mineContainer = GameObject.FindGameObjectWithTag("MineContainer");
        mines = mineContainer.gameObject.GetComponentsInChildren<Mine>();
        foreach (Mine mine in mines)
        {
            myArea.CollectPositions.Add(mine.transform.position);
        }

        ChangeSelectedMine(0);

    }

    protected override void ReceiveOrders(WorkerStates nextDesiredState)
    {
        ChangeSelectedMine(0);
        base.ReceiveOrders(nextDesiredState);
    }

    protected override void MoveToCollect(WorkerStates nextDesiredState)
    {        
        Vector2 shaftPos = new Vector2(myArea.transform.position.x, myArea.CollectPositions[currentMineIndex].y);
        MoveToLocation(shaftPos, nextDesiredState);
    }

    protected override void Collect(WorkerStates nextDesiredState, Func<float, float> collectionMethod)
    {
        // Collect from the current Mine's deposit container
        base.Collect(nextDesiredState, mines[currentMineIndex].DepositContainer.CollectFromContainer);
    }





}
