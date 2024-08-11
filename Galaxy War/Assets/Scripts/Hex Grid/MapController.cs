using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private IMapFunctionalService mapFunctionalService;
    private IMapVisualService mapVisualService;
    private GridSystem gridSystem;
    private GridPosition[,] gridPositions;

    private MapGridViewSingle[,] mapGridViewSingleArray;

    [SerializeField] private Transform coordinatesPrefab;
    [SerializeField] private Transform hexTilePrefab;

    void Awake()
    {
        mapFunctionalService = ProjectContext.Instance.MapFunctionalService;
        mapVisualService = ProjectContext.Instance.MapVisualService;

        gridSystem = mapFunctionalService.GridSystem;
        gridPositions = mapVisualService.GridPositions;

        gridSystem.DisplayCoordinates(coordinatesPrefab);
        InitializeGridView();
        HideAll();

    }
    // Start is called before the first frame update
    void Start()
    {
        /*gridSystem.DisplayCoordinates(coordinatesPrefab);
        InitializeGridView();
        HideAll();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeGridView()
    {
        mapGridViewSingleArray = new MapGridViewSingle[gridPositions.GetLength(0), gridPositions.GetLength(1)];

        for (int x = 0; x < gridPositions.GetLength(0); x++)
        {
            for (int z = 0; z < gridPositions.GetLength(1); z++)
            {
                GridPosition gridPosition = gridPositions[x,z];

                Transform mapGridViewTransform = Instantiate(hexTilePrefab, gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);
                mapGridViewSingleArray[x,z] = mapGridViewTransform.GetComponent<MapGridViewSingle>();
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
}
