using UnityEngine;

public class GroundRunner : WorkerBase {

    private Animator animator;
    private Container collectionContainer;

    protected override void Awake()
    {
        base.Awake();
   
        animator = gameObject.GetComponentInChildren<Animator>();

        collectionContainer = GameController.Instance.Elevator.DepositContainer;

        CollectionMethod = collectionContainer.CollectFromContainer;
        DepositAction = (x) => { GameController.Instance.AddCash(x); };
        

    }

    protected override void ReceiveOrders(WorkerStates nextDesiredState)
    {
        animator.Play("collect");

        if (collectionContainer.HasValue)        {

            base.ReceiveOrders(nextDesiredState);
        }
    }

    protected override void Collect(WorkerStates nextDesiredState)
    {
        animator.Play("collect");

        if (collectionContainer.HasValue)
        {

            base.Collect(nextDesiredState);
        }
        else
        {
            ChangeState(nextDesiredState);
        }
    }

    protected override void MoveToCollect(WorkerStates nextDesiredState)
    {
        animator.Play("move");
        WorkerSprite.flipX = false;
        base.MoveToCollect(nextDesiredState);
    }

    protected override void MoveToDeposit(WorkerStates nextDesiredState)
    {
        animator.Play("move");
        WorkerSprite.flipX = true;
        base.MoveToDeposit(nextDesiredState);
    }
}
