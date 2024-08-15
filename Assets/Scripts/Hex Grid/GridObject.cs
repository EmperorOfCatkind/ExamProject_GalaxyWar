using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Ship> shipList;
    private List<SpaceWaypoint> spaceWaypointsList;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        shipList = new List<Ship>();
        spaceWaypointsList = new List<SpaceWaypoint>();
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    public void AddShip(Ship ship)
    {
        shipList.Add(ship);
    }

    public void AddSpaceWaypoint(SpaceWaypoint spaceWaypoint)
    {
        spaceWaypointsList.Add(spaceWaypoint);
    }

    public List<SpaceWaypoint> GetSpaceWaypointsList()
    {
        return spaceWaypointsList;
    }


    public SpaceWaypoint GetAvailableSpaceWaypoint()
    {
        foreach(var spaceWaypoint in spaceWaypointsList)
        {
            if(spaceWaypoint.hasShip == false)
            {
                return spaceWaypoint;
            }
        }
        Debug.LogAssertion("All waypoints are occupied for hex " + gridPosition.ToString());
        return null;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }
}
