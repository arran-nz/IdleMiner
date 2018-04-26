using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRunner : WorkerBase {

    private Animator workerAnimator;
    private Container collectionContainer;

    protected override void Awake()
    {
        base.Awake();

        Elevator elevator = FindObjectOfType<Elevator>();
        collectionContainer = elevator.DepositContainer;
        
        workerAnimator = gameObject.GetComponentInChildren<Animator>();

    }

    protected override void ReceiveOrders(WorkerStates nextDesiredState)
    {
        workerAnimator.Play("wait");

        if (collectionContainer.HasValue)        {

            base.ReceiveOrders(nextDesiredState);
        }
    }

    protected override void Collect(WorkerStates nextDesiredState, Func<decimal, decimal> collectionMethod)
    {
        workerAnimator.Play("collect");

        collectionMethod = collectionContainer.CollectFromContainer;

        if (collectionContainer.HasValue)
        {

            base.Collect(nextDesiredState, collectionMethod);
        }
        else
        {
            ChangeState(nextDesiredState);
        }
    }

    protected override void MoveToCollect(WorkerStates nextDesiredState)
    {
        workerAnimator.Play("roll");
        WorkerSprite.flipX = false;
        base.MoveToCollect(nextDesiredState);
    }

    protected override void MoveToDeposit(WorkerStates nextDesiredState)
    {
        workerAnimator.Play("roll");
        WorkerSprite.flipX = true;
        base.MoveToDeposit(nextDesiredState);
    }
}
