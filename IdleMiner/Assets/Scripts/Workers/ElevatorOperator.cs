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
        myArea.ResourceAmount = mines[newIndex].ContainerAmount;
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

    protected override void EmptyLoad(WorkerStates nextDesiredState)
    {
        ChangeSelectedMine(0);

        base.EmptyLoad(nextDesiredState);
    }

    protected override void MoveToCollect(WorkerStates nextDesiredState)
    {        
        Vector2 shaftPos = new Vector2(myArea.transform.position.x, myArea.CollectPositions[currentMineIndex].y);
        MoveToLocation(shaftPos, nextDesiredState);
    }

    protected override void Collect(WorkerStates nextDesiredState, Func<WorkerStates,WorkerStates> finishedCollecting)
    {
        base.Collect(nextDesiredState, selectMineToCollect);

    }

    WorkerStates selectMineToCollect(WorkerStates nextDesiredState)
    {
        if(CurrentCarryAmount < CarryCapacity && mines[currentMineIndex].ResourceAmount > 0)
        {
            if (currentMineIndex < mines.Length - 1)
            {
                ChangeSelectedMine(currentMineIndex+1);
            }
            else
            {
                ChangeSelectedMine(0);
            }

            return WorkerStates.moveToCollect;
        }


        return nextDesiredState;

    }


    protected override void CollectionUpdate(float amount)
    {
        mines[currentMineIndex].RemoveFromContainer(amount);
        base.CollectionUpdate(amount);
    }





}
