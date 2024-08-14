using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;

public class GridSystem
{
    private const float HEX_X_OFFSET_MULTIPLIER = .5f;
    private const float HEX_Z_OFFSET_MULTIPLIER = .75f;
    private int width;
    private int height;
    private float hexSize;
    private List<Vector3Int> neighbourHexesList;

    private GridObject[,] gridObjectArray;

    public GridSystem(int width, int height, float hexSize)
    {
        this.width = width;
        this.height = height;
        this.hexSize = hexSize;

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
        return
            new Vector3(gridPosition.x, 0, 0) * hexSize +
            new Vector3(0, 0, gridPosition.z) * hexSize * HEX_Z_OFFSET_MULTIPLIER +
            ((gridPosition.z % 2) == 1 ? new Vector3(1, 0, 0) * hexSize * HEX_X_OFFSET_MULTIPLIER : Vector3.zero);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)  //TODO - remove after updating to GetHexGridPosition
    {
        int x = Mathf.RoundToInt(worldPosition.x / hexSize);
        int z = Mathf.RoundToInt(worldPosition.z / hexSize);
        return new GridPosition(x, z);
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
    public List<Vector3Int> GetNeighbourHexesList()
    {
        return neighbourHexesList;
    }
    
    public void DisplayCoordinates(Transform coordinatesPrefab)     //Debug Purposes
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

    public GridPosition GetHexGridPosition(Vector3 worldPosition)
    {
        int roughX = Mathf.RoundToInt(worldPosition.x / hexSize);
        int roughZ = Mathf.RoundToInt(worldPosition.z / hexSize / HEX_Z_OFFSET_MULTIPLIER);

        Vector3Int roughXZ = new Vector3Int(roughX, 0, roughZ);


        bool isOddRow = roughZ % 2 == 1;
        neighbourHexesList = new List<Vector3Int>
        {
            roughXZ + new Vector3Int(-1, 0, 0),
            roughXZ + new Vector3Int(+1, 0, 0),

            roughXZ + new Vector3Int(isOddRow ? +1 : -1, 0, +1),
            roughXZ + new Vector3Int(+0, 0, +1),

            roughXZ + new Vector3Int(isOddRow ? +1 : -1, 0, -1),
            roughXZ + new Vector3Int(+0, 0, -1),    
        };

        //Debug.Log("XXXXXXXXX");
        //Debug.Log(roughXZ);

        Vector3Int closestGridPosition = roughXZ;

        foreach (Vector3Int neighbourHex in neighbourHexesList)
        {
            //Debug.Log(neighbourHex);
            if(Vector3.Distance(worldPosition, GetWorldPosition(new GridPosition(neighbourHex.x, neighbourHex.z))) < 
               Vector3.Distance(worldPosition, GetWorldPosition(new GridPosition(closestGridPosition.x, closestGridPosition.z))))
               {
                    closestGridPosition = neighbourHex;
               }
        }
        return new GridPosition(closestGridPosition.x, closestGridPosition.z);
    }

    public bool IsInBounds(GridPosition gridPosition)
    {
        if(gridPosition.x >= 0 && gridPosition.x < width && gridPosition.z >= 0 && gridPosition.z < height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public GridObject[,] GetGridObjectArray()
    {
        return gridObjectArray;
    }
}
