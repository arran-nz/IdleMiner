using UnityEngine;

/// <summary>
/// This worker collects from each mine container and brings the value to the surface
/// </summary>
public class ElevatorOperator : WorkerBase {

    private Animator animator;
    private MineManager mineManager;

    /// <summary>
    /// Selected mine to collect from
    /// </summary>
    int selectedMine = 0;

    /// <summary>
    /// Used to cycle mines without exceeding the mine array length
    /// </summary>
    private int CycleMineIndex(int desiredIndex)
    {
        if (desiredIndex < mineManager.MineCount)
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

        mineManager = GameController.Instance.MineManager;
        animator = gameObject.GetComponentInChildren<Animator>();

        DepositAction = (x) => { MyArea.DepositContainer.AddToContainer(x); };
    }

    /// <summary>
    /// Check's if there is value for purchasing to the next state, starting from the top of the mine
    /// </summary>
    /// <param name="nextDesiredState"></param>
    protected override void ReceiveOrders(WorkerStates nextDesiredState)
    {
        animator.Play("move");

        // Check if MineList has any value before moving to the next state
        bool foundValue = false;
        for (int i = 0; i < mineManager.MineCount; i++)
        {
            if (mineManager.Mines[i].DepositContainer.HasValue)
            {
                foundValue = true;
                i = mineManager.MineCount;
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

    /// <summary>
    /// Move to the selected Mine shaft position
    /// </summary>
    /// <param name="nextDesiredState"></param>
    protected override void MoveToCollect(WorkerStates nextDesiredState)
    {
        Vector2 shaftPos = new Vector2(MyArea.transform.position.x, mineManager.Mines[selectedMine].transform.position.y);
        MoveToLocation(shaftPos, nextDesiredState);
    }

    /// <summary>
    /// Collect from the selected mine deposit container
    /// </summary>
    /// <param name="nextDesiredState"></param>
    protected override void Collect(WorkerStates nextDesiredState)
    {
        // Set Collection from the current Mine's deposit container
        Container currentMineContainer = mineManager.Mines[selectedMine].DepositContainer;
        CollectionMethod = currentMineContainer.CollectFromContainer;

        if(currentMineContainer.HasValue)
        {
            // If CarryAmount has reached capacity it will progress to the next state
            base.Collect(nextDesiredState);
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
