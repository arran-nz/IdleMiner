using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElevatorOperator : WorkerBase {

    private GameObject mineContainer;
    private Mine[] mines;

    WorkerStates nextState;
    int currentMineIndex = 2;

    protected override void Awake()
    {
        mineContainer = GameObject.FindGameObjectWithTag("MineContainer");
        mines = mineContainer.gameObject.GetComponentsInChildren<Mine>();
        Debug.Log(mines.Length);

        base.Awake();
    }

    protected override void MoveToCollect(WorkerStates nextDesiredState)
    {
        nextState = nextDesiredState;


        myArea.ResourceAmount = mines[currentMineIndex].ContainerAmount;
        Vector2 shaftPos = new Vector2(myArea.transform.position.x, mines[currentMineIndex].ContainerPosition.y);
        MoveToLocation(shaftPos, nextDesiredState);
    }

    protected override void CollectionUpdate(float amount)
    {
        mines[currentMineIndex].RemoveFromContainer(amount);
        base.CollectionUpdate(amount);
    }





}
