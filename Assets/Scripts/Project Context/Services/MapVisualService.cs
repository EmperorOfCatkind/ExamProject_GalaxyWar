using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapVisualService
{
    Transform HexPrefab {get;}
    GridPosition[,] GridPositions {get; set;}
    void SetGridPositions();
}
public class MapVisualService : IMapVisualService
{
    public Transform HexPrefab {get;}
    public GridPosition[,] GridPositions { get; set; }

    private IMapFunctionalService MapFunctionalService;
    private GridSystem GridSystem;

    public MapVisualService(IMapFunctionalService MapFunctionalService)
    {
        this.MapFunctionalService = MapFunctionalService;
        GridSystem = MapFunctionalService.GridSystem;
        SetGridPositions();
    }

    public void SetGridPositions()
    {
        int x = GridSystem.GetWidth();
        int z = GridSystem.GetHeight();
        
        GridPositions = new GridPosition[x,z];

        for(int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++){
                GridPosition gridPosition = new GridPosition(i, j);
                GridPositions[i,j] = gridPosition;
            }
        }
    }
}
