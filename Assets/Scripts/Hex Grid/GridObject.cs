using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private Dictionary<PlayerType, List<Ship>> shipListByPlayerType;
    private List<SpaceDock> spaceDocks;

    private List<SpaceWaypoint> spaceWaypointsList;
    private List<Planet> planets;
    private Dictionary<Planet, SpaceDockWaypoint> spaceDockWaypoints;
    private Dictionary<Planet, GroundForceWaypoint[]> groundForceWaypoints;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;

        shipListByPlayerType = new Dictionary<PlayerType, List<Ship>>();
        spaceDocks = new List<SpaceDock>();

        spaceWaypointsList = new List<SpaceWaypoint>();
        planets = new List<Planet>();
        spaceDockWaypoints = new Dictionary<Planet, SpaceDockWaypoint>();
        groundForceWaypoints = new Dictionary<Planet, GroundForceWaypoint[]>();

    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    public Dictionary<PlayerType, List<Ship>> GetShipListByPlayerType()
    {
        return shipListByPlayerType;
    }
    public void AddShip(Ship ship)
    {
        if(!shipListByPlayerType.ContainsKey(ship.GetPlayerType()))
        {
            List<Ship> shipList = new List<Ship>();
            shipList.Add(ship);
            shipListByPlayerType.Add(ship.GetPlayerType(), shipList);
            return;
        }

        if(shipListByPlayerType[ship.GetPlayerType()].Count < 3)
        {
            shipListByPlayerType[ship.GetPlayerType()].Add(ship);
        }
        else
        {
            Debug.LogAssertion("Max 3 ship per player on Hex!");
        }
        
    }
    public void RemoveShip(Ship ship)
    {
        ship.GetCurrentWaypoint().hasShip = false;
        shipListByPlayerType[ship.GetPlayerType()].Remove(ship);
        if(shipListByPlayerType[ship.GetPlayerType()].Count == 0)
        {
            shipListByPlayerType.Remove(ship.GetPlayerType());
        }
    }

    public void AddSpaceWaypoint(SpaceWaypoint spaceWaypoint)
    {
        spaceWaypointsList.Add(spaceWaypoint);
    }

    public List<SpaceWaypoint> GetSpaceWaypointsList()
    {
        return spaceWaypointsList;
    }


    public List<Planet> GetPlanets()
    {
        return planets;
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
    public void AddGroundForceWaypoints(Planet planet)
    {
        groundForceWaypoints.Add(planet, planet.GetGroundForceWaypoints());
    }

    public bool GridObjectIsAvailable(PlayerType playerType)
    {
        if(!shipListByPlayerType.ContainsKey(playerType) || shipListByPlayerType[playerType].Count < 3)
        {
            return true;
        }
        
        return false;
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

    public Planet GetAvailablePlanetForSpaceDock()
    {
        foreach(var key in spaceDockWaypoints)  
        {
            if (key.Key.GetSpaceDockWaypoint().hasDock == false)
            {
                return key.Key;
            }
        }

        Debug.LogAssertion("No free planet on this hex " + gridPosition.ToString());
        return null;
    }

    public Planet GetAvailablePlanetForGroundForce()
    {
        foreach(var planet in groundForceWaypoints)
        {
            foreach(var waypoint in planet.Key.GetGroundForceWaypoints()) 
            {
                if(waypoint.hasGroundForce == false)
                {
                    return planet.Key;
                }
            }
        }
        Debug.LogAssertion("No place for additional ground force on this hex " + gridPosition.ToString());
        return null;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }
}
