using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorkingAreaBase : MonoBehaviour {


    public Container DepositContainer;

    public List<Vector2> CollectPositions = new List<Vector2>();

    public Vector2 DepositContainerPosition
    {
        get
        {
            return DepositContainer.transform.position;
        }
    }


    public bool ManangerPresent { get; private set; }
    public float MovementSpeed { get; protected set; }
    public float CollectionSpeed { get; protected set; }
    public float CarryCapacity { get; protected set; }

    protected virtual void Start()
    {
        ManangerPresent = true;
    }

    private void Awake()
    {
        DepositContainer = gameObject.GetComponentInChildren<Container>();
    }

}
