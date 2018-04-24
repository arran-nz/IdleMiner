using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour {

    public TextMesh carryValueText;

    private Mine myMine;

    public float WalkingSpeed = 1f;
    public float MiningSpeed = 1f;
    public float CarryCapacity = 10f;


    private enum WorkerStates
    {
        walkToMine,
        mine,
        walkToBucket,
        emptyLoad,
        waitingForWork

    }

    private float currentCarryAmount;
    private WorkerStates currentState;

    private void Awake()
    {
        myMine = gameObject.GetComponentInParent<Mine>();
        UpdateText(0);
        currentState = WorkerStates.walkToMine;
    }

    // Update is called once per frame
    void Update () {

        switch (currentState)
        {
            case WorkerStates.walkToMine:
                WalkToLocation(myMine.MinePosition, WorkerStates.mine);
                break;
            case WorkerStates.mine:
                Mine(WorkerStates.walkToBucket);
                break;
            case WorkerStates.walkToBucket:
                WalkToLocation(myMine.BucketPosition, WorkerStates.emptyLoad);
                break;
            case WorkerStates.emptyLoad:
                EmptyLoad(WorkerStates.walkToMine);
                break;
            case WorkerStates.waitingForWork:
                WaitForWork(WorkerStates.walkToMine);
                break;
            default:
                break;
        }

    }

    void WaitForWork(WorkerStates nextDesiredState)
    {
        if(myMine.ManangerPresent)
        {
            currentState = nextDesiredState;
        }
        else
        {
            // USER TAPS SCREEN
        }
    }

    void EmptyLoad(WorkerStates nextDesiredState)
    {
        myMine.AddToBucket(currentCarryAmount);
        currentCarryAmount = 0;
        UpdateText(currentCarryAmount);
        currentState = nextDesiredState;
    }


    void Mine(WorkerStates nextDesiredState)
    {
        // Run every second
        if(currentCarryAmount + MiningSpeed <= CarryCapacity)
        {
            currentCarryAmount += MiningSpeed;
            UpdateText(currentCarryAmount);
        }
        else
        {
            currentState = nextDesiredState;
        }
    }

    void WalkToLocation(Vector2 position, WorkerStates nextDesiredState)
    {
        if ((Vector2)transform.position != position)
        {
            transform.position = Vector2.MoveTowards(transform.position, position, WalkingSpeed * Time.deltaTime);
        }
        else
        {
            currentState = nextDesiredState;
        }
    }

    void UpdateText(float newAmount)
    {
        carryValueText.text = newAmount + "k";
    }

}
