using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GridSystem
{

    private int width;
    private int height;
    private float tileSize;

    private GridObject[,] gridObjectArray;

    public GridSystem(int width, int height, float tileSize)
    {
        this.width = width;
        this.height = height;
        this.tileSize = tileSize;

        gridObjectArray = new GridObject[width, height];
        for(int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x,z] = new GridObject(this, gridPosition);
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * tileSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / tileSize), Mathf.RoundToInt(worldPosition.z / tileSize));
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    
    public void DisplayCoordinates(Transform coordinatesPrefab)
    {
        for (int x = 0; x < width; x++){
            for (int z = 0; z < height; z++){
                GridPosition gridPosition = new GridPosition(x, z);
                Transform coordinates = GameObject.Instantiate(coordinatesPrefab, GetWorldPosition(gridPosition), Quaternion.identity);

                MapGridCoordinates mapGridCoordinates = coordinates.GetComponent<MapGridCoordinates>();
                mapGridCoordinates.SetCoordinates(GetGridObject(gridPosition));
            }
        }
    }
}
