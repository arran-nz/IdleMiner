using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : WorkingAreaBase {

    protected override void Start()
    {
        base.Start();

        WorkingAreaName = "Elevator";

        MovementSpeed.DisplayName = "Flying Speed";
        MovementSpeed.Value = 1;

        CollectionSpeed.Value = 140;

        CarryCapacity.Value = 200;

        Workers.DisplayName = "Elevator Birds";
        Workers.Value = 1;


    }

}
