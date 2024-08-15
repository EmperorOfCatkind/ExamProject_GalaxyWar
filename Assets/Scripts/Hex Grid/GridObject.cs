using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;

    private List<Ship> shipList;
    private List<SpaceDock> spaceDocks;

    private List<SpaceWaypoint> spaceWaypointsList;
    private List<Planet> planets;
    private Dictionary<Planet, SpaceDockWaypoint> spaceDockWaypoints;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;

        shipList = new List<Ship>();
        spaceDocks = new List<SpaceDock>();

        spaceWaypointsList = new List<SpaceWaypoint>();
        planets = new List<Planet>();
        spaceDockWaypoints = new Dictionary<Planet, SpaceDockWaypoint>();

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

    public void AddPlanet(Planet planet)
    {
        planets.Add(planet);
    }
    public void AddSpaceDockWaypoint(Planet planet)
    {
        spaceDockWaypoints.Add(planet, planet.GetSpaceDockWaypoint());
    }
    public Dictionary<Planet, SpaceDockWaypoint> GetSpaceDockWaypoints()
    {
        return spaceDockWaypoints;
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

    public SpaceDockWaypoint GetAvailableSpaceDockWaypoint()
    {
        foreach(var key in spaceDockWaypoints)  //check all planets to see if any of them has free dock point
        {
            if (key.Key.GetSpaceDockWaypoint().hasDock == false)
            {
                return key.Value;
            }
        }

        Debug.LogAssertion("No free planet on this hex " + gridPosition.ToString());
        return null;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }
}
