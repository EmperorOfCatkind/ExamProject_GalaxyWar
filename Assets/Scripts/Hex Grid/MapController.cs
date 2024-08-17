using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController Instance;

    private IMapFunctionalService mapFunctionalService;
    private IMapVisualService mapVisualService;
    private GridSystem gridSystem;
    private GridPosition[,] gridPositions;
    private GridObject[,] gridObjects;

    private MapGridViewSingle[,] mapGridViewSingleArray;

    [SerializeField] private Transform coordinatesPrefab;
    [SerializeField] private Transform hexTilePrefab;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        mapFunctionalService = ProjectContext.Instance.MapFunctionalService;
        mapVisualService = ProjectContext.Instance.MapVisualService;

        gridSystem = mapFunctionalService.GridSystem;
        gridPositions = mapVisualService.GridPositions;
        gridObjects = gridSystem.GetGridObjectArray();

        //gridSystem.DisplayCoordinates(coordinatesPrefab);   //debug purposes
        InitializeGridView();
        HideAll();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeGridView()
    {
        mapGridViewSingleArray = new MapGridViewSingle[gridObjects.GetLength(0), gridObjects.GetLength(1)];

        for (int x = 0; x < gridObjects.GetLength(0); x++)
        {
            for (int z = 0; z < gridObjects.GetLength(1); z++)
            {
                GridPosition gridPosition = gridObjects[x,z].GetGridPosition();

                Transform mapGridViewTransform = Instantiate(hexTilePrefab, gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);

                MapGridViewSingle mapGridViewSingle = mapGridViewTransform.GetComponent<MapGridViewSingle>();
                mapGridViewSingleArray[x,z] = mapGridViewSingle;

                GridObject gridObject = gridSystem.GetGridObject(gridPosition);
                mapGridViewSingleArray[x,z].SetGridObject(gridObject);

                foreach(var spaceWaypoint in mapGridViewSingle.GetSpaceWaypoints())
                {
                    gridObject.AddSpaceWaypoint(spaceWaypoint);
                }

                foreach(var planet in mapGridViewSingle.GetPlanets())
                {
                    gridObject.AddPlanet(planet);
                    gridObject.AddSpaceDockWaypoint(planet);
                    gridObject.AddGroundForceWaypoints(planet);
                }
            }
        }
    }
    public void HideAll()
    {
        for(int x = 0; x < mapGridViewSingleArray.GetLength(0); x++)
        {
            for(int z = 0; z < mapGridViewSingleArray.GetLength(1); z++)
            {
                mapGridViewSingleArray[x,z].Hide();
            }
        }
    }
    public MapGridViewSingle GetMapGridViewSingle(GridPosition gridPosition)
    {
        if(gridSystem.IsInBounds(gridPosition))
        {
            return mapGridViewSingleArray[gridPosition.x, gridPosition.z];
        }
        return null;
    }
    public void AddShipAtGridPosition(GridPosition gridPosition, Ship ship)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddShip(ship);
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);
    public GridPosition GetHexGridPosition(Vector3 worldPosition) => gridSystem.GetHexGridPosition(worldPosition);
    public bool IsInBounds(GridPosition gridPosition) => gridSystem.IsInBounds(gridPosition);
    public GridObject GetGridObject(GridPosition gridPosition) => gridSystem.GetGridObject(gridPosition);
}
