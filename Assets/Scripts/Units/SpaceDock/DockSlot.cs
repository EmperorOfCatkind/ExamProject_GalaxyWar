using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockSlot
{
    public UnitToBuild unitToBuild {get; set;}
    public bool isEmpty => unitToBuild == null;

    public DockSlot(UnitToBuild unitToBuild)
    {
        this.unitToBuild = unitToBuild;
    }

    public bool CanPutInSlot(UnitToBuild unitToBuild)
    {
        if(isEmpty)
        {
            return true;
        }
        return false;
    }
}
