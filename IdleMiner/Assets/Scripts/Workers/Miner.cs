using UnityEngine;

public class Miner : WorkerBase {

    private Animator workerAnimator;

    protected override void Awake()
    {
        base.Awake();

        workerAnimator = gameObject.GetComponentInChildren<Animator>();
        CarryValueTextRenderer.enabled = false;

        // Collect an infinte amount of resources from the mine
        CollectionMethod = (x) => { return x; };
        DepositAction = (x) => { MyArea.DepositContainer.AddToContainer(x); };
    }

    protected override void ReceiveOrders(WorkerStates nextDesiredState)
    {
        workerAnimator.Play("wait");
        base.ReceiveOrders(nextDesiredState);
    }

    protected override void Collect(WorkerStates nextDesiredState)
    {
        workerAnimator.Play("collect");
        base.Collect(nextDesiredState);
    }

    protected override void MoveToCollect(WorkerStates nextDesiredState)
    {
        workerAnimator.Play("roll");
        WorkerSprite.flipX = true;
        base.MoveToCollect(nextDesiredState);
    }

    protected override void MoveToDeposit(WorkerStates nextDesiredState)
    {
        workerAnimator.Play("roll");
        WorkerSprite.flipX = false;
        base.MoveToDeposit(nextDesiredState);
    }


}
