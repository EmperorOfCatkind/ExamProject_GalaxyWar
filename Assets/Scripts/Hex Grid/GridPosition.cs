using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct GridPosition : IEquatable<GridPosition>
{
    public int x;
    public int z;
    //private GridSystem gridSystem;
    //public List<GridPosition> neighbours;

    public GridPosition(int x, int z)
    {

        this.x = x;
        this.z = z;
        //gridSystem = null;
        //neighbours = new List<GridPosition>();
        //neighbours = GetNeighbours();
    }

    public override string ToString()
    {
        return "x: " + x + "; z: " + z;
    }

    public static bool operator == (GridPosition a, GridPosition b)
    {
        return a.x == b.x && a.z == b.z;
    }

    public static bool operator != (GridPosition a, GridPosition b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
            x == position.x &&
            z == position.z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x + b.x, a.z + b.z);
    }

    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x - b.x, a.z - b.z);
    }
    /*public void SetGridSystem(GridSystem gridSystem)
    {
        this.gridSystem = gridSystem;
    }*/
    /*public List<GridPosition> GetNeighbours(GridSystem gridSystem)
    {
        List<GridPosition> gridPositionsInSystem = gridSystem.GetGridPositionsList();
        List<GridPosition> neighbours = new List<GridPosition>();

        bool isOddRow = z % 2 == 1;

        if(isOddRow)
        {
            //left
            if(gridPositionsInSystem.Contains())
            {
                neighbours.Add(new GridPosition(x - 1, z));
            }
            //right
            if(gridSystem.IsInBounds(new GridPosition(x + 1, z)))
            {
                neighbours.Add(new GridPosition(x + 1, z));
            }
            //top left
            if(gridSystem.IsInBounds(new GridPosition(x, z + 1)))
            {
                neighbours.Add(new GridPosition(x, z + 1));
            }
            //bottom left
            if(gridSystem.IsInBounds(new GridPosition(x, z - 1)))
            {
                neighbours.Add(new GridPosition(x, z - 1));
            }
            //top right
            if(gridSystem.IsInBounds(new GridPosition(x + 1, z + 1)))
            {
                neighbours.Add(new GridPosition(x + 1, z + 1));
            }
            //bottom right
            if(gridSystem.IsInBounds(new GridPosition(x + 1, z - 1)))
            {
                neighbours.Add(new GridPosition(x + 1, z - 1));
            }
        }
        else if(!isOddRow)
        {
            //left
            if(gridSystem.IsInBounds(new GridPosition(x - 1, z)))
            {
                neighbours.Add(new GridPosition(x - 1, z));
            }
            //right
            if(gridSystem.IsInBounds(new GridPosition(x + 1, z)))
            {
                neighbours.Add(new GridPosition(x + 1, z));
            }
            //top left
            if(gridSystem.IsInBounds(new GridPosition(x - 1, z + 1)))
            {
                neighbours.Add(new GridPosition(x - 1, z + 1));
            }
            //bottom left
            if(gridSystem.IsInBounds(new GridPosition(x - 1, z - 1)))
            {
                neighbours.Add(new GridPosition(x - 1, z - 1));
            }
            //top right
            if(gridSystem.IsInBounds(new GridPosition(x, z + 1)))
            {
                neighbours.Add(new GridPosition(x, z + 1));
            }
            //bottom right
            if(gridSystem.IsInBounds(new GridPosition(x, z - 1)))
            {
                neighbours.Add(new GridPosition(x, z - 1));
            }
        }

        return neighbours;
    }*/
}
