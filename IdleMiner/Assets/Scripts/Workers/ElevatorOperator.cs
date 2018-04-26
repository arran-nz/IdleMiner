using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorOperator : WorkerBase {

    private Mine[] mines;
    private GameObject mineContainer;

    int selectedMine = 0;

    private int CycleMineIndex(int desiredIndex)
    {
        if (desiredIndex < mines.Length)
        {
            return desiredIndex;
        }
        else
        {
            return 0;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        mineContainer = GameObject.FindGameObjectWithTag("MineContainer");
        mines = mineContainer.gameObject.GetComponentsInChildren<Mine>();
    }

    private void UpdateMineList()
    {
        Array.Clear(mines, 0, mines.Length);
        mines = mineContainer.gameObject.GetComponentsInChildren<Mine>();
    }

    protected override void ReceiveOrders(WorkerStates nextDesiredState)
    {
        // Check if MineList has any value before moving to the next state
        bool foundValue = false;
        for (int i = 0; i < mines.Length; i++)
        {
            if (mines[i].DepositContainer.HasValue)
            {
                foundValue = true;
                i = mines.Length;
            }
        }

        // Start at the top mine
        selectedMine = 0;

        // If found value, procede into the base state
        if (foundValue)
        {
            base.ReceiveOrders(nextDesiredState);
        }
    }

    protected override void MoveToCollect(WorkerStates nextDesiredState)
    {
        Vector2 shaftPos = new Vector2(myArea.transform.position.x, mines[selectedMine].transform.position.y);
        MoveToLocation(shaftPos, nextDesiredState);
    }

    protected override void Collect(WorkerStates nextDesiredState, Func<decimal, decimal> collectionMethod)
    {
        // Collect from the current Mine's deposit container
        Container currentMineContainer = mines[selectedMine].DepositContainer;
        collectionMethod = currentMineContainer.CollectFromContainer;

        if(currentMineContainer.HasValue)
        {
            // If CarryAmount has reached capacity it will progress to the next state
            base.Collect(nextDesiredState, collectionMethod);
        }
        else
        {
            int newIndex = CycleMineIndex(selectedMine + 1);

            if(newIndex != 0)
            {
                // Collect more
                selectedMine = newIndex;
                ChangeState(WorkerStates.MoveToCollect);                
            }
            else
            {
                // or else move on to the next state ( EG. Moving to deposit)
                ChangeState(nextDesiredState);

            }

        }
    }





}
