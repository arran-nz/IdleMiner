using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : WorkerBase {


    private Animator workerAnimator;


    protected override void Awake()
    {
        workerAnimator = gameObject.GetComponentInChildren<Animator>();
        base.Awake();
    }

    protected override void WaitForWork(WorkerStates nextDesiredState)
    {
        workerAnimator.Play("wait");
        base.WaitForWork(nextDesiredState);
    }

    protected override void Collect(WorkerStates nextDesiredState, Func<WorkerStates, WorkerStates> finishedCollecting)
    {
        workerAnimator.Play("punch");
        base.Collect(nextDesiredState, finishedCollecting);
    }
    
    protected override void MoveToLocation(Vector2 position, WorkerStates nextDesiredState)
    {
        // Make the miner face the correct direction while moving to the new location.
        Vector2 direction = (Vector2)transform.position - position;
        workerSprite.flipX = direction.x > 0 ? true : false;

        workerAnimator.Play("walk");

        base.MoveToLocation(position, nextDesiredState);
    }


}
