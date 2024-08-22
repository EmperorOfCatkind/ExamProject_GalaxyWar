using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class UnitToBuild
{
    public UnitData unitData {get; set;}

    public UnitToBuild(UnitData unitData)
    {
        this.unitData = unitData;
    }
}
