using UnityEngine;

/// <summary>
/// This worker collected resources and deposits them indefinitely
/// </summary>
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

    /// <summary>
    /// Animate and execute base state
    /// </summary>
    protected override void ReceiveOrders(WorkerStates nextDesiredState)
    {
        workerAnimator.Play("wait");
        base.ReceiveOrders(nextDesiredState);
    }

    /// <summary>
    /// Animate and execute base state
    /// </summary>
    /// <param name="nextDesiredState"></param>
    protected override void Collect(WorkerStates nextDesiredState)
    {
        workerAnimator.Play("collect");
        base.Collect(nextDesiredState);
    }

    /// <summary>
    /// Flip the sprite, animate and execute the base state
    /// </summary>
    protected override void MoveToCollect(WorkerStates nextDesiredState)
    {
        workerAnimator.Play("roll");
        WorkerSprite.flipX = true;
        base.MoveToCollect(nextDesiredState);
    }

    /// <summary>
    /// Flip the sprite, animate and execute the base state
    /// </summary>
    protected override void MoveToDeposit(WorkerStates nextDesiredState)
    {
        workerAnimator.Play("roll");
        WorkerSprite.flipX = false;
        base.MoveToDeposit(nextDesiredState);
    }


}
