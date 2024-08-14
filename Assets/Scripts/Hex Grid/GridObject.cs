using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Ship> shipList;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        shipList = new List<Ship>();
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    public void AddShip(Ship ship)
    {
        shipList.Add(ship);
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }
}
