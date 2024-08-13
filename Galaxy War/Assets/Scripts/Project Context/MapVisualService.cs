using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapVisualService
{
    Transform HexPrefab {get;}
    //MapGridViewSingle[,] MapGridViewSingles {get; set;}
    GridPosition[,] GridPositions {get; set;}
    void SetGridPositions();
}
public class MapVisualService : IMapVisualService
{
    public Transform HexPrefab {get;}
    //public MapGridViewSingle[,] MapGridViewSingles {get; set;}
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
        //MapGridViewSingles= new MapGridViewSingle[x,z];
        GridPositions = new GridPosition[x,z];

        for(int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++){
                GridPosition gridPosition = new GridPosition(i, j);
                GridPositions[i,j] = gridPosition;

                
                //Instantiate in Monobehavior
            }
        }
    }
}
