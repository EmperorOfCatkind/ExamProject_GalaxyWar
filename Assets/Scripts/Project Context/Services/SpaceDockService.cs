using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public interface ISpaceDockService
{
    DockSlot[] dockSlots {get;}
    void AddUnit(UnitToBuild unitToBuild);
}
public class SpaceDockService : ISpaceDockService
{
    public DockSlot[] dockSlots {get;}

    public SpaceDockService(IConfigService configService)
    {
        var units = configService.Units;
        dockSlots = new DockSlot[units.Length];

        for(int i = 0; i < dockSlots.Length; i++)
        {
            dockSlots[i] = new DockSlot(default);
        }

        for(int i = 0; i < units.Length; i++)
        {
            var unit = new UnitToBuild(units[i]);
            AddUnit(unit);
        }
    }

    public void AddUnit(UnitToBuild unitToBuild)
    {
        for(int i = 0; i < dockSlots.Length; i++)
        {
            if(dockSlots[i].CanPutInSlot(unitToBuild))
            {
                dockSlots[i].unitToBuild = unitToBuild;
                break;
            }
        }
    }
}
