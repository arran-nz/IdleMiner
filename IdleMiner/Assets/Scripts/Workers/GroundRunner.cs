using UnityEngine;

/// <summary>
/// This worker tranfers value from the elevator's container to sell it
/// </summary>
public class GroundRunner : WorkerBase {

    private Animator animator;
    private Container collectionContainer;

    protected override void Awake()
    {
        base.Awake();
   
        animator = gameObject.GetComponentInChildren<Animator>();

        collectionContainer = GameController.Instance.Elevator.DepositContainer;

        CollectionMethod = collectionContainer.CollectFromContainer;

        // Depoits straight into the cash pile (selling it)
        DepositAction = (x) => { GameController.Instance.AddCash(x); };
        

    }

    /// <summary>
    /// If collection container has value, procede to the base class state
    /// </summary>
    /// <param name="nextDesiredState"></param>
    protected override void ReceiveOrders(WorkerStates nextDesiredState)
    {
        animator.Play("collect");

        if (collectionContainer.HasValue)        {

            base.ReceiveOrders(nextDesiredState);
        }
    }

    /// <summary>
    /// Collect from the elevators deposit container
    /// </summary>
    /// <param name="nextDesiredState"></param>
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

    /// <summary>
    /// Animate and exucute the base state
    /// </summary>
    /// <param name="nextDesiredState"></param>
    protected override void Deposit(WorkerStates nextDesiredState)
    {
        animator.Play("collect");

        base.Deposit(nextDesiredState);
    }

    /// <summary>
    /// Flip the sprite, animate and execute the base state
    /// </summary>
    /// <param name="nextDesiredState"></param>
    protected override void MoveToCollect(WorkerStates nextDesiredState)
    {
        animator.Play("move");
        WorkerSprite.flipX = false;
        base.MoveToCollect(nextDesiredState);
    }

    /// <summary>
    /// Flip the sprite, animate and execute the base state
    /// </summary>
    protected override void MoveToDeposit(WorkerStates nextDesiredState)
    {
        animator.Play("move");
        WorkerSprite.flipX = true;
        base.MoveToDeposit(nextDesiredState);
    }
}
